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
    /// This class is an attribute that, when applied to a bool property, causes
    /// the form generator to render the property as a radio group HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is only valid when placed on a property of type: bool.
    /// </para>
    /// </remarks>
    /// <example>
    /// Here is an example of decorating a model property to render a radio group:
    /// <code>
    /// using CG.Blazor.Forms.Attributes;
    /// class MyModel
    /// {
    ///     [RenderRadioGroup(Options = "A B C")]
    ///     public string MyProperty { get;set; }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderRadioGroupAttribute : HtmlAttribute
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
        /// This property indicates the component is read only.
        /// </summary>
        public bool ReadOnly { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of <see cref="RenderRadioGroupAttribute"/>
        /// class.
        /// </summary>
        public RenderRadioGroupAttribute()
        {
            // Set default values.
            Options = string.Empty; 
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
            // Create a table to hold the attributes.
            var attr = base.ToAttributes();

            // Note: options deliberately not added to the attributes.

            // Does this property have a non-default value?
            if (false != ReadOnly)
            {
                // Add the property value.
                attr[nameof(ReadOnly)] = ReadOnly;
            }

            // Was the class overridden?
            if (attr.ContainsKey(nameof(Class)))
            {
                // Get the existing CSS class(es)
                var @class = (string)attr[nameof(Class)];

                // Remove the generic form-control class.
                @class = @class.Replace("form-control", "");

                // Re-apply the class.
                attr[nameof(Class)] = @class;
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
                // If we get here then we are trying to render a radio group HTML element
                //   and bind it to the specified string property.

                // Should never happen, but, pffft, check it anyway.
                if (path.Count < 2)
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "RenderRadioGroupAttribute::Generate called with a shallow path!"
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
                        "RenderRadioGroupAttribute::Generate called with a null model!"
                        );

                    // Return the index.
                    return index;
                }

                // Get the model's type.
                var modelType = model.GetType();

                // Get the property's parent.
                var propParent = path.Skip(1).First();

                // We only render radio groups against strings.
                if (prop.PropertyType == typeof(string))
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Rendering property: '{PropName}' as a radio group element.",
                        prop.Name
                        );

                    // Get any non-default attribute values (overrides).
                    var attributes = this.ToAttributes();
                                        
                    // Ensure the type property value is set.
                    attributes["type"] = "radio";

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

                    // Create the label.
                    var label = string.IsNullOrEmpty(Label) ? prop.Name : Label;

                    // Split the options.
                    var options = Options.Split(',');

                    // Ensure the Name property value is set.
                    attributes["name"] = prop.Name;

                    // Get the current model value.
                    var value = (string)prop.GetValue(propParent);

                    // Render the outermost fieldset.
                    index = builder.RenderUIElement(
                        index, 
                        "fieldset",
                        contentDelegate: fieldSetBuilder =>
                        {
                            // Render the legend.
                            index = fieldSetBuilder.RenderUIElement(
                                index,
                                "legend",
                                contentDelegate: legendBuilder =>
                                    legendBuilder.AddContent(index++, label)
                                );

                            // Render the div.
                            index = fieldSetBuilder.RenderUIElement(
                                    index,
                                    "div",
                                    contentDelegate: divBuilder =>
                                    {
                                        // Loop through the options.
                                        foreach (var option in options)
                                        {
                                            // Ensure the value is set.
                                            attributes["value"] = option;

                                            // Select the right radio button.
                                            attributes["checked"] = ((string)attributes["value"] == value);

                                            // Attributes for the label.
                                            var labelAttributes = new Dictionary<string, object>()
                                            {
                                                { "for", option.Replace(" ", "") }
                                            };

                                            // Render the input element.
                                            index = divBuilder.RenderUIElement(
                                                        index,
                                                        "input",
                                                        attributes: attributes
                                                        );

                                            // Render the label.
                                            index = divBuilder.RenderUIElement(
                                                        index,
                                                        "label",
                                                        contentDelegate: labelBuilder =>
                                                            labelBuilder.AddContent(index++, option),
                                                        attributes: labelAttributes
                                                        );
                                        }
                                    });
                        });
                }
                else
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Ignoring property: '{PropName}' on: '{ObjName}' " +
                        "because we only render radio group elements on properties " +
                        "that are of type: bool. That property is of type: '{PropType}'!",
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
                    message: "Failed to render a radio group element! " +
                        "See inner exception(s) for more detail.",
                    innerException: ex
                    );
            }
        }

        #endregion
    }
}
