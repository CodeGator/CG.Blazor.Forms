using CG.Blazor.Forms.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace CG.Blazor.Forms
{
    /// <summary>
    /// This class is the code-behind for the <see cref="DynamicForm{T}"/>
    /// razor component.
    /// </summary>
    /// <typeparam name="T">The type associated with the component.</typeparam>
    public partial class DynamicForm<T>
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a form generator service.
        /// </summary>
        [Inject]
        private IFormGenerator FormGenerator { get; set; }

        /// <summary>
        /// This parameter contains a reference to the data model for the form.
        /// </summary>
        [Parameter]
        public T Model { get; set; }

        /// <summary>
        /// This parameter indicates whether the model and it's properties
        /// should be rendered inside a form, or not. True to render just 
        /// the model; False to render the model inside a form.
        /// </summary>
        [Parameter]
        public bool NoForm { get; set; }

        /// <summary>
        /// This parameter contains a callback for the OnInvalidSubmit event.
        /// </summary>
        [Parameter]
        public EventCallback<EditContext> OnInvalidSubmit { get; set; }

        /// <summary>
        /// This parameter contains a callback for the OnSubmit event.
        /// </summary>
        [Parameter]
        public EventCallback<EditContext> OnSubmit { get; set; }

        /// <summary>
        /// This parameter contains a callback for the OnValidSubmit event.
        /// </summary>
        [Parameter]
        public EventCallback<EditContext> OnValidSubmit { get; set; }

        #endregion

        // *******************************************************************
        // Private methods.
        // *******************************************************************

        #region Private methods

        /// <summary>
        /// This method generates the form using the Model as input.
        /// </summary>
        /// <returns>A <see cref="RenderFragment"/> containing the form.</returns>
        /// <exception cref="FormGenerationException">This exception is thrown
        /// whenever the optiona fails, for any reason.</exception>
        private RenderFragment GenerateFormBody() => builder => FormGenerator.Generate(
            builder,
            (IHandleEvent)this,
            Model
            );

        #endregion
    }
}
