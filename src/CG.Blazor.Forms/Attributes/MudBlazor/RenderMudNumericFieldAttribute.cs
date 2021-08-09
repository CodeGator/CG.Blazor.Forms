using CG.Blazor.Forms.Attributes;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be 
    /// rendered with a <see cref="MudNumericField{T}"/> control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudNumericFieldAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property indicates the Start or End Adornment if not set to None.
        /// </summary>
        public Adornment Adornment { get; set; }

        /// <summary>
        /// This property indicates the color of the adornment if used. It supports 
        /// the theme colors.
        /// </summary>
        public Color AdornmentColor { get; set; }

        /// <summary>
        /// This property indicates the icon that will be used if Adornment is set 
        /// to Start or End.
        /// </summary>
        public string AdornmentIcon { get; set; }

        /// <summary>
        /// This property indicates the text that will be used if Adornment is set
        /// to Start or End, the Text overrides Icon.
        /// </summary>
        public string AdornmentText { get; set; }

        /// <summary>
        /// This property, If true, will force the input focus automatically
        /// </summary>
        public bool AutoFocus { get; set; }

        /// <summary>
        /// This property contains user class names, separated by space.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property indicates the interval to be awaited in milliseconds 
        /// before changing the Text value
        /// </summary>
        public int DebounceInterval { get; set; }

        /// <summary>
        /// This property, if true, the input element will be disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// This property, if true, the input will not have an underline.
        /// </summary>
        public bool DisableUnderLine { get; set; }

        /// <summary>
        /// This property contains a conversion format value for ToString(), 
        /// can be used for formatting primitive types, DateTimes and TimeSpans
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// This property, if true, will force the input to take up the full width 
        /// of its container.
        /// </summary>
        public bool FullWidth { get; set; }

        /// <summary>
        /// This property, if true, hides the spin buttons, the user can still change 
        /// value with keyboard arrows and manual update.
        /// </summary>
        public bool HideSpinButtons { get; set; }

        /// <summary>
        /// This property contains the icon size.
        /// </summary>
        public Size IconSize { get; set; }

        /// <summary>
        /// This property, If true, the input will update the Value immediately on typing.
        /// If false, the Value is updated only on Enter.
        /// </summary>
        public bool Immediate { get; set; }

        /// <summary>
        /// This property hints at the type of data that might be entered by the user while
        /// editing the input. Defaults to numeric
        /// </summary>
        public InputMode InputMode { get; set; }

        /// <summary>
        /// This property contains the label for the text field.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// This property indicates the number of lines to display.
        /// </summary>
        public int Lines { get; set; }

        /// <summary>
        /// This property, if true, will adjust the vertical spacing.
        /// </summary>
        public Margin Margin { get; set; }

        /// <summary>
        /// This property indicates the maximum value for the input.
        /// </summary>
        public object Max { get; set; }

        /// <summary>
        /// This property indicates the minumum value for the input.
        /// </summary>
        public object Min { get; set; }

        /// <summary>
        /// This property, when specified, is a regular expression which the input's 
        /// value must match in order for the value to pass constraint validation. It 
        /// must be a valid JavaScript regular expression Defaults to[0 - 9,\.\-+]* 
        /// To get a numerical keyboard on safari, use the pattern.The default pattern 
        /// should achieve numerical keyboard.
        /// </summary>
        public string Pattern { get; set; }

        /// <summary>
        /// This property contains a short hint displayed in the input before the user 
        /// enters a value.
        /// </summary>
        public string Placeholder { get; set; }

        /// <summary>
        /// This property indicates whether the text field is read only, or not.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// This property contains the increment added/subtracted by the spin buttons.
        /// </summary>
        public object Step { get; set; }

        /// <summary>
        /// This property contains the CSS style for the text field.
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// This property contains the tag for the tabs icon.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// This property contains any user attributes to use for the tabs panel.
        /// </summary>
        public IDictionary<string, object> UserAttributes { get; set; }

        /// <summary>
        /// This property contains the variant for the control.
        /// </summary>
        public Variant Variant { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RenderMudNumericFieldAttribute"/>
        /// class.
        /// </summary>
        public RenderMudNumericFieldAttribute()
        {
            // Set default values.
            Adornment = Adornment.None;
            AdornmentColor = Color.Default;
            AdornmentIcon = string.Empty;
            AdornmentText = string.Empty;
            AutoFocus = false;
            Class = string.Empty;
            DebounceInterval = 0;
            Disabled = false;
            DisableUnderLine = false;
            Format = string.Empty;
            FullWidth = false;
            HideSpinButtons = false;
            IconSize = Size.Medium;
            Immediate = false;
            InputMode = InputMode.numeric;
            Label = string.Empty;
            Lines = 1;
            Margin = Margin.None;
            Max = null;
            Min = null;
            Pattern = string.Empty;
            Placeholder = string.Empty;
            ReadOnly = false;
            Step = null;
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
            if (0 != DebounceInterval)
            {
                // Add the property value.
                attr[nameof(DebounceInterval)] = DebounceInterval;
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
            if (false != HideSpinButtons)
            {
                // Add the property value.
                attr[nameof(HideSpinButtons)] = HideSpinButtons;
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
            if (InputMode.numeric != InputMode)
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
            if (null != Max)
            {
                // Add the property value.
                attr[nameof(Max)] = Max;
            }

            // Does this property have a non-default value?
            if (null != Min)
            {
                // Add the property value.
                attr[nameof(Min)] = Min;
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
            if (null != Step)
            {
                // Add the property value.
                attr[nameof(Step)] = Step;
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
