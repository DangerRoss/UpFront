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

            if(this.observers.Count > 0)
            {
                this.invocations.AddRange(this.observers);
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

        public void Subscribe(T observer) => this.observers.Add(observer);
        
        public bool IsSubscribed(T observer)
        {
            for(int i = 0; i < this.observers.Count; i++)
            {
                if(this.observers[i] == observer)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasSubscribers() => this.observers.Count > 0;

        public void Unsubscribe(T observer)
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

        public void UnsubscribeLast(T observer)
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

        public void UnsubscribeAll(T observer)
        {
            for (int i = this.observers.Count - 1; i >= 0; i--)
            {
                if (this.observers[i] == observer)
                {
                    this.observers.RemoveAt(i);
                }
            }
        }

        public void ClearSubscribers() => this.observers.Clear();     
    }
}
