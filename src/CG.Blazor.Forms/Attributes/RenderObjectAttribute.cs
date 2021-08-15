using System;
using System.Collections.Generic;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is an attribute that indicates a decorated property should 
    /// have all it's public child properties rendered.
    /// </summary>
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
