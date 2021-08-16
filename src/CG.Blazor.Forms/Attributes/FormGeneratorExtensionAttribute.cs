using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is the base for all form generation extension attributes.
    /// </summary>
    public abstract class FormGeneratorExtensionAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method is called by the form generator to extend the form
        /// rendering process to include custom content, from the attribute.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="viewModel">The view-model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public abstract int OnRender(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object viewModel
            );

        #endregion
    }
}
