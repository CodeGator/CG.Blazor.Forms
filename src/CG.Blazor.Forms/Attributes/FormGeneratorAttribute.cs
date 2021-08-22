using CG.Blazor.Forms.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

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
            // Return the empty table.
            return new Dictionary<string, object>();
        }

        // *******************************************************************

        /// <summary>
        /// This method is called by the form generator to generate content for
        /// the specified property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="path">The path to the current model.</param>
        /// <param name="prop">The reflection information for the property.</param>
        /// <param name="logger">The logger to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// one of more arguments are missing, or invalid.</exception>
        /// <exception cref="FormGenerationException">This exception is thrown whenever
        /// the rendering operation fails to complete.</exception>
        public virtual int Generate(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            Stack<object> path,
            PropertyInfo prop,
            ILogger<IFormGenerator> logger
            )
        {
            // Return the index.
            return index;
        }

        #endregion
    }
}
