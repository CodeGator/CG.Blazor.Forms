using CG.Blazor.Forms.Services;
using CG.Validations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CG.Blazor.Forms.Attributes
{
    /// <summary>
    /// This class is an attribute that, when applied to a TimeSpan property, 
    /// causes the form generator to render the property as an time HTML 
    /// element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is only valid when placed on a property of type: TimeSpan
    /// </para>
    /// </remarks>
    /// <example>
    /// Here is an example of decorating a model property to render a time:
    /// <code>
    /// using CG.Blazor.Forms.Attributes;
    /// class MyModel
    /// {
    ///     [RenderInputTime]
    ///     public TimeSpan MyProperty { get;set; }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderInputTimeAttribute : HtmlAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the maximum date for the element. The format
        /// MUST be yyyy-MM-dd.
        /// </summary>
        public string Max { get; set; }

        /// <summary>
        /// This property contains the minimum date for the element. The format
        /// MUST be yyyy-MM-dd.
        /// </summary>
        public string Min { get; set; }

        /// <summary>
        /// This property contains a list of pre-defined autocomplete options 
        /// for the element. The values should be space separated. The values
        /// should be in 24 hour format: ##:##, without embedded spaces or a 
        /// trailing AM or PM. 
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// This property contains a short hint that describes the expected 
        /// value of the component.
        /// </summary>
        public string Placeholder { get; set; }
        
        /// <summary>
        /// This property contains the stepping interval, when clicking up and down
        /// spinner buttons and validating the date.
        /// </summary>
        public string Step { get; set; }

        /// <summary>
        /// This property indicates the component is read only.
        /// </summary>
        public bool ReadOnly { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of <see cref="RenderInputTimeAttribute"/>
        /// class.
        /// </summary>
        public RenderInputTimeAttribute()
        {
            // Set default values.
            Max = string.Empty;
            Min = string.Empty;
            Options = string.Empty; 
            Placeholder = string.Empty;
            Step = string.Empty;
            ReadOnly = false;
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
            if (false == string.IsNullOrEmpty(Step))
            {
                // Add the property value.
                attr[nameof(Step)] = Step;
            }

            // Does this property have a non-default value?
            if (false != ReadOnly)
            {
                // Add the property value.
                attr[nameof(ReadOnly)] = ReadOnly;
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
                // If we get here then we are trying to render a time HTML element
                //   and bind it to the specified TimeSpan property.

                // Should never happen, but, pffft, check it anyway.
                if (path.Count < 2)
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "RenderInputTimeAttribute::Generate called with a shallow path!"
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
                        "RenderInputTimeAttribute::Generate called with a null model!"
                        );

                    // Return the index.
                    return index;
                }

                // Get the model's type.
                var modelType = model.GetType();

                // Get the property's parent.
                var propParent = path.Skip(1).First();

                // We only render time controls against timespans.
                if (modelType == typeof(TimeSpan))
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Rendering property: '{PropName}' as a time element.",
                        prop.Name
                        );

                    // Get any non-default attribute values (overrides).
                    var attributes = this.ToAttributes();

                    // Ensure the ID property value is set.
                    attributes["id"] = prop.Name;

                    // Ensure the Name property value is set.
                    attributes["name"] = prop.Name;

                    // Ensure the Value property value is set.
                    attributes["value"] = ((TimeSpan)prop.GetValue(propParent)).ToString();

                    // Ensure the type property value is set.
                    attributes["type"] = "time";

                    // Ensure the OnChange property is bound, both ways.
                    attributes["onchange"] = RuntimeHelpers.TypeCheck<EventCallback<ChangeEventArgs>>(
                        EventCallback.Factory.Create<ChangeEventArgs>(
                            eventTarget,
                            EventCallback.Factory.CreateInferred<ChangeEventArgs>(
                                eventTarget,
                                x => prop.SetValue(propParent, TimeSpan.Parse((string)x.Value)),
                                new ChangeEventArgs()
                                {
                                    Value = ((TimeSpan)prop.GetValue(propParent)).ToString()
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
                        "Ignoring property: '{PropName}' on: '{ObjName}' " +
                        "because we only render time elements on properties " +
                        "that are of type: TimeSpan. That property is of type: '{PropType}'!",
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
                    message: "Failed to render an time element! " +
                        "See inner exception(s) for more detail.",
                    innerException: ex
                    );
            }
        }

        #endregion
    }
}
