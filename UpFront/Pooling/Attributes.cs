using System;

namespace UpFront.Pooling
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class Initialiser : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ReInitialiser : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class Finaliser : Attribute { }
}
