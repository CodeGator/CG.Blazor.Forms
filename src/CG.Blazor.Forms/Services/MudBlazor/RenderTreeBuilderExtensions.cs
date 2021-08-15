using CG.Blazor.Forms.Services;
using CG.Blazor.Forms.Components;
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
        /// specified viewModel property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudAlert(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudAlertAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget))
                .ThrowIfNull(viewModel, nameof(viewModel));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();
                        
            // Render the property as a MudTextField control.
            index = builder.RenderUIComponent<MudAlert>(
                index++,
                attributes: attributes,
                contentDelegate: childBuilder =>
                {
                    // Add the child content.
                    childBuilder.AddContent(
                        index++, 
                        (string)prop.GetValue(propValue)
                        );
                });

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudAutocomplete{T}"/> object into 
        /// the specified <see cref="RenderTreeBuilder"/>.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudAutocomplete<T>(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudAutocompleteAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(eventTarget, nameof(eventTarget))
                .ThrowIfNull(viewModel, nameof(viewModel));

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
                    // If we get here then we need to go find a search method,
                    //   on either the property, or the view-model, that corresponds
                    //   with the method named in the attribute.

                    // Create possible targets for the search.
                    var targets = (viewModel == propValue)
                        ? new[] { viewModel }
                        : new[] { viewModel, propValue }; 

                    // Loop and look for the search function.
                    foreach (var target in targets)
                    {
                        // Get the target type.
                        var targetType = target.GetType();

                        // Look for the named search method.
                        var methodInfo = targetType.GetMethod(
                            methodName,
                            BindingFlags.Public | 
                            BindingFlags.NonPublic | 
                            BindingFlags.Instance
                            );

                        // Did we succeed?
                        if (null != methodInfo)
                        {
                            // Create a viewModel reference expression.
                            var viewModelExp = Expression.Constant(
                                target
                                );

                            // Create a parameter expression.
                            var p1 = Expression.Parameter(
                                typeof(T),
                                "p1"
                                );

                            // Create the method call expression.
                            var callExp = Expression.Call(
                                viewModelExp,
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

                            // We found the search function so stop looking for it.
                            break;
                        }
                    }

                    // If the search attribute is still a string, after the loop above, then
                    //    we failed to locate the named search function.
                    if (attributes["SearchFunc"] is string)
                    {
                        // If we get here then we failed to find the specified search function,
                        //   so, nothing left to do but drop back and punt.

                        // How many places did we look?
                        if (targets.Length == 1)
                        {
                            // Panic!!
                            throw new FormGenerationException(
                                message: $"Unable to locate an autocomplete search method " +
                                    $"named: '{methodName}' on the view-model type: " +
                                    $"'{viewModel.GetType().Name}' for the property named: " +
                                    $"'{prop.Name}'."
                                );
                        }
                        else
                        {
                            // Panic!!
                            throw new FormGenerationException(
                                message: $"Unable to locate an autocomplete search method " +
                                    $"named: '{methodName}' on either the view-model type: " +
                                    $"'{viewModel.GetType().Name}' OR the propery type: " +
                                    $"'{propValue.GetType().Name}' for the property named: " +
                                    $"'{prop.Name}'."
                                );
                        }
                    }
                }
            }

            // Ensure the Value property value is set.
            attributes["Value"] = (T)prop.GetValue(propValue);

            // Ensure the ValueChanged property is bound, both ways.
            attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(propValue, x),
                        (T)prop.GetValue(propValue)
                        )
                    )
                );

            // Make the compiler happy.
            if (null != propValue)
            {
                // Ensure the For property value is set.
                attributes["For"] = Expression.Lambda<Func<T>>(
                    MemberExpression.Property(
                        Expression.Constant(
                            propValue,
                            propValue.GetType()),
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
        /// specified viewModel property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudCheckBox<T>(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudCheckBoxAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
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
            attributes["Checked"] = (T)prop.GetValue(propValue);

            // Ensure the CheckedChanged property is bound, both ways.
            attributes["CheckedChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(propValue, x),
                        (T)prop.GetValue(propValue)
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
        /// This method renders a <see cref="MudSlider{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified viewModel property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudSlider<T>(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudSliderAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Ensure the Label property is set.
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Ensure the Value property value is set.
            attributes["Value"] = (T)prop.GetValue(propValue);

            // Ensure the ValueChanged property is bound, both ways.
            attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(propValue, x),
                        (T)prop.GetValue(propValue)
                        )
                    )
                );

            // Render the property as a MudSlider control.
            index = builder.RenderUIComponent<MudSlider<T>>(
                index++,
                attributes: attributes,
                contentDelegate: childBuilder =>
                {
                    var index2 = index; // Reset the index.

                    // Render the label.
                    childBuilder.AddContent(index2++, attributes["Label"]);
                });

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudSelect{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified viewModel property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudSelect<T>(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudSelectAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Ensure the Label property is set.
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Ensure the T attribute is set.
            attributes["T"] = typeof(T).Name;

            // Ensure the property value is set.
            attributes["Value"] = (T)prop.GetValue(propValue);

            // Ensure the property is bound, both ways.
            attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(propValue, x),
                        (T)prop.GetValue(propValue)
                        )
                    )
                );

            // Make the compiler happy.
            if (null != propValue)
            {
                // Ensure the For property value is set.
                attributes["For"] = Expression.Lambda<Func<T>>(
                    MemberExpression.Property(
                        Expression.Constant(
                            propValue,
                            propValue.GetType()),
                        prop.Name
                        )
                    );
            }

            // Render the property as a MudSelect control.
            index = builder.RenderUIComponent<MudSelect<T>>(
                index++,
                attributes: attributes,
                contentDelegate: childBuilder =>
                {
                    // Split the options.
                    var options = attribute.Options.Split(',');

                    // Loop through the options
                    foreach (var option in options)
                    {
                        var index2 = index; // Reset the index.

                        // Create attributes for the item.
                        var selectItemAttributes = new Dictionary<string, object>()
                        {
                            { "Value", option },
                            { "T", attributes["T"] }
                        };

                        // Render the MudSelectItem control.
                        index2 = childBuilder.RenderUIComponent<MudSelectItem<T>>(
                            index2++,
                            attributes: selectItemAttributes
                            );
                    }
                });

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudNumericField{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified viewModel property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudNumericField<T>(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudNumericFieldAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
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
            attributes["Value"] = (T)prop.GetValue(propValue);

            // Ensure the Value property is bound, both ways.
            attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(propValue, x),
                        (T)prop.GetValue(propValue)
                        )
                    )
                );

            // Make the compiler happy.
            if (null != propValue)
            {
                // Ensure the For property value is set.
                attributes["For"] = Expression.Lambda<Func<T>>(
                    MemberExpression.Property(
                        Expression.Constant(
                            propValue,
                            propValue.GetType()),
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
        /// This method renders a <see cref="MudPaper"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified viewModel property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <param name="contentDelegate">The delegate for rendering child content.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudPaper(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudPaperAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel,
            Action<RenderTreeBuilder> contentDelegate
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Render the property as a MudPaper control.
            index = builder.RenderUIComponent<MudPaper>(
                index++,
                attributes: attributes,
                contentDelegate: childBuilder =>
                    contentDelegate(childBuilder)
                    );

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MuddyGroupBox"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified viewModel property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <param name="contentDelegate">The delegate for rendering child content.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMuddyGroupBox(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMuddyGroupBoxAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel,
            Action<RenderTreeBuilder> contentDelegate
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Did we not override the label?
            if (false == attributes.ContainsKey("Label"))
            {
                // Ensure we have a label.
                attributes["Label"] = prop.Name;
            }

            // Render the property as a MuddyGroupBox control.
            index = builder.RenderUIComponent<MuddyGroupBox>(
                index++,
                attributes: attributes,
                contentDelegate: childBuilder =>
                    contentDelegate(childBuilder)
                    );

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders a <see cref="MudRadioGroup{T}"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified viewModel property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudRadioGroup<T>(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudRadioGroupAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Ensure the value is set.
            attributes["SelectedOption"] = (T)prop.GetValue(propValue);

            // Ensure the property is bound, both ways.
            attributes["SelectedOptionChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(propValue, x),
                        (T)prop.GetValue(propValue)
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
        /// specified viewModel property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudSwitch<T>(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudSwitchAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
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
            attributes["Checked"] = (T)prop.GetValue(propValue);

            // Ensure the property is bound, both ways.
            attributes["CheckedChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(propValue, x),
                        (T)prop.GetValue(propValue)
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
        /// specified viewModel property.
        /// </summary>
        /// <typeparam name="T">The type to associate with the control.</typeparam>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudTextField<T>(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudTextFieldAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
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
            attributes["Value"] = (T)prop.GetValue(propValue);

            // Ensure the property is bound, both ways.
            attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<T>>(
                EventCallback.Factory.Create<T>(
                    eventTarget,
                    EventCallback.Factory.CreateInferred<T>(
                        eventTarget,
                        x => prop.SetValue(propValue, x),
                        (T)prop.GetValue(propValue)
                        )
                    )
                );

            // Make the compiler happy.
            if (null != propValue)
            {
                // Ensure the For property value is set.
                attributes["For"] = Expression.Lambda<Func<T>>(
                    MemberExpression.Property(
                        Expression.Constant(
                            propValue,
                            propValue.GetType()),
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
        /// This method renders a <see cref="MudField"/> object into the 
        /// specified <see cref="RenderTreeBuilder"/> with bindings to the 
        /// specified viewModel property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudField(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudFieldAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
                .ThrowIfNull(eventTarget, nameof(eventTarget));

            // Get any non-default attribute values (overrides).
            var attributes = attribute.ToAttributes();

            // Ensure the property label is set.
            attributes["Label"] = (string)prop.GetValue(propValue);

            // Render the property as a MudField control.
            index = builder.RenderUIComponent<MudField>(
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
        /// specified viewModel property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudDatePicker(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudDatePickerAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            ) 
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
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
                attributes["Date"] = (DateTime?)prop.GetValue(propValue);

                // Ensure the property is bound, both ways.
                attributes["DateChanged"] = RuntimeHelpers.TypeCheck<EventCallback<DateTime?>>(
                    EventCallback.Factory.Create<DateTime?>(
                        eventTarget,
                        EventCallback.Factory.CreateInferred<DateTime?>(
                            eventTarget,
                            x => prop.SetValue(propValue, x),
                            (DateTime?)prop.GetValue(propValue)
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
        /// specified viewModel property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudTimePicker(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudTimePickerAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
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
                attributes["Time"] = (TimeSpan?)prop.GetValue(propValue);

                // Ensure the property is bound, both ways.
                attributes["TimeChanged"] = RuntimeHelpers.TypeCheck<EventCallback<TimeSpan?>>(
                    EventCallback.Factory.Create<TimeSpan?>(
                        eventTarget,
                        EventCallback.Factory.CreateInferred<TimeSpan?>(
                            eventTarget,
                            x => prop.SetValue(propValue, x),
                            (TimeSpan?)prop.GetValue(propValue)
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
        /// specified viewModel property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The target for any events.</param>
        /// <param name="attribute">The attribute to use for the operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property to use for the operation.</param>
        /// <param name="viewModel">The view model to use for the operation.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderMudColorPicker(
            this RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            RenderMudColorPickerAttribute attribute,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfLessThanZero(index, nameof(index))
                .ThrowIfNull(attribute, nameof(attribute))
                .ThrowIfNull(propValue, nameof(propValue))
                .ThrowIfNull(prop, nameof(prop))
                .ThrowIfNull(viewModel, nameof(viewModel))
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
                attributes["Value"] = (MudColor)prop.GetValue(propValue);

                // Ensure the property is bound, both ways.
                attributes["ValueChanged"] = RuntimeHelpers.TypeCheck<EventCallback<MudColor>>(
                    EventCallback.Factory.Create<MudColor>(
                        eventTarget,
                        EventCallback.Factory.CreateInferred<MudColor>(
                            eventTarget,
                            x => prop.SetValue(propValue, x),
                            (MudColor)prop.GetValue(propValue)
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
