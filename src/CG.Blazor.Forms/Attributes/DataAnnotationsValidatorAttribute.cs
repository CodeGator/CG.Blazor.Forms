using System;
using Microsoft.AspNetCore.Components.Forms;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is an attribute that indicates the form should be 
    /// rendered with a <see cref="DataAnnotationsValidator"/> tag.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Decorating the top-level model's class with this attribute causes 
    /// the form generator to render a <see cref="DataAnnotationsValidator"/>
    /// inside the generated form.
    /// </para>
    /// <para>
    /// This attribute is only valid when placed on the class definition for
    /// the top-level model class.
    /// </para>
    /// </remarks>    
    [AttributeUsage(AttributeTargets.Class)]
    public class DataAnnotationsValidatorAttribute : FormGeneratorAttribute
    {
        
    }
}
