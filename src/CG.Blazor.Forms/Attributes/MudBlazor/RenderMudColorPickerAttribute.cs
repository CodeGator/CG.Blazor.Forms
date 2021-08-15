using CG.Blazor.Forms.Attributes;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated class should be
    /// rendered using a <see cref="MudColorPicker"/> control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudColorPickerAttribute : FormGeneratorAttribute
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
        /// This property contains any CSS classes to use for the control.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property contains any CSS classes to use for the action buttons.
        /// </summary>
        public string ClassActions { get; set; }

        /// <summary>
        /// This property contains the color to use for the control.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// This property contains the inital mode (RGB, HSL or HEX) with which
        /// the picker should open. Defaults to RGB
        /// </summary>
        public ColorPickerMode ColorPickerMode { get; set; }

        /// <summary>
        /// This property contains the inital view of the picker. Views can 
        /// be changed if toolbar is enabled.
        /// </summary>
        public ColorPickerView ColorPickerView { get; set; }

        /// <summary>
        /// This property indicates whether alpha options will be displayed,
        /// or not.
        /// </summary>
        public bool DisableAlpha { get; set; }

        /// <summary>
        /// This property indicates whether the color field will be displayed,
        /// or not.
        /// </summary>
        public bool DisableColorField { get; set; }

        /// <summary>
        /// This property indicates whether the control is disabled, or not.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// This property indicates whether the drag effect is disabled, or not.
        /// </summary>
        public bool DisableDragEffect { get; set; }

        /// <summary>
        /// This property indicates whether the text field inputs and color mode
        /// switch will be displayed, or not.
        /// </summary>
        public bool DisableInputs { get; set; }

        /// <summary>
        /// This property indicates whether the switch to change color mode will 
        /// be displayed, or not.
        /// </summary>
        public bool DisableModeSwitch { get; set; }

        /// <summary>
        /// This property indicates whether the preview color box will be displayed, 
        /// or not. note that the preview color functions as a button as well for 
        /// collection colors.
        /// </summary>
        public bool DisablePreview { get; set; }

        /// <summary>
        /// This property indicates whether the sliders will be displayed, or not.
        /// </summary>
        public bool DisableSliders { get; set; }

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
        /// This property contains the orientiation for the control.
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// This property contains a comma separated list of colors for 
        /// the control's color pallette. 
        /// </summary>
        public string Palette { get; set; }

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
        /// This property contains user class names for picker's ToolBar, 
        /// separated by space
        /// </summary>
        public string ToolBarClass { get; set; }

        /// <summary>
        /// This property indicates whether binding changes occure also when 
        /// HSL values changed without a corresponding RGB change
        /// </summary>
        public bool UpdateBindingIfOnlyHSLChanged { get; set; }

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
        /// This constructor creates a new instance of the <see cref="RenderMudColorPickerAttribute"/>
        /// class.
        /// </summary>
        public RenderMudColorPickerAttribute()
        {
            // Set default values.
            Adornment = Adornment.End;
            AdornmentColor = Color.Default;
            AdornmentIcon = string.Empty;
            AllowKeyboardInput = false;
            Class = string.Empty;
            ClassActions = string.Empty;
            Color = Color.Primary;
            ColorPickerMode = ColorPickerMode.RGB;
            ColorPickerView = ColorPickerView.Spectrum;
            DisableAlpha = false;
            DisableColorField = false;
            Disabled = false;
            DisableDragEffect = false;
            DisableInputs = false;
            DisableModeSwitch = false;
            DisablePreview = false;
            DisableSliders = false;
            DisableToolbar = false;
            Editable = false;
            Elevation = 8;
            IconSize = Size.Medium;
            Label = string.Empty;
            Margin = Margin.None;
            Orientation = Orientation.Portrait;
            Palette = string.Empty;
            PickerVariant = PickerVariant.Inline;
            ReadOnly = false;
            Required = false;
            Rounded = false;
            Square = false;
            Style = string.Empty;
            Tag = null;
            ToolBarClass = string.Empty;
            UpdateBindingIfOnlyHSLChanged = false;
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
            if (false != AllowKeyboardInput)
            {
                // Add the property value.
                attr[nameof(AllowKeyboardInput)] = AllowKeyboardInput;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Class))
            {
                // Add the property value.
                attr[nameof(Class)] = Class;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(ClassActions))
            {
                // Add the property value.
                attr[nameof(ClassActions)] = ClassActions;
            }

            // Does this property have a non-default value?
            if (Color.Primary != Color)
            {
                // Add the property value.
                attr[nameof(Color)] = Color;
            }

            // Does this property have a non-default value?
            if (ColorPickerMode.RGB != ColorPickerMode)
            {
                // Add the property value.
                attr[nameof(ColorPickerMode)] = ColorPickerMode;
            }

            // Does this property have a non-default value?
            if (ColorPickerView.Spectrum != ColorPickerView)
            {
                // Add the property value.
                attr[nameof(ColorPickerView)] = ColorPickerView;
            }

            // Does this property have a non-default value?
            if (false != DisableAlpha)
            {
                // Add the property value.
                attr[nameof(DisableAlpha)] = DisableAlpha;
            }

            // Does this property have a non-default value?
            if (false != DisableColorField)
            {
                // Add the property value.
                attr[nameof(DisableColorField)] = DisableColorField;
            }

            // Does this property have a non-default value?
            if (false != Disabled)
            {
                // Add the property value.
                attr[nameof(Disabled)] = Disabled;
            }

            // Does this property have a non-default value?
            if (false != DisableDragEffect)
            {
                // Add the property value.
                attr[nameof(DisableDragEffect)] = DisableDragEffect;
            }

            // Does this property have a non-default value?
            if (false != DisableInputs)
            {
                // Add the property value.
                attr[nameof(DisableInputs)] = DisableInputs;
            }

            // Does this property have a non-default value?
            if (false != DisableModeSwitch)
            {
                // Add the property value.
                attr[nameof(DisableModeSwitch)] = DisableModeSwitch;
            }

            // Does this property have a non-default value?
            if (false != DisablePreview)
            {
                // Add the property value.
                attr[nameof(DisablePreview)] = DisablePreview;
            }

            // Does this property have a non-default value?
            if (false != DisableSliders)
            {
                // Add the property value.
                attr[nameof(DisableSliders)] = DisableSliders;
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
            if (8 != Elevation)
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
            if (Orientation.Portrait != Orientation)
            {
                // Add the property value.
                attr[nameof(Orientation)] = Orientation;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Palette))
            {
                // Add the property value.
                attr[nameof(Palette)] = Palette;
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
            if (false == string.IsNullOrEmpty(ToolBarClass))
            {
                // Add the property value.
                attr[nameof(ToolBarClass)] = ToolBarClass;
            }

            // Does this property have a non-default value?
            if (false != UpdateBindingIfOnlyHSLChanged)
            {
                // Add the property value.
                attr[nameof(UpdateBindingIfOnlyHSLChanged)] = UpdateBindingIfOnlyHSLChanged;
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
