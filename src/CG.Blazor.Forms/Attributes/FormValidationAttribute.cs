using System;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is a base for all form generaton validation attributes.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is only valid when placed on the class definition for
    /// the top-level model.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class)]
     
    public abstract class FormValidationAttribute : FormGeneratorAttribute
    {
        
    }
}
