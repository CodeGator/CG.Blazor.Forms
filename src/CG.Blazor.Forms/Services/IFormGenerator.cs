using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace CG.Blazor.Forms.Services
{
    /// <summary>
    /// This interface represents an object that generates HTML forms.
    /// </summary>
    public interface IFormGenerator
    {
        /// <summary>
        /// This method generated an HTML form using a POCO model and 
        /// the specified <see cref="RenderTreeBuilder"/> object.
        /// </summary>
        /// <param name="builder">The render tree builder to use for the operation.</param>
        /// <param name="eventTarget">The target for any form related events.</param>
        /// <param name="model">The POCO data model to use for the operation.</param>
        /// <exception cref="FormGenerationException">This exception is thrown whenever
        /// the generation operation fails to complete.</exception>
        /// <exception cref="ArgumentException">This extension is thrown whenever one or 
        /// more arguments are missing, or invalid.</exception>
        /// <remarks>
        /// <para>
        /// The properties on the associated POCO model must be decorated with one or more 
        /// form generation attribute(s) in order for anything to be generated. Undecorated
        /// properties on the associated POCO model are ignored during form generation.
        /// </para>
        /// </remarks>
        void Generate(
            RenderTreeBuilder builder,
            IHandleEvent eventTarget,
            object model
            );
    }
}
