using CG.Blazor.Forms.Attributes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be 
    /// rendered with a <see cref="MudCheckBox{T}"/> control.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Decorating a bool property with this attribute causes the form generator
    /// to render the property as a <see cref="MudCheckBox{T}"/> component. 
    /// </para>
    /// <para>
    /// This attribute is only valid when placed on a property that returns a 
    /// bool value.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudCheckBoxAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a custom checked icon, leave null for default.
        /// </summary>
        public string CheckedIcon { get; set;  }

        /// <summary>
        /// This property contains user class names, separated by space.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property indicates the color of the component. It supports 
        /// the theme colors.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// This proerty, if true, causes compact padding to be applied.
        /// </summary>
        public bool Dense { get; set; }

        /// <summary>
        /// This property, if true, the input will be disabled.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// This property, if true, disables ripple effect.
        /// </summary>
        public bool DisableRipple { get; set; }

        /// <summary>
        /// This property contains a custom indeterminate icon, leave 
        /// null for default.
        /// </summary>
        public string IndeterminateIcon { get; set; }

        /// <summary>
        /// This property contains an optional label for the text field.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// This property, if true, the input will be read-only.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// This property cotnains the size of the component.
        /// </summary>
        public Size Size { get; set; }

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

        /// <summary>
        /// This property indicates if the checkbox can cycle again through 
        /// indeterminate status.
        /// </summary>
        public bool TriState { get; set; }

        /// <summary>
        /// This property contains a custom unchecked icon, leave null for default.
        /// </summary>
        public string UncheckedIcon { get; set; }

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
        /// This constructor creates a new instance of the <see cref="RenderMudCheckBoxAttribute"/>
        /// class.
        /// </summary>
        public RenderMudCheckBoxAttribute()
        {
            // Set default values.
            CheckedIcon = string.Empty;
            Class = string.Empty;
            Color = Color.Default;
            Dense = false;
            Disabled = false;
            DisableRipple = false;
            IndeterminateIcon = string.Empty;
            Label = string.Empty;
            ReadOnly = false;
            Size = Size.Medium;
            Style = string.Empty;
            Tag = null;
            TriState = false;
            UncheckedIcon = string.Empty;
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
            if (false == string.IsNullOrEmpty(CheckedIcon))
            {
                // Add the property value.
                attr[nameof(CheckedIcon)] = CheckedIcon;
            }

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
            if (false != Dense)
            {
                // Add the property value.
                attr[nameof(Dense)] = Dense;
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
            if (false != string.IsNullOrEmpty(IndeterminateIcon))
            {
                // Add the property value.
                attr[nameof(IndeterminateIcon)] = IndeterminateIcon;
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
            if (null != Tag)
            {
                // Add the property value.
                attr[nameof(Tag)] = Tag;
            }

            // Does this property have a non-default value?
            if (false != TriState)
            {
                // Add the property value.
                attr[nameof(TriState)] = TriState;
            }

            // Does this property have a non-default value?
            if (false != string.IsNullOrEmpty(UncheckedIcon))
            {
                // Add the property value.
                attr[nameof(UncheckedIcon)] = UncheckedIcon;
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
