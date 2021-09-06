using CG.Blazor.Forms.Services;
using CG.Validations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
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
    /// <example>
    /// Here is an example of decorating a model property to render:
    /// <code>
    /// using CG.Blazor.Forms.Attributes;
    /// class MyModel
    /// {
    ///     [RenderObject]
    ///     public MyModel2 MyProperty { get;set; }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(
        AttributeTargets.Property | AttributeTargets.Class, 
        AllowMultiple = false)]     
    public class RenderObjectAttribute : FormGeneratorAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains an optional LINQ expression, one that 
        /// determines whether to render the associated object, or not. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// The format for the property is of type: x => x.Property = value,
        /// where 'x' is a LINQ placeholder for the associated object, and 
        /// 'Property' is the name of a property on that object, and 'value' 
        /// is the value to test for. Any valid LINQ comparison may be used 
        /// to produce the TRUE/FALSE return value.
        /// </para>
        /// <para>
        /// The associated object is rendered normally if the property is empty, 
        /// or. if the property contains a LINQ expression that resolved to TRUE.
        /// If the expression returns FALSE, the associated object is not rendered.
        /// </para>
        /// </remarks>
        public string VisibleExp { get; set; }

        #endregion

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

                // Is the IsVisible expression itself invalid?
                if (false == TryVisibleExp(
                    path, 
                    out var isVisible
                    ))
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Not rendering a '{PropType}' object. [idx: '{Index}'] object " +
                        "since the 'VisibleExp' property contains a malformed LINQ expression. " +
                        "The expression should be like: x => x.Property = \"value\"",
                        modelType.Name,
                        index
                        );

                    // Return the index.
                    return index;
                }

                // Is the IsVisible expression false?
                if (false == isVisible)
				{
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Not rendering a '{PropType}' object. [idx: '{Index}'] object " +
                        "since the 'VisibleExp' property contains a LINQ expression that " +
                        "resolves to false. ",
                        modelType.Name,
                        index
                        );

                    // Return the index.
                    return index;
                }

                // Let the world know what we're doing.
                logger.LogDebug(
                    "Rendering child properties for a '{PropType}' object. [idx: '{Index}']",
                    modelType.Name,
                    index
                    );

                // Get the child properties.
                var childProps = modelType.GetProperties()
                    .Where(x => x.CanWrite && x.CanRead);

                // Loop through the child properties.
                foreach (var childProp in childProps)
                {
                    // Create a complete property path, for logging.
                    var propPath = $"{string.Join('.', path.Reverse().Select(x => x.GetType().Name))}.{childProp.Name}";

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
                                "Not rendering property: '{PropPath}' [idx: '{Index}'] " +
                                "since it's value is null!",
                                propPath,
                                index
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
                            "Not rendering property: '{PropPath}' [idx: '{Index}'] " +
                            "since it's not decorated with a FormGenerator attribute!",
                            propPath,
                            index
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

        // *******************************************************************
        // Private methods.
        // *******************************************************************

        #region Private methods

        /// <summary>
        /// This method checks the <see cref="VisibleExp"/> property, and if
        /// populated, attemps to compile and invoke the LINQ expression contained 
        /// therein, returning the results of the expression.
        /// </summary>
        /// <param name="path">The path to the current model.</param>
        /// <param name="result">The results of the LINQ expression.</param>
        /// <returns>True if the <see cref="VisibleExp"/> property is either
        /// empty, or, contains a valid LINQ expression.</returns>
        private bool TryVisibleExp(
            Stack<object> path,
            out bool result
            )
		{
            // Make the compiler happy.
            result = true;

            // Is the expression missing, or empty?
            if (string.IsNullOrEmpty(VisibleExp))
			{
                // No expression is ok.
                return true;
			}

            // If we get here then we've determined there is a LINQ
            //   expression in the VisibleExp property, so we need to
            //   parse it now, and invoke the resulting Func to get
            //   the results.

            // Get the view-model reference.
            var viewModel = path.Skip(1).First();

            // Get the view-model's type.
            var viewModelType = viewModel.GetType();

            // Look for the expression parts.
            var parts = VisibleExp.Split("=>")
                .Select(x => x.Trim())
                .ToArray();

            // There should be 2 parts to a LINQ expression.
            if (2 == parts.Length)
            {
                // Create the parameter expression.
                var x = Expression.Parameter(
                    viewModelType, 
                    parts[0]
                    );

                // Parse the labmda expression.
                var exp = DynamicExpressionParser.ParseLambda(
                    new[] { x },
                    null,
                    parts[1]
                    );

                // Compile and invoke the expression.
                result = (bool)exp.Compile().DynamicInvoke(
                    viewModel
                    );

                // We have a valid result.
                return true;
            }

            // The expression was invalid.
            return false;
        }

		#endregion
	}
}
