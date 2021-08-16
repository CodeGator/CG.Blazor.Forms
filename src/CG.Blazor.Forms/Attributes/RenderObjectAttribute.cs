using System;
using System.Collections.Generic;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should 
    /// have all it's public child properties rendered.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In order to render a property that returns an object reference (in other
    /// words, not a primitive type), that property must be decorated with this
    /// attribute type, or a derived attribute type. Otherwise, the property will 
    /// be ignored during form generation.
    /// </para>
    /// <para>
    /// This attribute is only valid when placed on a property that returns an 
    /// object type.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderObjectAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="RenderObjectAttribute"/>
        /// class.
        /// </summary>
        public RenderObjectAttribute()
        {
            // Set default values.
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

            // Return the attributes.
            return attr;
        }

        #endregion
    }
}
