#region Using namespaces...

using System;

#endregion


namespace Expanse.Core.Attributes
{
    [AttributeUsage(
        AttributeTargets.Constructor |
        AttributeTargets.Method |
        AttributeTargets.Property |
        AttributeTargets.Field,
        AllowMultiple = false, Inherited = true)]
    public sealed class AutoAttribute : Attribute
    {

    }
}