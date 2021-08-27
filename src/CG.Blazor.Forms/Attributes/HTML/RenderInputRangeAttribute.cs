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
    /// This class is an attribute that, when applied to a numeric property, causes
    /// the form generator to render the property as a range HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is only valid when placed on a property of type: numeric.
    /// Which means, byte, int, long, float, double, or decimal.
    /// </para>
    /// </remarks>
    /// <example>
    /// Here is an example of decorating a model property to render a range:
    /// <code>
    /// using CG.Blazor.Forms.Attributes;
    /// class MyModel
    /// {
    ///     [RenderInputRange]
    ///     public int MyProperty { get;set; }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderInputRangeAttribute : HtmlAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a list of pre-defined autocomplete options 
        /// for the element. The values should be space separated.
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// This property indicates the maximum number the input should accept.
        /// </summary>
        public string Max { get; set; }

        /// <summary>
        /// This property indicates the minimum number the input should accept.
        /// </summary>
        public string Min { get; set; }

        /// <summary>
        /// This property contains a short hint that describes the expected 
        /// value of the component.
        /// </summary>
        public string Placeholder { get; set; }
        
        /// <summary>
        /// This property indicates the component is read only.
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// This property contains the stepping interval, when clicking up and down
        /// spinner buttons and validating the input.
        /// </summary>
        public string Step { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of <see cref="RenderInputRangeAttribute"/>
        /// class.
        /// </summary>
        public RenderInputRangeAttribute()
        {
            // Set default values.
            Options = string.Empty;
            Max = string.Empty;
            Min = string.Empty;
            Placeholder = string.Empty;
            ReadOnly = false;
            Step = string.Empty;
        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
        public override IDictionary<string, object> ToAttributes()
        {
            // Give the base class a chance.
            var attr = base.ToAttributes();

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Max))
            {
                // Add the property value.
                attr[nameof(Max)] = Max;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Min))
            {
                // Add the property value.
                attr[nameof(Min)] = Min;
            }

            // Note: options deliberately not added to attributes.

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Placeholder))
            {
                // Add the property value.
                attr[nameof(Placeholder)] = Placeholder;
            }

            // Does this property have a non-default value?
            if (false != ReadOnly)
            {
                // Add the property value.
                attr[nameof(ReadOnly)] = ReadOnly;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Step))
            {
                // Add the property value.
                attr[nameof(Step)] = Step;
            }

            // Return the attributes.
            return attr;
        }

        // *******************************************************************

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
                // If we get here then we are trying to render a range HTML element
                //   and bind it to the specified numeric property.

                // Should never happen, but, pffft, check it anyway.
                if (path.Count < 2)
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "RenderInputRangeAttribute::Generate called with a shallow path!"
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
                        "RenderInputRangeAttribute::Generate called with a null model!"
                        );

                    // Return the index.
                    return index;
                }

                // Get the model's type.
                var modelType = model.GetType();

                // Get the property's parent.
                var propParent = path.Skip(1).First();

                // We only render range controls against numeric types.
                if (modelType == typeof(byte) ||
                    modelType == typeof(int) ||
                    modelType == typeof(long) ||
                    modelType == typeof(float) ||
                    modelType == typeof(double) ||
                    modelType == typeof(decimal))
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Rendering property: '{PropName}' as a range element.",
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
                    attributes["type"] = "range";

                    // Ensure the OnChange property is bound, both ways.
                    attributes["onchange"] = RuntimeHelpers.TypeCheck<EventCallback<ChangeEventArgs>>(
                        EventCallback.Factory.Create<ChangeEventArgs>(
                            eventTarget,
                            EventCallback.Factory.CreateInferred<ChangeEventArgs>(
                                eventTarget,
                                x =>
                                { 
                                    if (typeof(byte) == prop.PropertyType)
                                    {
                                        if (byte.TryParse((string)x.Value, out var byteVal))
                                        {
                                            prop.SetValue(propParent, byteVal);
                                        }
                                    }
                                    else if (typeof(int) == prop.PropertyType)
                                    {
                                        if (int.TryParse((string)x.Value, out var intVal))
                                        {
                                            prop.SetValue(propParent, intVal);
                                        }
                                    }
                                    else if (typeof(long) == prop.PropertyType)
                                    {
                                        if (long.TryParse((string)x.Value, out var longVal))
                                        {
                                            prop.SetValue(propParent, longVal);
                                        }
                                    }
                                    else if (typeof(float) == prop.PropertyType)
                                    {
                                        if (float.TryParse((string)x.Value, out var floatVal))
                                        {
                                            prop.SetValue(propParent, floatVal);
                                        }
                                    }
                                    else if (typeof(double) == prop.PropertyType)
                                    {
                                        if (double.TryParse((string)x.Value, out var doubleVal))
                                        {
                                            prop.SetValue(propParent, doubleVal);
                                        }
                                    }
                                    else if (typeof(decimal) == prop.PropertyType)
                                    {
                                        if (decimal.TryParse((string)x.Value, out var decimalVal))
                                        {
                                            prop.SetValue(propParent, decimalVal);
                                        }
                                    }
                                },
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

                    // Attributes for the datalist.
                    var dataAttributes = new Dictionary<string, object>()
                    {
                        { "id", $"{prop.Name}-list" }
                    };

                    // Create the label.
                    var label = string.IsNullOrEmpty(Label) ? prop.Name : Label;

                    // Split the options.
                    var options = Options.Split(' ');

                    // Should we render options?
                    if (options.Length > 1)
                    {
                        // Ensure the element has the list id.
                        attributes["list"] = $"{prop.Name}-list";
                    }

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

                                // Should we render options?
                                if (options.Length > 1)
                                {
                                    // Render the data list.
                                    index = childBuilder.RenderUIElement(
                                        index,
                                        "datalist",
                                        contentDelegate: dataBuilder =>
                                        {
                                            // Loop and render options.
                                            foreach (var option in options)
                                            {
                                                // Render the option.
                                                index = dataBuilder.RenderUIElement(
                                                    index,
                                                    "option",
                                                    attributes: new Dictionary<string, object>() { { "value", option } }
                                                    );
                                            }
                                        },
                                        attributes: dataAttributes
                                        );
                                }
                            },
                            attributes: divAttributes
                            );
                }
                else
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Not rendering property: '{PropName}' on: '{ObjName}' " +
                        "because we only render range elements on properties " +
                        "that are of type: numeric. That property is of type: '{PropType}'!",
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
                    message: "Failed to render an range element! " +
                        "See inner exception(s) for more detail.",
                    innerException: ex
                    );
            }
        }

        #endregion
    }
}
