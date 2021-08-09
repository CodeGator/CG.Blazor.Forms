using CG.Blazor.Forms.Attributes;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated class should be
    /// rendered using a <see cref="MudTimePicker"/> control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudTimePickerAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property indicates the position for the control.
        /// </summary>
        public Adornment Adornment { get; set; }

        /// <summary>
        /// This property indicates the color for the control.
        /// </summary>
        public Color AdornmentColor { get; set; }

        /// <summary>
        /// This property indicates the icon for the control.
        /// </summary>
        public string AdornmentIcon { get; set; }

        /// <summary>
        /// This property indicates whether the control accepts keyboard 
        /// input, or not.
        /// </summary>
        public bool AllowKeyboardInput { get; set; }

        /// <summary>
        /// This property indicates whether the control shows a 12 or 24 hour day.
        /// </summary>
        public bool AmPm { get; set; }

        /// <summary>
        /// This property indicates whether the control should close 
        /// automatically, or not.
        /// </summary>
        public bool AutoClose { get; set; }

        /// <summary>
        /// This property contains any CSS classes to use for the control.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property contains any CSS classes to use for the action buttons.
        /// </summary>
        public string ClassActions { get; set; }

        /// <summary>
        /// This property indicates the closing delay for the control.
        /// </summary>
        public int ClosingDelay { get; set; }

        /// <summary>
        /// This property contains the color to use for the control.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// This property indicates whether the control is disabled, or not.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// This property indicates whether the toolbar is disabled, or not.
        /// </summary>
        public bool DisableToolbar { get; set; }

        /// <summary>
        /// This property indicates whether the control is editable, or not.
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// This property contains the elevation to use for the control.
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// This property indicates the icon size to use with the control.
        /// </summary>
        public Size IconSize { get; set; }

        /// <summary>
        /// This property contains a label for the control.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// This property contains the margin for the control.
        /// </summary>
        public Margin Margin { get; set; }

        /// <summary>
        /// This property contains the first view to show in the control.
        /// </summary>
        public OpenTo OpenTo { get; set; }

        /// <summary>
        /// This property contains the orientiation for the control.
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// This property contains the control container variant.
        /// </summary>
        public PickerVariant PickerVariant { get; set; }

        /// <summary>
        /// This property indicates whether the control is read only, or not.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// This property indicates whether the control is required, or not.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// This property indicates whether the control should have rounded corners, 
        /// or not.
        /// </summary>
        public bool Rounded { get; set; }

        /// <summary>
        /// This property indicates whether the control should show square corners, 
        /// or not.
        /// </summary>
        public bool Square { get; set; }

        /// <summary>
        /// This property indicates the CSS styles to use for the control.
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// This property contains the tag for the control.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// This property contains the edition mode. By default, you can 
        /// edit hours and minutes.
        /// </summary>
        public TimeEditMode TimeEditMode { get; set; }  

        /// <summary>
        /// This property contains the string format for the selected time.
        /// </summary>
        public string TimeFormat {  get; set; }

        /// <summary>
        /// This property contains user class names for picker's ToolBar, 
        /// separated by space
        /// </summary>
        public string ToolBarClass { get; set; }

        /// <summary>
        /// This property contains any user attributes to use for the control.
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
        /// This constructor creates a new instance of the <see cref="RenderMudTimePickerAttribute"/>
        /// class.
        /// </summary>
        public RenderMudTimePickerAttribute()
        {
            // Set default values.
            Adornment = Adornment.End;
            AdornmentColor = Color.Default;
            AdornmentIcon = string.Empty;
            AllowKeyboardInput = false;
            AmPm = false;
            AutoClose = false;
            Class = string.Empty;
            ClassActions = string.Empty;
            ClosingDelay = 100;
            Color = Color.Primary;
            Disabled = false;
            DisableToolbar = false;
            Editable = false;
            Elevation = 0;
            IconSize = Size.Medium;
            Label = string.Empty;
            Margin = Margin.None;
            OpenTo = OpenTo.Hours;
            Orientation = Orientation.Portrait;
            PickerVariant = PickerVariant.Inline;
            ReadOnly = false;
            Required = false;
            Rounded = false;
            Square = false;
            Style = string.Empty;
            Tag = null;
            TimeEditMode = TimeEditMode.Normal;
            TimeFormat = string.Empty;
            ToolBarClass = string.Empty;
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
            if (false != string.IsNullOrEmpty(AdornmentIcon))
            {
                // Add the property value.
                attr[nameof(AdornmentIcon)] = AdornmentIcon;
            }

            // Does this property have a non-default value?
            if (false != AllowKeyboardInput)
            {
                // Add the property value.
                attr[nameof(AllowKeyboardInput)] = AllowKeyboardInput;
            }

            // Does this property have a non-default value?
            if (false != AmPm)
            {
                // Add the property value.
                attr[nameof(AmPm)] = AmPm;
            }

            // Does this property have a non-default value?
            if (false != AutoClose)
            {
                // Add the property value.
                attr[nameof(AutoClose)] = AutoClose;
            }

            // Does this property have a non-default value?
            if (false != string.IsNullOrEmpty(Class))
            {
                // Add the property value.
                attr[nameof(Class)] = Class;
            }

            // Does this property have a non-default value?
            if (false != string.IsNullOrEmpty(ClassActions))
            {
                // Add the property value.
                attr[nameof(ClassActions)] = ClassActions;
            }

            // Does this property have a non-default value?
            if (100 != ClosingDelay)
            {
                // Add the property value.
                attr[nameof(ClosingDelay)] = ClosingDelay;
            }

            // Does this property have a non-default value?
            if (Color.Primary != Color)
            {
                // Add the property value.
                attr[nameof(Color)] = Color;
            }

            // Does this property have a non-default value?
            if (false != Disabled)
            {
                // Add the property value.
                attr[nameof(Disabled)] = Disabled;
            }

            // Does this property have a non-default value?
            if (false != DisableToolbar)
            {
                // Add the property value.
                attr[nameof(DisableToolbar)] = DisableToolbar;
            }

            // Does this property have a non-default value?
            if (false != Editable)
            {
                // Add the property value.
                attr[nameof(Editable)] = Editable;
            }

            // Does this property have a non-default value?
            if (0 != Elevation)
            {
                // Add the property value.
                attr[nameof(Elevation)] = Elevation;
            }

            // Does this property have a non-default value?
            if (Size.Medium != IconSize)
            {
                // Add the property value.
                attr[nameof(IconSize)] = IconSize;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Label))
            {
                // Add the property value.
                attr[nameof(Label)] = Label;
            }

            // Does this property have a non-default value?
            if (Margin.None != Margin)
            {
                // Add the property value.
                attr[nameof(Margin)] = Margin;
            }

            // Does this property have a non-default value?
            if (OpenTo.Date != OpenTo)
            {
                // Add the property value.
                attr[nameof(OpenTo)] = OpenTo;
            }

            // Does this property have a non-default value?
            if (Orientation.Portrait != Orientation)
            {
                // Add the property value.
                attr[nameof(Orientation)] = Orientation;
            }

            // Does this property have a non-default value?
            if (PickerVariant.Inline != PickerVariant)
            {
                // Add the property value.
                attr[nameof(PickerVariant)] = PickerVariant;
            }

            // Does this property have a non-default value?
            if (false != ReadOnly)
            {
                // Add the property value.
                attr[nameof(ReadOnly)] = ReadOnly;
            }

            // Does this property have a non-default value?
            if (false != Required)
            {
                // Add the property value.
                attr[nameof(Required)] = Required;
            }

            // Does this property have a non-default value?
            if (false != Rounded)
            {
                // Add the property value.
                attr[nameof(Rounded)] = Rounded;
            }

            // Does this property have a non-default value?
            if (false != Square)
            {
                // Add the property value.
                attr[nameof(Square)] = Square;
            }

            // Does this property have a non-default value?
            if (false != string.IsNullOrEmpty(Style))
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
            if (TimeEditMode.Normal != TimeEditMode)
            {
                // Add the property value.
                attr[nameof(TimeEditMode)] = TimeEditMode;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(TimeFormat))
            {
                // Add the property value.
                attr[nameof(TimeFormat)] = TimeFormat;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(ToolBarClass))
            {
                // Add the property value.
                attr[nameof(ToolBarClass)] = ToolBarClass;
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
