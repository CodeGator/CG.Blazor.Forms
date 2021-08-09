using System;
using System.Collections.Generic;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is the base for all form generation attributes.
    /// </summary>
    public abstract class FormGeneratorAttribute : Attribute
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method can be overridden to produce a table of named attributes.
        /// </summary>
        /// <returns>A table of named attributes.</returns>
        public virtual IDictionary<string, object> ToAttributes()
        {
            return new Dictionary<string, object>();
        }

        #endregion
    }
}
