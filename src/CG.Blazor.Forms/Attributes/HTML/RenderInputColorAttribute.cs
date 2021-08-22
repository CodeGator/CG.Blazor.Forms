using CG.Blazor.Forms.Services;
using CG.Validations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is an attribute that, when applied to a string property, causes
    /// the form generator to render the property as a color HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is only valid when placed on a property of type: string.
    /// </para>
    /// </remarks>
    /// <example>
    /// Here is an example of decorating a model property to render a color:
    /// <code>
    /// using CG.Blazor.Forms.Attributes;
    /// class MyModel
    /// {
    ///     [RenderInputColor]
    ///     public string MyProperty { get;set; }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderInputColorAttribute : HtmlAttribute
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
                // If we get here then we are trying to render a color HTML element
                //   and bind it to the specified string property.

                // Should never happen, but, pffft, check it anyway.
                if (path.Count < 2)
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "RenderInputColorAttribute::Generate called with a shallow path!"
                        );

                    // Return the index.
                    return index;
                }

                // Get the model reference.
                var model = path.Peek();

                // Should never happen, but, pffft, check it anyway.
                if (null == model)
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "RenderInputColorAttribute::Generate called with a null model!"
                        );

                    // Return the index.
                    return index;
                }

                // Get the model's type.
                var modelType = model.GetType();

                // Get the property's parent.
                var propParent = path.Skip(1).First();

                // We only render color controls against strings.
                if (modelType == typeof(string))
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Rendering property: '{PropName}' as a color element.",
                        prop.Name
                        );

                    // Get any non-default attribute values (overrides).
                    var attributes = ToAttributes();

                    // Ensure the ID property value is set.
                    attributes["id"] = prop.Name;

                    // Ensure the Name property value is set.
                    attributes["name"] = prop.Name;

                    // Ensure the Value property value is set.
                    attributes["value"] = prop.GetValue(propParent);

                    // Ensure the type property value is set.
                    attributes["type"] = "color";

                    // Ensure the OnChange property is bound, both ways.
                    attributes["onchange"] = RuntimeHelpers.TypeCheck<EventCallback<ChangeEventArgs>>(
                        EventCallback.Factory.Create<ChangeEventArgs>(
                            eventTarget,
                            EventCallback.Factory.CreateInferred<ChangeEventArgs>(
                                eventTarget,
                                x => prop.SetValue(propParent, x.Value),
                                new ChangeEventArgs()
                                {
                                    Value = prop.GetValue(propParent)
                                })
                            )
                        );

                    // Attributes for the label.
                    var labelAttributes = new Dictionary<string, object>()
                    {
                        { "for", attributes["name"] }
                    };

                    // Attributes for the div.
                    var divAttributes = new Dictionary<string, object>()
                    {
                        { "class", "form-group" }
                    };

                    // Create the label.
                    var label = string.IsNullOrEmpty(Label) ? prop.Name : Label;

                    // Render the outer div.
                    index = builder.RenderUIElement(
                            index,
                            "div",
                            contentDelegate: childBuilder =>
                            {
                                // Render the label.
                                index = childBuilder.RenderUIElement(
                                    index,
                                    nameof(Label),
                                    contentDelegate: labelBuilder =>
                                        labelBuilder.AddContent(index++, label),
                                    attributes: labelAttributes
                                    );

                                // Render the input element.
                                index = childBuilder.RenderUIElement(
                                    index,
                                    "input",
                                    attributes: attributes
                                    );
                            },
                            attributes: divAttributes
                            );
                }
                else
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Ignoring property: '{PropName}' on: '{ObjName}' " +
                        "because we only render color elements on properties " +
                        "that are of type: string. That property is of type: '{PropType}'!",
                        prop.Name,
                        propParent.GetType().Name,
                        prop.PropertyType.Name
                        );
                }

                // Return the index.
                return index;
            }
            catch (Exception ex)
            {
                // Provide better context for the error.
                throw new FormGenerationException(
                    message: "Failed to render an color element! " +
                        "See inner exception(s) for more detail.",
                    innerException: ex
                    );
            }
        }

        #endregion
    }
}
