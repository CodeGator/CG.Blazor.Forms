using CG.Blazor.Forms.Services;
using CG.Validations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is an attribute that, when applied to a class, or an object
    /// property on a class, causes the form generator to render the properties 
    /// of the object as HTML.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is only valid when placed on a property of type: object,
    /// or on a class definition itself.
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
     
    public class RenderObjectAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
        public override int Generate(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            Stack<object> path,
            PropertyInfo prop,
            ILogger<IFormGenerator> logger
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(path, nameof(path))
                .ThrowIfNull(logger, nameof(logger));

            try
            {
                // If we get here then we are trying to render an entire object,
                //   one child property at a time.

                // Should never happen, but, pffft, check it anyway.
                if (false == path.Any())
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "RenderObjectAttribute::Generate called with an empty path!"
                        );

                    // Return the index.
                    return index;
                }

                // Get the model reference.
                var model = path.Peek();

                // Get the model's type.
                var modelType = model.GetType();

                // Get the child properties.
                var childProps = modelType.GetProperties()
                    .Where(x => x.CanWrite && x.CanRead);

                // Loop through the child properties.
                foreach (var childProp in childProps)
                {
                    // Get the value of the child property.
                    var childValue = childProp.GetValue(model);

                    // Is the value missing?
                    if (null == childValue)
                    {
                        // If we get here then we've encountered a NULL reference
                        //   in the specified property. That may not be an issue,
                        //   if the property is a string, or a nullable type, because
                        //   we can continue to render.
                        // On the other hand, if the property isn't a string or 
                        //   nullable type then we really do need to ignore the property.

                        // Is the property type a string?
                        if (typeof(string) == childProp.PropertyType)
                        {
                            // Assign a default value.
                            childValue = string.Empty;
                        }

                        else if (typeof(Nullable<>) == childProp.PropertyType)
                        {
                            // Nothing to do here, really.
                        }

                        // Otherwise, is this a NULL object ref?
                        else if (childProp.PropertyType.IsClass)
                        {
                            // Let the world know what we're doing.
                            logger.LogDebug(
                                "ignoring property: '{PropName}' on: '{ParentName}' " +
                                "because it's value is null!",
                                childProp.Name,
                                model.GetType().Name
                                );

                            // Ignore this property.
                            continue;
                        }
                    }

                    // Push the property onto path.
                    path.Push(childValue);

                    // Look for any form generation attributes on the view-model.
                    var attrs = childProp.GetCustomAttributes<FormGeneratorAttribute>();

                    // Loop through the attributes.
                    foreach (var attr in attrs)
                    {
                        // Render the property.
                        index = attr.Generate(
                            builder,
                            index,
                            eventTarget,
                            path,
                            childProp,
                            logger
                            );
                    }

                    // Did we ignore this property?
                    if (false == attrs.Any())
                    {
                        // Let the world know what we're doing.
                        logger.LogDebug(
                            "ignoring property: '{PropName}' on: '{ParentName}' " +
                            "because it's not decorated with a FormGenerator attribute!",
                            childProp.Name,
                            model.GetType().Name
                            );
                    }

                    // Pop property off the path.
                    path.Pop();                    
                }

                // Return the index.
                return index;
            }
            catch (Exception ex)
            {
                // Provide better context for the error.
                throw new FormGenerationException(
                    message: "Failed to render an object! " +
                        "See inner exception(s) for more detail.",
                    innerException: ex
                    );
            }
        }

        #endregion
    }
}
