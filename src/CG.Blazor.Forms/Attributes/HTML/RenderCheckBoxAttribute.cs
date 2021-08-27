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
    /// the form generator to render the property as a checkbox HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is only valid when placed on a property of type: bool.
    /// </para>
    /// </remarks>
    /// <example>
    /// Here is an example of decorating a model property to render a checkbox:
    /// <code>
    /// using CG.Blazor.Forms.Attributes;
    /// class MyModel
    /// {
    ///     [RenderCheckBox]
    ///     public bool MyProperty { get;set; }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderCheckBoxAttribute : HtmlAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property indicates that the value of the checkbox is 
        /// indeterminate rather than true or false
        /// </summary>
        public bool Indeterminate { get; set; }
        
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
        /// This constructor creates a new instance of <see cref="RenderCheckBoxAttribute"/>
        /// class.
        /// </summary>
        public RenderCheckBoxAttribute()
        {
            // Set default values.
            Indeterminate = false;
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

            // Does this property have a non-default value?
            if (false != Indeterminate)
            {
                // Add the property value.
                attr[nameof(Indeterminate)] = Indeterminate;
            }

            // Does this property have a non-default value?
            if (false != ReadOnly)
            {
                // Add the property value.
                attr[nameof(ReadOnly)] = ReadOnly;
            }

            // Was the class not overridden?
            if (false == attr.ContainsKey(nameof(Class)))
            {
                // Ensure we apply the bootstrap class.
                attr[nameof(Class)] = "form-check-input";
            }
            else 
            {
                // Get the existing CSS class(es)
                var @class = (string)attr[nameof(Class)];

                // Replace the generic form-control with something right
                //   for a bootstrap checkbox.
                @class = @class.Replace("form-control", "form-check-input");

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
                // If we get here then we are trying to render a checkbox HTML element
                //   and bind it to the specified bool property.

                // Should never happen, but, pffft, check it anyway.
                if (path.Count < 2)
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "RenderCheckBoxAttribute::Generate called with a shallow path!"
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
                        "RenderCheckBoxAttribute::Generate called with a null model!"
                        );

                    // Return the index.
                    return index;
                }

                // Get the model's type.
                var modelType = model.GetType();

                // Get the property's parent.
                var propParent = path.Skip(1).First();

                // We only render CheckBox controls against bools.
                if (prop.PropertyType == typeof(bool))
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Rendering property: '{PropName}' as a CheckBox element.",
                        prop.Name
                        );

                    // Get any non-default attribute values (overrides).
                    var attributes = this.ToAttributes();

                    // Ensure the ID property value is set.
                    attributes["id"] = prop.Name;

                    // Ensure the Name property value is set.
                    attributes["name"] = prop.Name;

                    // Ensure the Value property value is set.
                    attributes["checked"] = prop.GetValue(propParent);

                    // Ensure the type property value is set.
                    attributes["type"] = "checkbox";

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
                        { "class", "form-check" }
                    };

                    // Create the label.
                    var label = string.IsNullOrEmpty(Label) ? prop.Name : Label;

                    // Render the outer div.
                    index = builder.RenderUIElement(
                            index,
                            "div",
                            contentDelegate: childBuilder =>
                            {
                                // Render the input element.
                                index = childBuilder.RenderUIElement(
                                    index,
                                    "input",
                                    attributes: attributes
                                    );

                                // Render the label.
                                index = childBuilder.RenderUIElement(
                                    index,
                                    nameof(Label),
                                    contentDelegate: labelBuilder =>
                                        labelBuilder.AddContent(index++, label),
                                    attributes: labelAttributes
                                    );
                            },
                            attributes: divAttributes
                            );
                }
                else
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Not rendering property: '{PropName}' on: '{ObjName}' " +
                        "because we only render checkbox elements on properties " +
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
                    message: "Failed to render a CheckBox element! " +
                        "See inner exception(s) for more detail.",
                    innerException: ex
                    );
            }
        }

        #endregion
    }
}
