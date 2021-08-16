using CG.Blazor.Forms.Attributes;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be 
    /// rendered with a <see cref="MudSlider{T}"/> controls.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Decorating a numeric property with this attribute causes the form generator
    /// to render the property as a <see cref="MudSlider{T}"/> component. 
    /// </para>
    /// <para>
    /// This attribute is only valid when placed on a property that returns a 
    /// numeric value (in other words, an int, long, byte, float, double, or 
    /// decimal type).
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudSliderAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property indicates the CSS class to use for the switch.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property indicates what color to use for the switch.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// This property indicates whether the switch is disabled, or not.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// This property, if true, the dragging the slider will update the 
        /// Value immediately. If false, the Value is updated only on releasing 
        /// the handle.indicates whether to disable the ripple, or not.
        /// </summary>
        public bool Immediate { get; set; }

        /// <summary>
        /// This property contains an optional label for the text field.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// This property contains the maximum allowed value of the slider. 
        /// Should not be equal to min.
        /// </summary>
        public object Max { get; set; }

        /// <summary>
        /// This property contains the minimum allowed value of the slider. 
        /// Should not be equal to max.
        /// </summary>
        public object Min { get; set; }

        /// <summary>
        /// This property contains how many steps the slider should take 
        /// on each move.
        /// </summary>
        public object Step { get; set; }

        /// <summary>
        /// This property contains the CSS style for the switch.
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

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RenderMudSliderAttribute"/>
        /// class.
        /// </summary>
        public RenderMudSliderAttribute()
        {
            // Set default values.
            Class = string.Empty;
            Color = Color.Default;
            Disabled = false;
            Immediate = false;
            Label = string.Empty;
            Max = null;
            Min = null;
            Step = null;
            Style = string.Empty;
            Tag = null;
            UserAttributes = null;
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
            if (false == string.IsNullOrEmpty(Class))
            {
                // Add the property value.
                attr[nameof(Class)] = Class;
            }

            // Does this property have a non-default value?
            if (Color.Default != Color)
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
            if (false != Immediate)
            {
                // Add the property value.
                attr[nameof(Immediate)] = Immediate;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Label))
            {
                // Add the property value.
                attr[nameof(Label)] = Label;
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

            // Return the attributes.
            return attr;
        }

        #endregion
    }
}
