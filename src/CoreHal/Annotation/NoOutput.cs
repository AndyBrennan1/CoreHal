using System;

namespace CoreHal.Annotation
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class NoOutput : Attribute
    {
    }
}