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
    /// the form generator to render the property as an email HTML element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This attribute is only valid when placed on a property of type: string.
    /// </para>
    /// </remarks>
    /// <example>
    /// Here is an example of decorating a model property to render an email:
    /// <code>
    /// using CG.Blazor.Forms.Attributes;
    /// class MyModel
    /// {
    ///     [RenderInputEmail]
    ///     public string MyProperty { get;set; }
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property)]
    public class RenderInputEmailAttribute : HtmlAttribute
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property indicates the maximum number of characters the input
        /// should accept.
        /// </summary>
        public string MaxLength { get; set; }

        /// <summary>
        /// This property indicates the minimum number of characters the input
        /// should accept.
        /// </summary>
        public string MinLength { get; set; }

        /// <summary>
        /// This property indicates whether the control allows multiple, 
        /// comma-separated, email addresses to be entered.
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// This property contains a list of pre-defined autocomplete options 
        /// for the element. The values should be space separated.
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// This property contains a regular expression the input's contents 
        /// must match in order to be valid.
        /// </summary>
        public string Pattern { get; set; }

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
        /// This property indicates how many characters wide the input field 
        /// should be.
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// This property indicates whether or not to enable spell checking 
        /// for the input field, or if the default spell checking configuration 
        /// should be used
        /// </summary>
        public bool SpellCheck { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of <see cref="RenderInputEmailAttribute"/>
        /// class.
        /// </summary>
        public RenderInputEmailAttribute()
        {
            // Set default values.
            MaxLength = string.Empty;
            MinLength = string.Empty;
            Multiple = false;
            Options = string.Empty;
            Pattern = string.Empty;
            Placeholder = string.Empty;
            ReadOnly = false;
            Size = string.Empty;
            SpellCheck = false;
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
            if (false == string.IsNullOrEmpty(MaxLength))
            {
                // Add the property value.
                attr[nameof(MaxLength)] = MaxLength;
            }

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(MinLength))
            {
                // Add the property value.
                attr[nameof(MinLength)] = MinLength;
            }

            // Does this property have a non-default value?
            if (false != Multiple)
            {
                // Add the property value.
                attr[nameof(Multiple)] = Multiple;
            }

            // Note: options deliberately not part of attributes.

            // Does this property have a non-default value?
            if (false == string.IsNullOrEmpty(Pattern))
            {
                // Add the property value.
                attr[nameof(Pattern)] = Pattern;
            }

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
            if (false == string.IsNullOrEmpty(Size))
            {
                // Add the property value.
                attr[nameof(Size)] = Size;
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
                // If we get here then we are trying to render an email HTML element
                //   and bind it to the specified string property.

                // Should never happen, but, pffft, check it anyway.
                if (path.Count < 2)
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "RenderInputEmailAttribute::Generate called with a shallow path!"
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
                        "RenderInputEmailAttribute::Generate called with a null model!"
                        );

                    // Return the index.
                    return index;
                }

                // Get the model's type.
                var modelType = model.GetType();

                // Get the property's parent.
                var propParent = path.Skip(1).First();

                // We only render InputText controls against strings.
                if (modelType == typeof(string))
                {
                    // Let the world know what we're doing.
                    logger.LogDebug(
                        "Rendering property: '{PropName}' as an email element.",
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
                    attributes["type"] = "email";

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
                        "because we only render email elements on properties " +
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
                    message: "Failed to render an email element! " +
                        "See inner exception(s) for more detail.",
                    innerException: ex
                    );
            }
        }

        #endregion
    }
}
