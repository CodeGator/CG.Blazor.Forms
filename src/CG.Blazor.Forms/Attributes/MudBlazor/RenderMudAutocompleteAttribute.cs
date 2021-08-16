using CG.Blazor.Forms.Attributes;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be 
    /// rendered with a <see cref="MudAutocomplete{T}"/> control.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Decorating a string property with this attribute causes the form generator
    /// to render the property as a <see cref="MudAutocomplete{T}"/> component. 
    /// </para>
    /// <para>
    /// This attribute is only valid when placed on a property that returns a 
    /// string value.
    /// </para>
    /// <para>
    /// In order for the search operation to function, in the auto-complete 
    /// control, the name of a publicly accessable search function must be placed
    /// into the <see cref="RenderMudAutocompleteAttribute.SearchFunc"/> property.
    /// That search function can either live on the same object as the decorated
    /// property, or, it can live on the top-level model for the form, which can
    /// then be thought of as a kind of view-model.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudAutocompleteAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the Start or End Adornment if not set to None.
        /// </summary>
        public Adornment Adornment { get; set; }

        /// <summary>
        /// This property contains the color of the adornment if used. It 
        /// supports the theme colors.
        /// </summary>
        public Color AdornmentColor { get; set; }

        /// <summary>
        /// This property contains the Icon that will be used if Adornment 
        /// is set to Start or End.
        /// </summary>
        public string AdornmentIcon { get; set; }

        /// <summary>
        /// This property contains text that will be used if Adornment is set 
        /// to Start or End, the Text overrides Icon.
        /// </summary>
        public string AdornmentText { get; set; }

        /// <summary>
        /// This property indicates the input will focus automatically, when true.
        /// </summary>
        public bool AutoFocus { get; set; }

        /// <summary>
        /// This property contains user class names, separated by space
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property indicates whether to show the clear button, or not.
        /// </summary>
        public bool Clearable { get; set; }

        /// <summary>
        /// This property contains the Close Autocomplete Icon.
        /// </summary>
        public string CloseIcon { get; set; }

        /// <summary>
        /// This property appears on the drop-down close override Text with 
        /// selected Value. This makes it clear to the user which list value 
        /// is currently selected and disallows incomplete values in Text.
        /// </summary>
        public bool CoerceText { get; set; }

        /// <summary>
        /// This property indicates whether user input will be coerced, or not.
        /// </summary>
        public bool CoerceValue { get; set; }

        /// <summary>
        /// This property contains an interval to be awaited, in milliseconds, 
        /// before changing the Text value
        /// </summary>
        public double DebounceInterval { get; set; }

        /// <summary>
        /// This property indicates whether compact vertical padding will be 
        /// applied to all Autocomplete items, or not.
        /// </summary>
        public bool Dense { get; set; }

        /// <summary>
        /// This property indicates the direction the Autocomplete menu should 
        /// use, when opening.
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// This property indicates whether the input will have an underline,
        /// or not.
        /// </summary>
        public bool DisableUnderLine { get; set; }

        /// <summary>
        /// This property contains the conversion format parameter for ToString(), 
        /// can be used for formatting primitive types, DateTimes and TimeSpans
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// This property indicates whether the input will take up the full width 
        /// of its container, or not.
        /// </summary>
        public bool FullWidth { get; set; }

        /// <summary>
        /// This property indicates the icon size.
        /// </summary>
        public Size IconSize { get; set; }

        /// <summary>
        /// This property indicates whether the the input will update the Value 
        /// immediately on typing. If false, the Value is updated only on Enter.
        /// </summary>
        public bool Immediate { get; set; }

        /// <summary>
        /// This property hints at the type of data that might be entered by the 
        /// user while editing the input
        /// </summary>
        public InputMode InputMode { get; set; }

        /// <summary>
        /// This property contains the label text will be displayed in the input, 
        /// and scaled down at the top if the input has value.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// This property contains the number of lines that the input will display.
        /// </summary>
        public int Lines { get; set; }

        /// <summary>
        /// This property indicates how much to change the vertical spacing. 
        /// </summary>
        public Margin Margin { get; set; }

        /// <summary>
        /// This property indicates how may items to display. 
        /// </summary>
        public int? MaxItems { get; set; }

        /// <summary>
        /// This property indicates the minimal number of character required to
        /// initiate a search.
        /// </summary>
        public int MinCharacters { get; set; }

        /// <summary>
        /// This property indicates whether the Autocomplete menu opens before 
        /// or after the input (left/right).
        /// </summary>
        public bool OffsetX { get; set; }

        /// <summary>
        /// This property indicates whether the Autocomplete menu opens before 
        /// or after the input (top/bottom).
        /// </summary>
        public bool OffsetY { get; set; }

        /// <summary>
        /// This property contains the open icon.
        /// </summary>
        public string OpenIcon { get; set; }

        /// <summary>
        /// This property contains a regular expression which the input's value 
        /// must match in order for the value to pass constraint validation. It 
        /// must be a valid JavaScript regular expression Not Supported in multline 
        /// input
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// This property contains the short hint displayed in the input before
        /// the user enters a value.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// This property indicates whether the input will be read-only, or not.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// This property indicates whether the control will reset Value if user 
        /// deletes the text
        /// </summary>
        public bool ResetValueOnEmptyText { get; set; }

        /// <summary>
        /// This property contains the name of an optional search function, on 
        /// the associated model.
        /// </summary>
        public string SearchFunc { get; set; }

        /// <summary>
        /// This property indicates whether the currently selected item from the
        /// drop-down (if it is open) will be selected on a tab, or not.
        /// </summary>
        public bool SelectValueOnTab { get; set; }

        /// <summary>
        /// This property contains user styles, applied on top of the component's 
        /// own classes and styles
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// This property contain a tag to attach any user data object to the component 
        /// for your convenience.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// This property contains attributes you add to the component that don't match 
        /// any of its parameters. They will be splatted onto the underlying HTML tag.
        /// </summary>
        public IDictionary<string, object> UserAttributes { get; set; }

        /// <summary>
        /// This property contains the variant to use with the control.
        /// </summary>
        public Variant Variant { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RenderMudAutocompleteAttribute"/>
        /// class.
        /// </summary>
        public RenderMudAutocompleteAttribute()
        {
            // Set default values.
            Adornment = Adornment.None;
            AdornmentColor = Color.Default;
            AdornmentIcon = string.Empty;
            AdornmentText = string.Empty;
            AutoFocus = false;
            Class = string.Empty;
            Clearable = false;
            CloseIcon = string.Empty;
            CoerceText = true;
            CoerceValue = false;
            DebounceInterval = 100;
            Dense = false;
            Direction = Direction.Bottom;
            DisableUnderLine = false;
            Format = string.Empty;
            FullWidth = false;
            IconSize = Size.Medium;
            Immediate = false;
            InputMode = InputMode.text;
            Label = string.Empty;
            Lines = 1;
            Margin = Margin.None;
            MaxItems = null;
            MinCharacters = 0;
            OffsetX = false;
            OffsetY = true;
            OpenIcon = string.Empty;
            Pattern = string.Empty;
            Placeholder = string.Empty;
            ReadOnly = false;
            ResetValueOnEmptyText = false;
            SearchFunc = string.Empty;
            SelectValueOnTab = false;
            Style = string.Empty;
            Tag = null;
            UserAttributes = null;
            Variant = Variant.Text;
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
            var attr = new Dictionary<string, object>();

            // Does this property have a non-default value?
            if (Adornment.None != Adornment)
            {
                // Add the property value.
                attr[nameof(Adornment)] = Adornment;
            }

            // Does this property have a non-default value?
            if (Color.Default != AdornmentColor)
            {
                // Add the property value.
                attr[nameof(AdornmentColor)] = AdornmentColor;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(AdornmentIcon))
            {
                // Add the property value.
                attr[nameof(AdornmentIcon)] = AdornmentIcon;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(AdornmentText))
            {
                // Add the property value.
                attr[nameof(AdornmentText)] = AdornmentText;
            }

            // Does this property have a non-default value?
            if (false != AutoFocus)
            {
                // Add the property value.
                attr[nameof(AutoFocus)] = AutoFocus;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Class))
            {
                // Add the property value.
                attr[nameof(Class)] = Class;
            }

            // Does this property have a non-default value?
            if (false != Clearable)
            {
                // Add the property value.
                attr[nameof(Clearable)] = Clearable;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(CloseIcon))
            {
                // Add the property value.
                attr[nameof(CloseIcon)] = CloseIcon;
            }

            // Does this property have a non-default value?
            if (true != CoerceText)
            {
                // Add the property value.
                attr[nameof(CoerceText)] = CoerceText;
            }

            // Does this property have a non-default value?
            if (false != CoerceValue)
            {
                // Add the property value.
                attr[nameof(CoerceValue)] = CoerceValue;
            }

            // Does this property have a non-default value?
            if (100 != DebounceInterval)
            {
                // Add the property value.
                attr[nameof(DebounceInterval)] = DebounceInterval;
            }

            // Does this property have a non-default value?
            if (false != Dense)
            {
                // Add the property value.
                attr[nameof(Dense)] = Dense;
            }

            // Does this property have a non-default value?
            if (Direction.Bottom != Direction)
            {
                // Add the property value.
                attr[nameof(Direction)] = Direction;
            }

            // Does this property have a non-default value?
            if (false != DisableUnderLine)
            {
                // Add the property value.
                attr[nameof(DisableUnderLine)] = DisableUnderLine;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Format))
            {
                // Add the property value.
                attr[nameof(Format)] = Format;
            }

            // Does this property have a non-default value?
            if (false != FullWidth)
            {
                // Add the property value.
                attr[nameof(FullWidth)] = FullWidth;
            }
            
            // Does this property have a non-default value?
            if (Size.Medium != IconSize)
            {
                // Add the property value.
                attr[nameof(IconSize)] = IconSize;
            }

            // Does this property have a non-default value?
            if (false != Immediate)
            {
                // Add the property value.
                attr[nameof(Immediate)] = Immediate;
            }

            // Does this property have a non-default value?
            if (InputMode.text != InputMode)
            {
                // Add the property value.
                attr[nameof(InputMode)] = InputMode;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Label))
            {
                // Add the property value.
                attr[nameof(Label)] = Label;
            }

            // Does this property have a non-default value?
            if (1 != Lines)
            {
                // Add the property value.
                attr[nameof(Lines)] = Lines;
            }

            // Does this property have a non-default value?
            if (Margin.None != Margin)
            {
                // Add the property value.
                attr[nameof(Margin)] = Margin;
            }

            // Does this property have a non-default value?
            if (null != MaxItems)
            {
                // Add the property value.
                attr[nameof(MaxItems)] = MaxItems.Value;
            }

            // Does this property have a non-default value?
            if (0 != MinCharacters)
            {
                // Add the property value.
                attr[nameof(MinCharacters)] = MinCharacters;
            }

            // Does this property have a non-default value?
            if (false != OffsetX)
            {
                // Add the property value.
                attr[nameof(OffsetX)] = OffsetX;
            }

            // Does this property have a non-default value?
            if (true != OffsetY)
            {
                // Add the property value.
                attr[nameof(OffsetY)] = OffsetY;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(OpenIcon))
            {
                // Add the property value.
                attr[nameof(OpenIcon)] = OpenIcon;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Pattern))
            {
                // Add the property value.
                attr[nameof(Pattern)] = Pattern;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Placeholder))
            {
                // Add the property value.
                attr[nameof(Placeholder)] = Placeholder;
            }

            // Does this property have a non-default value?
            if (false != ReadOnly)
            {
                // Add the property value.
                attr[nameof(ReadOnly)] = ReadOnly;
            }

            // Does this property have a non-default value?
            if (false != ResetValueOnEmptyText)
            {
                // Add the property value.
                attr[nameof(ResetValueOnEmptyText)] = ResetValueOnEmptyText;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(SearchFunc))
            {
                // Add the property value.
                attr[nameof(SearchFunc)] = SearchFunc;
            }

            // Does this property have a non-default value?
            if (false != SelectValueOnTab)
            {
                // Add the property value.
                attr[nameof(SelectValueOnTab)] = SelectValueOnTab;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Style))
            {
                // Add the property value.
                attr[nameof(Style)] = Style;
            }
            
            // Does this property have a non-default value?
            if (null != Tag)
            {
                // Add the property value.
                attr[nameof(Tag)] = Tag;
            }

            // Does this property have a non-default value?
            if (null != UserAttributes)
            {
                // Add the property value.
                attr[nameof(UserAttributes)] = UserAttributes;
            }

            // Does this property have a non-default value?
            if (Variant.Text != Variant)
            {
                // Add the property value.
                attr[nameof(Variant)] = Variant;
            }

            // Return the attributes.
            return attr;
        }

        #endregion
    }
}
