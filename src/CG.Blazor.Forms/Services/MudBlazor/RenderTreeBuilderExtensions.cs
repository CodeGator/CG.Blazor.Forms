using CG.Blazor.Forms.Services;
using CG.Validations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.CompilerServices;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace MudBlazor
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="RenderTreeBuilder"/>
    /// type.
    /// </summary>
    internal static partial class RenderTreeBuilderExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method renders the specified UI component.
        /// </summary>
        /// <typeparam name="T">The type of UI component to render.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="contentDelegate">An optional delegate for building 
        /// any content for the control.</param>
        /// <param name="attributes">Optional table of named attributes.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderUIComponent<T>(
            this RenderTreeBuilder builder,
            int index,
            Action<RenderTreeBuilder> contentDelegate = null,
            IDictionary<string, object> attributes = null
            ) where T : class, IComponent
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index));

            // Open the HTML tag.
            builder.OpenComponent<T>(index++);

            // Are any attributes specified?
            if (null != attributes)
            {
                // Loop through the properties.
                foreach (var prop in attributes)
                {
                    // Add the standard attribute.
                    builder.AddAttribute(
                        index++,
                        prop.Key,
                        prop.Value
                        );
                }
            }

            // Should we render child content?
            if (null != contentDelegate)
            {
                // Render the child content
                builder.AddAttribute(
                    index++,
                    "ChildContent",
                    new RenderFragment(contentBuilder =>
                        contentDelegate(contentBuilder)
                        )
                    );
            }

            // Close the HTML tag.
            builder.CloseComponent();

            // Make the HTML purdy.
            builder.AddMarkupContent(index++, "\r\n    ");

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudAlert"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/>.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderDataAnnotationsValidator(
            this RenderTreeBuilder builder,
            int index
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index));

            // Open the HTML tag.
            builder.OpenComponent<DataAnnotationsValidator>(index++);

            // Close the HTML tag.
            builder.CloseComponent();

            // Make the HTML purdy.
            builder.AddMarkupContent(index++, "\r\n    ");

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudAlert"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified model property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudAlert(
            this RenderTreeBuilder builder,
            int index,
            RenderMudAlertAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();
                        
            // Render the property as a MudTextField control.
            index = builder.RenderUIComponent<MudAlert>(
                index++,
                attributes: attributes,
                contentDelegate: childBuilder =>
                {
                    // Add the child content.
                    childBuilder.AddContent(index++, (string)prop.GetValue(model));
                });

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudAutocomplete{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/>.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudAutocomplete<T>(
            this RenderTreeBuilder builder,
            int index,
            RenderMudAutocompleteAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Did we not override the label?
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Did we override the search function?
            if (attributes.ContainsKey("SearchFunc"))
            {
                // Should we convert from the string?
                if (attributes["SearchFunc"] is string methodName)
                {
                    // Get the model type.
                    var modelType = model?.GetType();

                    // Get the method information.
                    var methodInfo = modelType?.GetMethod(
                        methodName,
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
                        );

                    // Did we fail?
                    if (null == methodInfo)
                    {
                        // Panic!!
                        throw new FormGenerationException(
                            message: $"Unable to locate an autocomplete search method named: " +
                                $"'{methodName}' on the model type: '{modelType?.Name}'. Please " +
                                $"check the spelling for the 'SearchFunc' property, on the " +
                                $"MudAutocompleteAttribute that is currently decorating the " +
                                $"'{prop.Name}' property. Remember, the search method should be " +
                                $"part of the model. If the method is located anywhere else we won't " +
                                $"be able to find it."
                            );
                    }

                    // Create a model reference expression.
                    var modelExp = Expression.Constant(
                        model
                        );

                    // Create a parameter expression.
                    var p1 = Expression.Parameter(
                        typeof(T),
                        "p1"
                        );

                    // Create the method call expression.
                    var callExp = Expression.Call(
                        modelExp,
                        methodInfo,
                        p1
                        );

                    // Create a lambda expression.
                    var lambdaExp = Expression.Lambda<Func<T, Task<IEnumerable<T>>>>(
                        callExp,
                        callExp.Arguments.OfType<ParameterExpression>()
                        );

                    // Compile the expression to a func.
                    var func = lambdaExp.Compile();

                    // Replace the method name with the func.
                    attributes["SearchFunc"] = new Func<T, Task<IEnumerable<T>>>(
                        func
                        );
                }
            }

            // Ensure the Value property value is set.
            attributes["Value"] = (T)prop.GetValue(model);

            // Ensure the ValueChanged property is bound, both ways.
            attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(model, x),
                        (T)prop.GetValue(model)
                        )
                    )
                );

            // Make the compiler happy.
            if (null != model)
            {
                // Ensure the For property value is set.
                attributes["For"] = Expression.Lambda<Func<T>>(
                    MemberExpression.Property(
                        Expression.Constant(
                            model,
                            model.GetType()),
                        prop.Name
                        )
                    );
            }

            // Render the MudAutocomplete control.
            builder.RenderUIComponent<MudAutocomplete<T>>(
                index++,
                attributes: attributes
                );
            
            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudCheckBox{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified model property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudCheckBox<T>(
            this RenderTreeBuilder builder,
            int index,
            RenderMudCheckBoxAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Ensure the Label property is set.
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Ensure the Checked property value is set.
            attributes["Checked"] = (T)prop.GetValue(model);

            // Ensure the CheckedChanged property is bound, both ways.
            attributes["CheckedChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(model, x),
                        (T)prop.GetValue(model)
                        )
                    )
                );

            // Render the property as a MudCheckBox control.
            index = builder.RenderUIComponent<MudCheckBox<T>>(
                index++,
                attributes: attributes
                );

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudNumericField{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified model property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudNumericField<T>(
            this RenderTreeBuilder builder,
            int index,
            RenderMudNumericFieldAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Did we not override the label?
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure the Label property is set.
                attributes["Label"] = prop.Name;
            }

            // Ensure the Value property value is set.
            attributes["Value"] = (T)prop.GetValue(model);

            // Ensure the Value property is bound, both ways.
            attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(model, x),
                        (T)prop.GetValue(model)
                        )
                    )
                );

            // Make the compiler happy.
            if (null != model)
            {
                // Ensure the For property value is set.
                attributes["For"] = Expression.Lambda<Func<T>>(
                    MemberExpression.Property(
                        Expression.Constant(
                            model,
                            model.GetType()),
                        prop.Name
                        )
                    );
            }

            // Render as a MudNumericField control.
            index = builder.RenderUIComponent<MudNumericField<T>>(
                index++,
                attributes: attributes
                );

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudRadioGroup{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified model property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudRadioGroup<T>(
            this RenderTreeBuilder builder,
            int index,
            RenderMudRadioGroupAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Ensure the value is set.
            attributes["SelectedOption"] = (T)prop.GetValue(model);

            // Ensure the property is bound, both ways.
            attributes["SelectedOptionChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(model, x),
                        (T)prop.GetValue(model)
                        )
                    )
                );

            // Render the property as a MudRadioGroup control.
            index = builder.RenderUIComponent<MudRadioGroup<T>>(
                index++,
                attributes: attributes,
                contentDelegate: childBuilder => 
                {
                    // Split the optional attributes.
                    var colors = attribute.Colors.Split(',');
                    var disabled = attribute.Disabled.Split(',');
                    var x = 0;

                    // Loop through the options.
                    var options = attribute.Options.Split(',');
                    foreach (var option in options)
                    {
                        var index2 = index; // Reset the index.

                        // Create attributes for the radio button.
                        var buttonAttributes = new Dictionary<string, object>()
                        {
                            { "Option", option }
                        };

                        // Should we copy the dense flag?
                        if (attributes.ContainsKey("Dense"))
                        {
                            // Copy the attribute.
                            buttonAttributes["Dense"] = attributes["Dense"];
                        }

                        // Should we copy the disable ripple flag?
                        if (attributes.ContainsKey("DisableRipple"))
                        {
                            // Copy the attribute.
                            buttonAttributes["DisableRipple"] = attributes["DisableRipple"];
                        }

                        // Should we copy the placement?
                        if (attributes.ContainsKey("Placement"))
                        {
                            // Copy the attribute.
                            buttonAttributes["Placement"] = attributes["Placement"];
                        }

                        // Should we copy the size?
                        if (attributes.ContainsKey("Size"))
                        {
                            // Copy the attribute.
                            buttonAttributes["Size"] = attributes["Size"];
                        }

                        // Should we copy the style?
                        if (attributes.ContainsKey("Style"))
                        {
                            // Copy the attribute.
                            buttonAttributes["Style"] = attributes["Style"];
                        }

                        // Should we copy the user attributes?
                        if (attributes.ContainsKey("UserAttributes"))
                        {
                            // Copy the attribute.
                            buttonAttributes["UserAttributes"] = attributes["UserAttributes"];
                        }

                        // Do we have a color for this radio button?
                        if (x < colors.Length)
                        {
                            if (Enum.TryParse<Color>(colors[x], out var colorValue))
                            {
                                // Set the color attribute.
                                buttonAttributes["Color"] = colorValue;
                            }
                        }

                        // Do we have a disabled flag for this radio button?
                        if (x < disabled.Length)
                        {
                            if (bool.TryParse(disabled[x], out var disabledValue))
                            {
                                // Set the disabled attribute.
                                buttonAttributes["Disabled"] = disabledValue;
                            }
                        }

                        // Render the mudradio control.
                        index2 = childBuilder.RenderUIComponent<MudRadio<T>>(
                            index2++,
                            attributes: buttonAttributes,
                            contentDelegate: grandChildBuilder =>
                            {
                                // Render the radio button's content.
                                grandChildBuilder.AddContent(
                                    index2++,
                                    option
                                    );
                            });

                        // Track which button we're rendering.
                        x++;
                    }
                });

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudSwitch{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified model property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudSwitch<T>(
            this RenderTreeBuilder builder,
            int index,
            RenderMudSwitchAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Did we not override the label?
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Ensure the property value is set.
            attributes["Checked"] = (T)prop.GetValue(model);

            // Ensure the property is bound, both ways.
            attributes["CheckedChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(model, x),
                        (T)prop.GetValue(model)
                        )
                    )
                );

            // Render the property as a MudSwitch control.
            index = builder.RenderUIComponent<MudSwitch<T>>(
                index++,
                attributes: attributes
                );

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudTextField{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified model property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudTextField<T>(
            this RenderTreeBuilder builder,
            int index,
            RenderMudTextFieldAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Did we not override the label?
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Ensure the property value is set.
            attributes["Value"] = (T)prop.GetValue(model);

            // Ensure the property is bound, both ways.
            attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(model, x),
                        (T)prop.GetValue(model)
                        )
                    )
                );

            // Make the compiler happy.
            if (null != model)
            {
                // Ensure the For property value is set.
                attributes["For"] = Expression.Lambda<Func<T>>(
                    MemberExpression.Property(
                        Expression.Constant(
                            model,
                            model.GetType()),
                        prop.Name
                        )
                    );
            }            

            // Render the property as a MudTextField control.
            index = builder.RenderUIComponent<MudTextField<T>>(
                index++,
                attributes: attributes
                );

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudDatePicker"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified model property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudDatePicker(
            this RenderTreeBuilder builder,
            int index,
            RenderMudDatePickerAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            ) 
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Did we not override the label?
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Are we binding to a datetime?
            if (prop.PropertyType == typeof(DateTime) ||
                prop.PropertyType == typeof(Nullable<DateTime>))
            {
                // Ensure the property value is set.
                attributes["Date"] = (DateTime?)prop.GetValue(model);

                // Ensure the property is bound, both ways.
                attributes["DateChanged"] = RuntimeHelpers.TypeCheck<EventCallback<DateTime?>>(
                    EventCallback.Factory.Create<DateTime?>(
                        eventTarget,
                        EventCallback.Factory.CreateInferred<DateTime?>(
                            eventTarget,
                            x => prop.SetValue(model, x),
                            (DateTime?)prop.GetValue(model)
                            )
                        )
                    );
            }

            // Is the type unknown?
            else
            {
                // Panic!!
                throw new FormGenerationException(
                    message: $"Failed to bind a MudTimePicker to property: " +
                        $"'{prop.Name}', of type '{prop.PropertyType.Name}'"
                    );
            }

            // Render the property as a MudDatePicker control.
            index = builder.RenderUIComponent<MudDatePicker>(
                index++,
                attributes: attributes
                );

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudTimePicker"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified model property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudTimePicker(
            this RenderTreeBuilder builder,
            int index,
            RenderMudTimePickerAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Did we not override the label?
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Are we binding to a timespan?
            if (prop.PropertyType == typeof(TimeSpan) ||
                prop.PropertyType == typeof(Nullable<TimeSpan>))
            {
                // Ensure the property value is set.
                attributes["Time"] = (TimeSpan?)prop.GetValue(model);

                // Ensure the property is bound, both ways.
                attributes["TimeChanged"] = RuntimeHelpers.TypeCheck<EventCallback<TimeSpan?>>(
                    EventCallback.Factory.Create<TimeSpan?>(
                        eventTarget,
                        EventCallback.Factory.CreateInferred<TimeSpan?>(
                            eventTarget,
                            x => prop.SetValue(model, x),
                            (TimeSpan?)prop.GetValue(model)
                            )
                        )
                    );
            }

            // Is the type unknown?
            else
            {
                // Panic!!
                throw new FormGenerationException(
                    message: $"Failed to bind a MudTimePicker to property: " +
                        $"'{prop.Name}', of type '{prop.PropertyType.Name}'"
                    );
            }

            // Render the property as a MudTimePicker control.
            index = builder.RenderUIComponent<MudTimePicker>(
                index++,
                attributes: attributes
                );

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudColorPicker"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified model property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudColorPicker(
            this RenderTreeBuilder builder,
            int index,
            RenderMudColorPickerAttribute attribute,
            object model,
            PropertyInfo prop,
            IHandleEvent eventTarget
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(model, nameof(model))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Did we not override the label?
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Are we binding to a MudColor?
            if (prop.PropertyType == typeof(MudColor))
            {
                // Ensure the property value is set.
                attributes["Value"] = (MudColor)prop.GetValue(model);

                // Ensure the property is bound, both ways.
                attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<MudColor>>(
                    EventCallback.Factory.Create<MudColor>(
                        eventTarget,
                        EventCallback.Factory.CreateInferred<MudColor>(
                            eventTarget,
                            x => prop.SetValue(model, x),
                            (MudColor)prop.GetValue(model)
                            )
                        )
                    );
            }

            // Is the type unknown?
            else
            {
                // Panic!!
                throw new FormGenerationException(
                    message: $"Failed to bind a MudColorPicker to property: " +
                        $"'{prop.Name}', of type '{prop.PropertyType.Name}'"
                    );
            }

            // Render the property as a MudColorPicker control.
            index = builder.RenderUIComponent<MudColorPicker>(
                index++,
                attributes: attributes
                );

            // Return the index.
            return index;
        }

        #endregion
    }
}
