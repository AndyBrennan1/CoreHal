using System;

namespace CoreHal.Annotation
{
    /// <summary>
    /// Represents an attribute used to indictae that the propery, although present in the model, should not be added to the generated output.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class NoOutput : Attribute
    {
    }
}