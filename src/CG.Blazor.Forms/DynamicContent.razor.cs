
namespace CG.Blazor.Forms;

/// <summary>
/// This class is the code-behind for the <see cref="DynamicContent{T}"/>
/// razor component.
/// </summary>
/// <typeparam name="T">The type associated with the component.</typeparam>
public partial class DynamicContent<T>
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains any child content for the form.
    /// </summary>
    public RenderFragment ChildContent { get; set; }

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
    /// This constructor creates a new instance of the <see cref="DynamicContent{T}"/>
    /// class.
    /// </summary>
    public DynamicContent()
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
