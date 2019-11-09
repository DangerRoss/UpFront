using System;

namespace UpFront.Events
{
    /// <summary>
    /// An Observable event that once notified unsubscribes its observers
    /// </summary>
    public sealed class Signal : Subject<Action>
    {
        public Signal() : base() { }

        /// <summary>
        /// Notifies subscribed observers and unsubcribes them
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
                this.ClearSubscribers();
            }
        }

        /// <summary>
        /// Subscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal operator +(Signal signal, Action observer)
        {
            signal.Subscribe(observer);
            return signal;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal operator -(Signal signal, Action observer)
        {
            signal.Unsubscribe(observer);
            return signal;
        }
    }


    /// <summary>
    /// An Observable value that once notified unsubscribes its observers
    /// </summary>
    public sealed class Signal<T1> : Subject<Action<T1>>
    {
        public Signal() : base() { }

        public void Notify(T1 arg)
        {
            lock(this)
            {
                this.PrepareInvocation();
                for (int i = 0; i < this.invocations.Count; i++)
                {
                    this.invocations[i](arg);
                }
                this.ClearInvocations();
                this.ClearSubscribers();
            }
        }

        /// <summary>
        /// Subscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal<T1> operator +(Signal<T1> signal, Action<T1> observer)
        {
            signal.Subscribe(observer);
            return signal;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal<T1> operator -(Signal<T1> signal, Action<T1> observer)
        {
            signal.Unsubscribe(observer);
            return signal;
        }
    }

    /// <summary>
    /// An Observable with multiple values that once notified unsubscribes its observers
    /// </summary>
    public sealed class Signal<T1, T2> : Subject<Action<T1, T2>>
    {
        public Signal() : base() { }

        /// <summary>
        /// Notifies subscribed observers and unsubcribes them
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
                this.ClearSubscribers();
            }
        }

        /// <summary>
        /// Subscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal<T1, T2> operator +(Signal<T1, T2> signal, Action<T1, T2> observer)
        {
            signal.Subscribe(observer);
            return signal;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal<T1, T2> operator -(Signal<T1, T2> signal, Action<T1, T2> observer)
        {
            signal.Unsubscribe(observer);
            return signal;
        }
    }

    /// <summary>
    /// An Observable with multiple values that once notified unsubscribes its observers
    /// </summary>
    public sealed class Signal<T1, T2, T3> : Subject<Action<T1, T2, T3>>
    {
        public Signal() : base() { }

        /// <summary>
        /// Notifies subscribed observers and unsubcribes them
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
                this.ClearSubscribers();
            }
        }

        /// <summary>
        /// Subscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal<T1, T2, T3> operator +(Signal<T1, T2, T3> signal, Action<T1, T2, T3> observer)
        {
            signal.Subscribe(observer);
            return signal;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal<T1, T2, T3> operator -(Signal<T1, T2, T3> signal, Action<T1, T2, T3> observer)
        {
            signal.Unsubscribe(observer);
            return signal;
        }
    }

    /// <summary>
    /// An Observable with multiple values that once notified unsubscribes its observers
    /// </summary>
    public sealed class Signal<T1, T2, T3, T4> : Subject<Action<T1, T2, T3, T4>>
    {
        public Signal() : base() { }

        /// <summary>
        /// Notifies subscribed observers and unsubcribes them
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
                this.ClearSubscribers();
            }
        }

        /// <summary>
        /// Subscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal<T1, T2, T3, T4> operator +(Signal<T1, T2, T3, T4> signal, Action<T1, T2, T3, T4> observer)
        {
            signal.Subscribe(observer);
            return signal;
        }

        /// <summary>
        /// Usubscribe operator to be compatible with replacing .NET events
        /// </summary>
        public static Signal<T1, T2, T3, T4> operator -(Signal<T1, T2, T3, T4> signal, Action<T1, T2, T3, T4> observer)
        {
            signal.Unsubscribe(observer);
            return signal;
        }
    }
}
