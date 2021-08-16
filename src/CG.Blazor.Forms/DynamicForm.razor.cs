using CG.Blazor.Forms.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using CG.Blazor.Forms.Attributes;

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
        /// This parameter indicates whether the model and it's properties
        /// should be rendered inside a form, or not. True to render just 
        /// the model; False to render the model inside a form.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Setting this value to True prevents the form generator from wrapping
        /// the properties of the <see cref="DynamicForm{T}.Model"/> object in
        /// an <see cref="EditForm"/> component. That, in turn, prevents any
        /// of the form related callbacks from ever getting raised, since there
        /// isn't a form to raise them.
        /// </para>
        /// <para>
        /// This property is good to set if you need a form with dynamic content, 
        /// but, you want to handle the <see cref="EditForm"/> part yourself.
        /// </para>
        /// </remarks>
        [Parameter]
        public bool NoForm { get; set; }

        /// <summary>
        /// This parameter contains a callback for the OnInvalidSubmit event.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This callback is optional. It will be called whenever the form
        /// is submitted with one or more invalid form values. As such,
        /// it will only be called if the <see cref="DynamicForm{T}.NoForm"/> 
        /// property is set to false, and the <see cref="DynamicForm{T}.Model"/> 
        /// property has been set to a valid POCO reference, and the model's 
        /// class has been decorated with a <see cref="DataAnnotationsValidatorAttribute"/> 
        /// attribute (or equivilant), and at least one or more properties 
        /// on the model have a value (or values) that fail form validation.
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
        /// otherwise pass validation, or not. As such, it will only be called 
        /// if the <see cref="DynamicForm.NoForm"/> property is set to false, 
        /// and the <see cref="DynamicForm.Model"/>  property has been set to a 
        /// valid POCO reference.
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
        /// is submitted with valid form values. As such, it will only be 
        /// called if the <see cref="DynamicForm.NoForm"/> property is set to 
        /// false, and the <see cref="DynamicForm.Model"/>  property has been 
        /// set to a valid POCO reference, and the model's  class has been 
        /// decorated with a <see cref="DataAnnotationsValidatorAttribute"/> 
        /// attribute (or equivilant).
        /// </para>
        /// </remarks>
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
