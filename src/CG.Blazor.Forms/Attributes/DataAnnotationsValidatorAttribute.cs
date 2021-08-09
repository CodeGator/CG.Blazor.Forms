using System;
using Microsoft.AspNetCore.Components.Forms;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is an attribute that indicates the form should be 
    /// rendered with a <see cref="DataAnnotationsValidator"/> tag.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DataAnnotationsValidatorAttribute : FormGeneratorAttribute
    {
        
    }
}
