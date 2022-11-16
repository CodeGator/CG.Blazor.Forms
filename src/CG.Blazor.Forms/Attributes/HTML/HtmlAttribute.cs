
namespace CG.Blazor.Forms.Attributes;

/// <summary>
/// This class is a base for all HTML specific form generation attributes.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public abstract class HtmlAttribute : FormGeneratorAttribute
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property indicates the component should automatically get focus 
    /// when the page loads
    /// </summary>
    public bool AutoFocus { get; set; }

    /// <summary>
    /// This property contains user class names, separated by spaces.
    /// </summary>
    public string Class { get; set; }

    /// <summary>
    /// This property contains the identifier for the element.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// This property indicates the type of virtual keyboard configuration 
    /// to use when editing this element or its contents. Values include 
    /// none, text, tel, url, email, numeric, decimal, and search.
    /// </summary>
    public string InputMode { get; set; }

    /// <summary>
    /// This property contains the label that will be displayed for the
    /// element.
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// This property contains the for the input control. 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// This property contains user styles, applied on top of the element's 
    /// own classes and styles
    /// element.
    /// </summary>
    public string Style { get; set; }

    /// <summary>
    /// This property contains an integer attribute indicating if the element 
    /// can take input focus (is focusable), if it should participate to sequential 
    /// keyboard navigation. As all input types except for input of type hidden 
    /// are focusable, this attribute should not be used on form controls, 
    /// because doing so would require the management of the focus order for 
    /// all elements within the document with the risk of harming usability 
    /// and accessibility if done incorrectly.
    /// </summary>
    public string TabIndex { get; set; }

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of <see cref="HtmlAttribute"/>
    /// class.
    /// </summary>
    protected HtmlAttribute()
    {
        // Set default values.
        AutoFocus = false;
        Class = string.Empty;
        Id = string.Empty;
        Name = string.Empty;
        InputMode = string.Empty;   
        Label = string.Empty;
        Style = string.Empty;
        TabIndex = string.Empty;    
    }

    #endregion

    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <inheritdoc/>
    public override IDictionary<string, object> ToAttributes()
    {
        // Create a table to hold the attributes.
        var attr = base.ToAttributes();

        // Does this property have a non-default value?
        if (false != AutoFocus)
        {
            // Add the property value.
            attr[nameof(AutoFocus)] = AutoFocus;
        }

        // Does this property have a non-default value?
        if (false == string.IsNullOrEmpty(Class))
        {
            // Get the existing CSS class(es)
            var @class = Class;

            // Has the form-control class not been applied?
            if (false == @class.Contains("form-control"))
            {
                // Apply the form-control class.
                attr[nameof(Class)] = @class + " form-control";
            }
        }
        else
        {
            // Ensure we set the form-control class.
            attr[nameof(Class)] = "form-control";
        }

        // Does this property have a non-default value?
        if (false == string.IsNullOrEmpty(Id))
        {
            // Add the property value.
            attr[nameof(Id)] = Id;
        }

        // Does this property have a non-default value?
        if (false == string.IsNullOrEmpty(InputMode))
        {
            // Add the property value.
            attr[nameof(InputMode)] = InputMode;
        }

        // Does this property have a non-default value?
        if (false == string.IsNullOrEmpty(Name))
        {
            // Add the property value.
            attr[nameof(Name)] = Name;
        }

        // Does this property have a non-default value?
        if (false == string.IsNullOrEmpty(Style))
        {
            // Add the property value.
            attr[nameof(Style)] = Style;
        }

        // Does this property have a non-default value?
        if (false == string.IsNullOrEmpty(TabIndex))
        {
            // Add the property value.
            attr[nameof(TabIndex)] = TabIndex;
        }

        // Return the attributes.
        return attr;
    }

    #endregion
}
