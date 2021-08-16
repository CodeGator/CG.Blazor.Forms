using System;
using System.Collections.Generic;
using CG.Blazor.Forms.Attributes;

namespace MudBlazor
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should be
    /// rendered using a <see cref="MudAlert"/> control.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Decorating a string property with this attribute causes the form generator
    /// to render the property as a <see cref="MudAlert"/> component. 
    /// </para>
    /// <para>
    /// This attribute is only valid when placed on a property that returns a 
    /// string value.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderMudAlertAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property sets the position of the text to the start (Left in 
        /// LTR and right in RTL).
        /// </summary>
        public AlertTextPosition AlertTextPosition { get; set; }

        /// <summary>
        /// This property contains user class names, separated by space.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// This property defines the icon used for the close button.
        /// </summary>
        public string CloseIcon { get; set; }

        /// <summary>
        /// This property indicates, if true, compact padding will be used.
        /// </summary>
        public bool Dense { get; set; }

        /// <summary>
        /// This property indicates the elevation. The higher the number, 
        /// the heavier the drop-shadow. 0 for no shadow.
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// This property indicates a custom icon, leave unset to use the 
        /// standard icon which depends on the Severity
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// This property indicates, if true, no alert icon will be used.
        /// </summary>
        public bool NoIcon { get; set; }

        /// <summary>
        /// This property indicates the severity of the alert. This defines 
        /// the color and icon used.
        /// </summary>
        public Severity Severity { get; set; }

        /// <summary>
        /// This property indicates if the alert shows a close icon.
        /// </summary>
        public bool ShowCloseIcon { get; set; }

        /// <summary>
        /// This property indicates, if true, rounded corners are disabled.
        /// </summary>
        public bool Square { get; set; }

        /// <summary>
        /// This property indicates user styles, applied on top of the 
        /// component's own classes and styles
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// This property contains a use Tag to attach any user data object 
        /// to the component for your convenience.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// This property carries all attributes you add to the component that 
        /// don't match any of its parameters. They will be splatted onto the 
        /// underlying HTML tag.
        /// </summary>
        public IDictionary<string, object> UserAttributes { get; set; }

        /// <summary>
        /// This property indicates the variant to use.
        /// </summary>
        public Variant Variant { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RenderMudAlertAttribute"/>
        /// class.
        /// </summary>
        public RenderMudAlertAttribute()
        {
            // Set default values.
            AlertTextPosition = AlertTextPosition.Left;
            Class = string.Empty;
            CloseIcon = string.Empty;
            Dense = false;
            Elevation = 0;
            Icon = string.Empty;
            NoIcon = false;
            Severity = Severity.Normal;
            ShowCloseIcon = false;
            Square = false;
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
            if (AlertTextPosition.Left != AlertTextPosition)
            {
                // Add the property value.
                attr[nameof(AlertTextPosition)] = AlertTextPosition;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Class))
            {
                // Add the property value.
                attr[nameof(Class)] = Class;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(CloseIcon))
            {
                // Add the property value.
                attr[nameof(CloseIcon)] = CloseIcon;
            }

            // Does this property have a non-default value?
            if (false != Dense)
            {
                // Add the property value.
                attr[nameof(Dense)] = Dense;
            }

            // Does this property have a non-default value?
            if (0 != Elevation)
            {
                // Add the property value.
                attr[nameof(Elevation)] = Elevation;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Icon))
            {
                // Add the property value.
                attr[nameof(Icon)] = Icon;
            }

            // Does this property have a non-default value?
            if (false != NoIcon)
            {
                // Add the property value.
                attr[nameof(NoIcon)] = NoIcon;
            }

            // Does this property have a non-default value?
            if (Severity.Normal != Severity)
            {
                // Add the property value.
                attr[nameof(Severity)] = Severity;
            }

            // Does this property have a non-default value?
            if (false != ShowCloseIcon)
            {
                // Add the property value.
                attr[nameof(ShowCloseIcon)] = ShowCloseIcon;
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
