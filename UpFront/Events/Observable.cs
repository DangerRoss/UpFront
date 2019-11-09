using System;

namespace UpFront.Events
{
    /// <summary>
    /// An Observable event that can have many observers subscribed and unsubscribed
    /// </summary>
    public sealed class Observable : Subject<Action>
    {
        public Observable() : base() { }

        /// <summary>
        /// Notifies subscribed observers
        /// </summary>
        public void Notify()
        {
            lock(this)
            {
                this.PrepareInvocation();

                for (int i = 0; i < this.invocations.Count; i++)
                {
                    this.invocations[i]();
                }
                this.ClearInvocations();
            }
        }

        /// <summary>
        /// Cancels any further observers from being notfied during a notify event
        /// </summary>
        public void CancelNotify() => this.ClearInvocations();


        /// <summary>
        /// Subscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Observable operator +(Observable observable, Action observer)
        {
            observable.Subscribe(observer);
            return observable;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Observable operator -(Observable observable, Action observer)
        {
            observable.Unsubscribe(observer);
            return observable;
        }
    }

    /// <summary>
    /// An Observable value that can have many observers subscribed and unsubscribed
    /// </summary>
    public sealed class Observable<T1> : Subject<Action<T1>>
    {
        public Observable() : base() { }

        /// <summary>
        /// Notifies subscribed observers
        /// </summary>
        public void Notify(T1 arg)
        {
            lock (this)
            {
                this.PrepareInvocation();

                for (int i = 0; i < this.invocations.Count; i++)
                {
                    this.invocations[i](arg);
                }
                this.ClearInvocations();
            }
        }

        /// <summary>
        /// Cancels any further observers from being notfied during a notify event
        /// </summary>
        public void CancelNotify() => this.ClearInvocations();

        /// <summary>
        /// Subscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Observable<T1> operator +(Observable<T1> observable, Action<T1> observer)
        {
            observable.Subscribe(observer);
            return observable;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Observable<T1> operator -(Observable<T1> observable, Action<T1> observer)
        {
            observable.Unsubscribe(observer);
            return observable;
        }
    }

    /// <summary>
    /// An Observable with multiple values that can have many observers subscribed and unsubscribed
    /// </summary>
    public sealed class Observable<T1, T2> : Subject<Action<T1, T2>>
    {
        public Observable() : base() { }

        /// <summary>
        /// Notifies subscribed observers
        /// </summary>
        public void Notify(T1 arg1, T2 arg2)
        {
            lock(this)
            {
                this.PrepareInvocation();

                for (int i = 0; i < this.invocations.Count; i++)
                {
                    this.invocations[i](arg1, arg2);
                }
                this.ClearInvocations();
            }
        }

        /// <summary>
        /// Cancels any further observers from being notfied during a notify event
        /// </summary>
        public void CancelNotify() => this.ClearInvocations();


        /// <summary>
        /// Subscribe operator to be compatiable with replacing .NET events
        /// </summary>
        public static Observable<T1, T2> operator +(Observable<T1, T2> observable, Action<T1, T2> observer)
        {
            observable.Subscribe(observer);
            return observable;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Observable<T1, T2> operator -(Observable<T1, T2> observable, Action<T1, T2> observer)
        {
            observable.Unsubscribe(observer);
            return observable;
        }
    }

    /// <summary>
    /// An Observable with multiple values that can have many observers subscribed and unsubscribed
    /// </summary>
    public sealed class Observable<T1, T2, T3> : Subject<Action<T1, T2, T3>>
    {
        public Observable() : base() { }

        /// <summary>
        /// Notifies subscribed observers
        /// </summary>
        public void Notify(T1 arg1, T2 arg2, T3 arg3)
        {
            lock(this)
            {
                this.PrepareInvocation();

                for (int i = 0; i < this.invocations.Count; i++)
                {
                    this.invocations[i](arg1, arg2, arg3);
                }
                this.ClearInvocations();
            }
        }

        /// <summary>
        /// Cancels any further observers from being notfied during a notify event
        /// </summary>
        public void CancelNotify() => this.ClearInvocations();


        /// <summary>
        /// Subscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Observable<T1, T2, T3> operator +(Observable<T1, T2, T3> observable, Action<T1, T2, T3> observer)
        {
            observable.Subscribe(observer);
            return observable;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Observable<T1, T2, T3> operator -(Observable<T1, T2, T3> observable, Action<T1, T2, T3> observer)
        {
            observable.Unsubscribe(observer);
            return observable;
        }
    }

    /// <summary>
    /// An Observable with multiple values that can have many observers subscribed and unsubscribed
    /// </summary>
    public sealed class Observable<T1, T2, T3, T4> : Subject<Action<T1, T2, T3, T4>>
    {
        public Observable() : base() { }

        /// <summary>
        /// Notifies subscribed observers
        /// </summary>
        public void Notify(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            lock(this)
            {
                this.PrepareInvocation();

                for (int i = 0; i < this.invocations.Count; i++)
                {
                    this.invocations[i](arg1, arg2, arg3, arg4);
                }
                this.ClearInvocations();
            }
        }

        /// <summary>
        /// Cancels any further observers from being notfied during a notify event
        /// </summary>
        public void CancelNotify() => this.ClearInvocations();

        /// <summary>
        /// Subscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Observable<T1, T2, T3, T4> operator +(Observable<T1, T2, T3, T4> observable, Action<T1, T2, T3, T4> observer)
        {
            observable.Subscribe(observer);
            return observable;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Observable<T1, T2, T3, T4> operator -(Observable<T1, T2, T3, T4> observable, Action<T1, T2, T3, T4> observer)
        {
            observable.Unsubscribe(observer);
            return observable;
        }
    }
}
