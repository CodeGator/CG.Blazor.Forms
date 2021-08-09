using CG.Blazor.Forms.Attributes;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be 
    /// rendered with a <see cref="MudRadioGroup{T}"/> control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudRadioGroupAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a comma separated list of options for the 
        /// radio buttons in the group.
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// This property contains a comma separated list of colors for the 
        /// radio buttons in the group.
        /// </summary>
        public string Colors { get; set; }

        /// <summary>
        /// This property indicates whether the radio buttons in the group 
        /// should be dense, or not.
        /// </summary>
        public bool Dense { get; set; }

        /// <summary>
        /// This property contains a comma separated list of disabled flags
        /// for the radio buttons in the group.
        /// </summary>
        public string Disabled { get; set; }

        /// <summary>
        /// This property indicates whether the radio buttons in the group 
        /// should disable the ripple, or not.
        /// </summary>
        public bool DisableRipple { get; set; }

        /// <summary>
        /// This property indicates the placement for the radio buttons in the 
        /// group.
        /// </summary>
        public Placement Placement { get; set; }

        /// <summary>
        /// This property indicates the size for the radio buttons in the group.
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// This property indicates the CSS style to use for the buttons in the
        /// group.
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// This property contains user attributes to be used for the buttons in 
        /// the group.
        /// </summary>
        public IDictionary<string, object> UserAttributes { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RenderMudRadioGroupAttribute"/>
        /// class.
        /// </summary>
        public RenderMudRadioGroupAttribute()
        {
            // Set default values.
            Options = string.Empty;
            Colors = string.Empty;
            Dense = false;
            Disabled = string.Empty;
            DisableRipple = false;
            Placement = Placement.End;
            Size = Size.Medium;
            Style = string.Empty;
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
            if (false == string.IsNullOrEmpty(Options))
            {
                // Add the property value.
                attr[nameof(Options)] = Options;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Colors))
            {
                // Add the property value.
                attr[nameof(Colors)] = Colors;
            }

            // Does this property have a non-default value?
            if (false != Dense)
            {
                // Add the property value.
                attr[nameof(Dense)] = Dense;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Disabled))
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
            if (Placement.End != Placement)
            {
                // Add the property value.
                attr[nameof(Placement)] = Placement;
            }

            // Does this property have a non-default value?
            if (Size.Medium != Size)
            {
                // Add the property value.
                attr[nameof(Size)] = Size;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Style))
            {
                // Add the property value.
                attr[nameof(Style)] = Style;
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
