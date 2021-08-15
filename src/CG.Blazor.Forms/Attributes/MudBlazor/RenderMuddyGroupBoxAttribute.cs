using System;
using System.Collections.Generic;
using CG.Blazor.Forms.Attributes;
using CG.Blazor.Forms.Components;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be
    /// rendered using a <see cref="MuddyGroupBox"/> control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMuddyGroupBoxAttribute : RenderObjectAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains any CSS classes to use for the control.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property contains the elevation to use for the control.
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// This property contains the label for the component.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// This property contains the color for the label.
        /// </summary>
        public Color LabelColor { get; set; }

        /// <summary>
        /// This property contains the typography for the label.
        /// </summary>
        public Typo LabelTypo { get; set; }

        /// <summary>
        /// This property indicates whether the control should be outlined, 
        /// or not.
        /// </summary>
        public bool Outlined { get; set; }

        /// <summary>
        /// This property indicates the CSS styles to use for the control.
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// This property indicates whether the control should show square corners, 
        /// or not.
        /// </summary>
        public bool Square { get; set; }

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
        
        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RenderMuddyGroupBoxAttribute"/>
        /// class.
        /// </summary>
        public RenderMuddyGroupBoxAttribute()
        {
            // Set default values.
            Class = string.Empty;
            Elevation = 1;
            Label = string.Empty;
            LabelColor = Color.Default;
            LabelTypo = Typo.h6;
            Outlined = false;
            Style = string.Empty;
            Square = false;
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
            if (1 != Elevation)
            {
                // Add the property value.
                attr[nameof(Elevation)] = Elevation;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Label))
            {
                // Add the property value.
                attr[nameof(Label)] = Label;
            }

            // Does this property have a non-default value?
            if (Color.Default != LabelColor)
            {
                // Add the property value.
                attr[nameof(LabelColor)] = LabelColor;
            }

            // Does this property have a non-default value?
            if (Typo.h6 != LabelTypo)
            {
                // Add the property value.
                attr[nameof(LabelTypo)] = LabelTypo;
            }

            // Does this property have a non-default value?
            if (false != Outlined)
            {
                // Add the property value.
                attr[nameof(Outlined)] = Outlined;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Style))
            {
                // Add the property value.
                attr[nameof(Style)] = Style;
            }

            // Does this property have a non-default value?
            if (false != Square)
            {
                // Add the property value.
                attr[nameof(Square)] = Square;
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
