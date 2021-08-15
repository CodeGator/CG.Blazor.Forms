using System;
using System.Collections.Generic;
using CG.Blazor.Forms.Attributes;
using MudBlazor;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should 
    /// have all it's public child properties rendered inside a <see cref="MudPaper"/>
    /// control.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudPaperAttribute : RenderObjectAttribute
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
        /// This property contains the height of the component.
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// This property contains the maximum height of the component.
        /// </summary>
        public string MaxHeight { get; set; }

        /// <summary>
        /// This property contains the minumum height of the component.
        /// </summary>
        public string MinHeight { get; set; }

        /// <summary>
        /// This property contains the maximum width of the component.
        /// </summary>
        public string MaxWidth { get; set; }

        /// <summary>
        /// This property contains the minumum width of the component.
        /// </summary>
        public string MinWidth { get; set; }

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

        /// <summary>
        /// This property contains the width of the component.
        /// </summary>
        public string Width { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RenderMudPaperAttribute"/>
        /// class.
        /// </summary>
        public RenderMudPaperAttribute()
        {
            // Set default values.
            Class = string.Empty;
            Elevation = 1;
            Height = string.Empty;
            MaxHeight = string.Empty;
            MinHeight = string.Empty;
            MaxWidth = string.Empty;
            MinWidth = string.Empty;
            Outlined = false;
            Style = string.Empty;
            Square = false;
            Tag = null;
            UserAttributes = null;
            Width = string.Empty;
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
            if (false == string.IsNullOrEmpty(Height))
            {
                // Add the property value.
                attr[nameof(Height)] = Height;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(MaxHeight))
            {
                // Add the property value.
                attr[nameof(MaxHeight)] = MaxHeight;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(MinHeight))
            {
                // Add the property value.
                attr[nameof(MinHeight)] = MinHeight;
            }            

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(MaxWidth))
            {
                // Add the property value.
                attr[nameof(MaxWidth)] = MaxWidth;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(MinWidth))
            {
                // Add the property value.
                attr[nameof(MinWidth)] = MinWidth;
            }

            // Does this property have a non-default value?
            if (1 != Elevation)
            {
                // Add the property value.
                attr[nameof(Elevation)] = Elevation;
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
            if (false == string.IsNullOrEmpty(Width))
            {
                // Add the property value.
                attr[nameof(Width)] = Width;
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
