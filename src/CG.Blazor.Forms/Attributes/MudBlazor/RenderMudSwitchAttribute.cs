using CG.Blazor.Forms.Attributes;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be 
    /// rendered with a <see cref="MudSwitch{T}"/> controls.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Decorating a bool property with this attribute causes the form generator
    /// to render the property as a <see cref="MudSwitch{T}"/> component. 
    /// </para>
    /// <para>
    /// This attribute is only valid when placed on a property that returns a 
    /// bool value.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudSwitchAttribute : FormGeneratorAttribute
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
        /// This property indicates whether to disable the ripple, or not.
        /// </summary>
        public bool DisableRipple { get; set; }

        /// <summary>
        /// This property contains the label for the switch.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// This property indicates whether the switch is read only, or not.
        /// </summary>
        public bool ReadOnly { get; set; }

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
        /// This constructor creates a new instance of the <see cref="RenderMudSwitchAttribute"/>
        /// class.
        /// </summary>
        public RenderMudSwitchAttribute()
        {
            // Set default values.
            Class = string.Empty;
            Color = Color.Default;
            Disabled = false;
            DisableRipple = false;
            Label = string.Empty;
            ReadOnly = false;
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
            if (false != DisableRipple)
            {
                // Add the property value.
                attr[nameof(DisableRipple)] = DisableRipple;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Label))
            {
                // Add the property value.
                attr[nameof(Label)] = Label;
            }

            // Does this property have a non-default value?
            if (false != ReadOnly)
            {
                // Add the property value.
                attr[nameof(ReadOnly)] = ReadOnly;
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
