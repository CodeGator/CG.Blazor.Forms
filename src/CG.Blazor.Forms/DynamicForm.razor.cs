using CG.Blazor.Forms.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

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
        /// This property contains any child content for the form.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// This property contains any custom button(s) for the form.
        /// </summary>
        [Parameter]
        public RenderFragment ButtonContent { get; set; }

        /// <summary>
        /// This property indicates whether the child content should be rendered
        /// before, or after the generated content. True to render the child
        /// content before the rendered content; false to render the child
        /// content after the rendered content.
        /// </summary>
        [Parameter]
        public bool ChildContentAfter { get; set; }

        /// <summary>
        /// This property contains a form generator service.
        /// </summary>
        [Inject]
        private IFormGenerator FormGenerator { get; set; }

        /// <summary>
        /// This parameter contains a reference to the data model for the form.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Any type can be used as a model, for form generation, but it was
        /// designed to work with a simple, to moderately complex POCO class,
        /// containing properties that return either primitive types, or, 
        /// other POCO references.
        /// </para>
        /// </remarks>
        [Parameter]
        public T Model { get; set; }

        /// <summary>
        /// This parameter contains a callback for the OnInvalidSubmit event.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This callback is optional. It will be called whenever the form
        /// is submitted by the user, with one or more invalid form values. 
        /// </para>
        /// </remarks>
        [Parameter]
        public EventCallback<EditContext> OnInvalidSubmit { get; set; }

        /// <summary>
        /// This parameter contains a callback for the OnSubmit event.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This callback is optional. It will be called whenever the form
        /// is submitted by the user, whether or not the form's values would
        /// otherwise pass validation, or not. 
        /// </para>
        /// </remarks>
        [Parameter]
        public EventCallback<EditContext> OnSubmit { get; set; }

        /// <summary>
        /// This parameter contains a callback for the OnValidSubmit event.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This callback is optional. It will be called whenever the form
        /// is submitted by the user, with valid form values. 
        /// </para>
        /// </remarks>
        [Parameter]
        public EventCallback<EditContext> OnValidSubmit { get; set; }

        /// <summary>
        /// This parameter indicates whether the form should display a reset 
        /// button, or not. True to show; False otherwise.
        /// </summary>
        [Parameter]
        public bool ShowResetButton { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="DynamicForm{T}"/>
        /// class.
        /// </summary>
        public DynamicForm()
        {
            // Set default values.
            ShowResetButton = false;
        }

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
