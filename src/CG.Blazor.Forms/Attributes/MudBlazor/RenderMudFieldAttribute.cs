using CG.Blazor.Forms.Attributes;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be 
    /// rendered with a <see cref="MudField"/> control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudFieldAttribute : FormGeneratorAttribute
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
        /// This property contains user class names, separated by space
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property indicates whether the inpur element is disabled, or not.
        /// </summary>
        public bool Disabled { get; set; }

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
        public Size IconSize { get; set;  }

        /// <summary>
        /// This property indicates whether the the input will update the Value 
        /// immediately on typing. If false, the Value is updated only on Enter.
        /// </summary>
        public bool Immediate { get; set; }

        /// <summary>
        /// This property indicates the control should remove any inner padding.
        /// </summary>
        public bool InnerPadding { get; set; }

        /// <summary>
        /// This property contains the label text will be displayed in the input, 
        /// and scaled down at the top if the input has value.
        /// </summary>
        public string Label { get; set;  }

        /// <summary>
        /// This property indicates how much to change the vertical spacing. 
        /// </summary>
        public Margin Margin { get;set; }

        /// <summary>
        /// This property contains user styles, applied on top of the component's 
        /// own classes and styles
        /// </summary>
        public string Style { get;set; }

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
        /// This constructor creates a new instance of the <see cref="RenderMudFieldAttribute"/>
        /// class.
        /// </summary>
        public RenderMudFieldAttribute()
        {
            // Set default values.
            Adornment = Adornment.None;
            AdornmentIcon = string.Empty;
            AdornmentText = string.Empty;
            Class = string.Empty;
            Disabled = false;
            DisableUnderLine = false;
            Format = string.Empty;
            FullWidth = false;
            IconSize = Size.Medium;
            Immediate = false;
            InnerPadding = true;
            Label = string.Empty;
            Margin = Margin.None;
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
            if (false == string.IsNullOrEmpty(Class))
            {
                // Add the property value.
                attr[nameof(Class)] = Class;
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
            if (true != InnerPadding)
            {
                // Add the property value.
                attr[nameof(InnerPadding)] = InnerPadding;
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
