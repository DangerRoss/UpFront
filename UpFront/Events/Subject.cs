using System;
using System.Collections.Generic;
using System.Threading;

namespace UpFront.Events
{
    /// <summary>
    /// Base implementation for observers that have subscribable delegates
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Subject<T> where T : Delegate
    {
        protected List<T> observers;
        protected List<T> invocations;

        public Subject()
        {
            this.observers = new List<T>();
            this.invocations = new List<T>();
        }

        protected void PrepareInvocation()
        {
            if (!Monitor.IsEntered(this))
            {
                throw new SynchronizationLockException();
            }

            lock(this.observers)
            {
                if (this.observers.Count > 0)
                {
                    for (int i = 0; i < this.observers.Count; i++)
                    {
                        this.invocations.Add(this.observers[i]);
                    }
                }
            }    
        }

        protected void ClearInvocations()
        {
            if (!Monitor.IsEntered(this))
            {
                throw new SynchronizationLockException();
            }

            this.invocations.Clear();
        }

        public void Subscribe(T observer)
        {
            lock(this.observers)
            {
                this.observers.Add(observer);
            }
        }
        
        public bool IsSubscribed(T observer)
        {
            lock (this.observers)
            {
                for (int i = 0; i < this.observers.Count; i++)
                {
                    if (this.observers[i] == observer)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool HasSubscribers()
        {
            lock(this.observers)
            {
               return this.observers.Count > 0;
            }
        }

        public void Unsubscribe(T observer)
        {
            lock (this.observers)
            {
                for (int i = 0; i < this.observers.Count; i++)
                {
                    if (this.observers[i] == observer)
                    {
                        this.observers.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void UnsubscribeLast(T observer)
        {
            lock (this.observers)
            {
                for (int i = this.observers.Count - 1; i >= 0; i--)
                {
                    if (this.observers[i] == observer)
                    {
                        this.observers.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        public void UnsubscribeAll(T observer)
        {
            lock(this.observers)
            {
                for (int i = this.observers.Count - 1; i >= 0; i--)
                {
                    if (this.observers[i] == observer)
                    {
                        this.observers.RemoveAt(i);
                    }
                }
            }
        }

        public void ClearSubscribers()
        {
            lock(this.observers)
            {
                this.observers.Clear();
            }
        }
    }
}
