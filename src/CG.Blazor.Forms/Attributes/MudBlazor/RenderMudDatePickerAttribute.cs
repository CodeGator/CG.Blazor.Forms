using CG.Blazor.Forms.Attributes;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated class should be
    /// rendered using a <see cref="MudDatePicker"/> control.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Decorating a <see cref="DateTime"/> property with this attribute causes 
    /// the form generator to render the property as a <see cref="MudDatePicker"/> 
    /// component. 
    /// </para>
    /// <para>
    /// This attribute is only valid when placed on a property that returns a 
    /// <see cref="DateTime"/> value.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudDatePickerAttribute : FormGeneratorAttribute
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
        /// This property indicates the date format for the control.
        /// </summary>
        public string DateFormat { get; set; }

        /// <summary>
        /// This property indicates whether the control is disabled, or not.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// This property indicates whether the toolbar is disabled, or not.
        /// </summary>
        public bool DisableToolbar { get; set; }

        /// <summary>
        /// This property indicates how many months to display in the control.
        /// </summary>
        public int DisplayMonths { get; set; }

        /// <summary>
        /// This property indicates whether the control is editable, or not.
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// This property contains the elevation to use for the control.
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// This property contains an optional day on which to start the week.
        /// </summary>
        public DayOfWeek? FirstDayOfWeek { get; set; }

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
        /// This property contains an optional maximum date for the control.
        /// </summary>
        public DateTime? MaxDate { get; set; }

        /// <summary>
        /// This property contains an optional maximum number of months to show
        /// in a singlee row, on the control.
        /// </summary>
        public int? MaxMonthColumns { get; set; }

        /// <summary>
        /// This property contains an optional minimum date for the control.
        /// </summary>
        public DateTime? MinDate { get; set; }

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
        /// This property indicates whether the control should show weekly numbers, 
        /// or not.
        /// </summary>
        public bool ShowWeekNumbers { get; set; }

        /// <summary>
        /// This property indicates whether the control should show square corners, 
        /// or not.
        /// </summary>
        public bool Square { get; set; }

        /// <summary>
        /// This property contains an optional starting month date for the control.
        /// </summary>
        public DateTime? StartMonth { get; set; }

        /// <summary>
        /// This property indicates the CSS styles to use for the control.
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// This property contains the tag for the control.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// This property indicates the format to use for the selected date, in
        /// the title of the control.
        /// </summary>
        public string TitleDateFormat { get; set; }

        /// <summary>
        /// This property contains any CSS classes to use for the control's tool
        /// bar.
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
        /// This constructor creates a new instance of the <see cref="RenderMudDatePickerAttribute"/>
        /// class.
        /// </summary>
        public RenderMudDatePickerAttribute()
        {
            // Set default values.
            Adornment = Adornment.End;
            AdornmentColor = Color.Default;
            AdornmentIcon = string.Empty;
            AllowKeyboardInput = false;
            AutoClose = false;
            Class = string.Empty;
            ClassActions = string.Empty;
            ClosingDelay = 100;
            Color = Color.Primary;
            DateFormat = string.Empty;
            Disabled = false;
            DisableToolbar = false;
            DisplayMonths = 1;
            Editable = false;
            Elevation = 0;
            FirstDayOfWeek = null;
            IconSize = Size.Medium;
            Label = string.Empty;
            Margin = Margin.None;
            MaxDate = null;
            MaxMonthColumns = null;
            MinDate = null;
            OpenTo = OpenTo.Date;
            Orientation = Orientation.Portrait;
            PickerVariant = PickerVariant.Inline;
            ReadOnly = false;
            Required = false;
            Rounded = false;
            ShowWeekNumbers = false;
            Square = false;
            StartMonth = null;
            Style = string.Empty;
            Tag = null;
            TitleDateFormat = string.Empty;
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
            if (false != AutoClose)
            {
                // Add the property value.
                attr[nameof(AutoClose)] = AutoClose;
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
            if (false == string.IsNullOrEmpty(DateFormat))
            {
                // Add the property value.
                attr[nameof(DateFormat)] = DateFormat;
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
            if (1 != DisplayMonths)
            {
                // Add the property value.
                attr[nameof(DisplayMonths)] = DisplayMonths;
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
            if (null != FirstDayOfWeek)
            {
                // Add the property value.
                attr[nameof(FirstDayOfWeek)] = FirstDayOfWeek.Value;
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
            if (null != MaxDate)
            {
                // Add the property value.
                attr[nameof(MaxDate)] = MaxDate;
            }

            // Does this property have a non-default value?
            if (null != MaxMonthColumns)
            {
                // Add the property value.
                attr[nameof(MaxMonthColumns)] = MaxMonthColumns;
            }

            // Does this property have a non-default value?
            if (null != MinDate)
            {
                // Add the property value.
                attr[nameof(MinDate)] = MinDate;
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
            if (false != ShowWeekNumbers)
            {
                // Add the property value.
                attr[nameof(ShowWeekNumbers)] = ShowWeekNumbers;
            }

            // Does this property have a non-default value?
            if (false != Square)
            {
                // Add the property value.
                attr[nameof(Square)] = Square;
            }

            // Does this property have a non-default value?
            if (null != StartMonth)
            {
                // Add the property value.
                attr[nameof(StartMonth)] = StartMonth;
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
            if (false == string.IsNullOrEmpty(TitleDateFormat))
            {
                // Add the property value.
                attr[nameof(TitleDateFormat)] = TitleDateFormat;
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
