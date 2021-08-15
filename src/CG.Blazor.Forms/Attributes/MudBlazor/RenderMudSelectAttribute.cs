using CG.Blazor.Forms.Attributes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be 
    /// rendered with a <see cref="MudSelect{T}"/> control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudSelectAttribute : FormGeneratorAttribute
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
        /// This property contains user class names, separated by space.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property indicates whether to show the clear button, or not.
        /// </summary>
        public bool Clearable { get; set; }

        /// <summary>
        /// This property contains the close select icon.
        /// </summary>
        public string CloseIcon { get; set; }

        /// <summary>
        /// This property, if true, causes compact vertical padding to be 
        /// applied to all Select items.
        /// </summary>
        public bool Dense { get; set; }

        /// <summary>
        /// This property sets the direction the Select menu should open.
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// This property, if true, the input will be disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// This property, if true, disables the under line for the input.
        /// </summary>
        public bool DisableUnderLine { get; set; }

        /// <summary>
        /// This property contains the conversion format parameter for ToString(), 
        /// which can be used for formatting primitive types, DateTimes and TimeSpans
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
        /// This property contains an optional label for the text field.
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
        /// This property indicates the maximum height the select can have when open.
        /// </summary>
        public int MaxHeight { get; set; }

        /// <summary>
        /// This property, if true, multiple values can be selected via checkboxes which 
        /// are automatically shown in the dropdown
        /// </summary>
        public bool MultiSelection { get; set; }

        /// <summary>
        /// This property, if true, the Select menu will open either before or after the 
        /// input (left/right).
        /// </summary>
        public bool OffsetX { get; set; }

        /// <summary>
        /// This property, if true, the Select menu will open either before or after the 
        /// input (top/bottom).
        /// </summary>
        public bool OffsetY { get; set; }

        /// <summary>
        /// This property contains the open select icon.
        /// </summary>
        public string OpenIcon { get; set; }

        /// <summary>
        /// This property contains a comma separated list of options, for the dropdown.
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// This property contains the pattern attribute, when specified, is a regular 
        /// expression which the input's value must match in order for the value to 
        /// pass constraint validation. It must be a valid JavaScript regular expression.
        /// Not Supported in multline input
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// This property, if true, the input will be read-only.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// This propert, ff true, the Select's input will not show any values that 
        /// are not defined in the dropdown. This can be useful if Value is bound 
        /// to a variable which is initialized to a value which is not in the list
        /// and you want the Select to show the label / placeholder instead.
        /// </summary>
        public bool Strict { get; set; }

        /// <summary>
        /// This property contains user styles, applied on top of the component's 
        /// own classes and styles
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// This property contains a use Tag to attach any user data object to the 
        /// component for your convenience.
        /// </summary>
        public object Tag { get; set; }
                
        /// This property contains any user attributes to use for the tabs panel.
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
        /// This constructor creates a new instance of the <see cref="RenderMudSelectAttribute"/>
        /// class.
        /// </summary>
        public RenderMudSelectAttribute()
        {
            // Set default values.
            Adornment = Adornment.End;
            AdornmentColor = Color.Default;
            AdornmentIcon = string.Empty;
            AdornmentText = string.Empty;
            AutoFocus = false;
            Class = string.Empty;
            Clearable = false;
            CloseIcon = string.Empty;
            Dense = false;
            Direction = Direction.Bottom;
            Disabled = false;
            DisableUnderLine = false;
            Format = string.Empty;
            FullWidth = false;
            IconSize = Size.Medium;
            Immediate = false;
            InputMode = InputMode.text;
            Label = string.Empty;
            Lines = 1;
            Margin = Margin.None;
            MaxHeight = 300;
            MultiSelection = false;
            OffsetX = false;
            OffsetY = false;
            OpenIcon = string.Empty;
            Options = string.Empty;
            Pattern = string.Empty;
            ReadOnly = false;
            Strict = false;
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
            if (Adornment.End != Adornment)
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
            if (false != Disabled)
            {
                // Add the property value.
                attr[nameof(Disabled)] = Disabled;
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
            if (300 != MaxHeight)
            {
                // Add the property value.
                attr[nameof(MaxHeight)] = MaxHeight;
            }

            // Does this property have a non-default value?
            if (false != MultiSelection)
            {
                // Add the property value.
                attr[nameof(MultiSelection)] = MultiSelection;
            }

            // Does this property have a non-default value?
            if (false != OffsetX)
            {
                // Add the property value.
                attr[nameof(OffsetX)] = OffsetX;
            }

            // Does this property have a non-default value?
            if (false != OffsetY)
            {
                // Add the property value.
                attr[nameof(OffsetY)] = OffsetY;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Pattern))
            {
                // Add the property value.
                attr[nameof(Pattern)] = Pattern;
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
            if (false != ReadOnly)
            {
                // Add the property value.
                attr[nameof(ReadOnly)] = ReadOnly;
            }

            // Does this property have a non-default value?
            if (false != Strict)
            {
                // Add the property value.
                attr[nameof(Strict)] = Strict;
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
