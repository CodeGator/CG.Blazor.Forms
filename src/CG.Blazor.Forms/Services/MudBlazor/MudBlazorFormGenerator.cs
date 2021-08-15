using CG.Blazor.Forms.Attributes;
using CG.Blazor.Forms.Services;
using CG.Validations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor.Utilities;
using System;
using System.Linq;
using System.Reflection;

namespace MudBlazor
{
    /// <summary>
    /// This class is a MudBlazor specific implementation of the <see cref="IFormGenerator"/>
    /// interface.
    /// </summary>
    internal class MudBlazorFormGenerator : IFormGenerator
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
        public virtual void Generate(
            RenderTreeBuilder builder,
            IHandleEvent eventTarget,
            object viewModel
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfNull(eventTarget, nameof(eventTarget))
                .ThrowIfNull(viewModel, nameof(viewModel));

            var index = 0;
            try
            {
                // Get the type of the viewModel.
                var propValueType = viewModel?.GetType();

                // Should we render a data validator for the form?
                var validatorAttr = propValueType?.GetCustomAttribute<DataAnnotationsValidatorAttribute>();
                if (null != validatorAttr)
                {
                    // Render the tag in the form.
                    builder.RenderDataAnnotationsValidator(index++);
                }

                // Render the viewModel.
                index = RenderObjectWrapped(
                    builder,
                    index,
                    eventTarget,
                    viewModel,
                    null, // <-- null to render the viewModel itself.
                    viewModel
                    );
            }
            catch (Exception ex)
            {
                // Give the error more context.
                throw new FormGenerationException(
                    message: $"Failed to render the '{viewModel?.GetType().Name}' " +
                        $"viewModel instance. See inner exception(s) for more detail.",
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
        /// This method optionally wraps an object property before recursively 
        /// iterating through all the public properties and rendering each one.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="propValue">The propValue to use for the operation.</param>
        /// <param name="prop">The property info to use for the operation.</param>
        /// <param name="viewModel">The viewModel to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private int RenderObjectWrapped(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Sanity check the object reference.
            if (null == propValue)
            {
                return index; // Nothing to render.
            }

            // Are we rendering a child property?
            if (null != prop)
            {
                // Should we ignore the property?
                var objectAttr = prop.GetCustomAttribute<RenderObjectAttribute>();
                if (null == objectAttr)
                {
                    return index; // Nothing to render.
                }

                // Should we wrap things in a MuddyGroupBox?
                var muddyGroupBoxAttr = prop.GetCustomAttribute<RenderMuddyGroupBoxAttribute>();
                if (null != muddyGroupBoxAttr)
                {
                    // Render the MuddyGroupBox control.
                    index = builder.RenderMuddyGroupBox(
                        index,
                        eventTarget,
                        muddyGroupBoxAttr,
                        propValue,
                        prop,
                        viewModel,
                        childBuilder =>
                        {
                            var index2 = index; // Reset the index.

                            // Render the child content.
                            index2 = RenderObject(
                                childBuilder,
                                index2++,
                                eventTarget,
                                propValue,
                                prop,
                                viewModel
                                );
                        });

                    // Return the index.
                    return index;
                }

                // Should we wrap things in a MudPaper?
                var mudPaperAttr = prop.GetCustomAttribute<RenderMudPaperAttribute>();
                if (null != mudPaperAttr)
                {
                    // Should we setup a default style for the mudpaper?
                    if (string.IsNullOrEmpty(mudPaperAttr.Style))
                    {
                        // Visually, this mudpaper is acting like a groupbox,
                        //   in Windows, so we'll give it some style here to
                        //   make it look a little better in that role.
                        mudPaperAttr.Style = "margin: 2px";
                    }

                    // Render the MudPaper control.
                    index = builder.RenderMudPaper(
                        index,
                        eventTarget,
                        mudPaperAttr,
                        propValue,
                        prop,
                        viewModel,
                        childBuilder =>
                        {
                            var index2 = index; // Reset the index.

                            // Render the child content.
                            index2 = RenderObject(
                                childBuilder,
                                index2++,
                                eventTarget,
                                propValue,
                                prop,
                                viewModel
                                );
                        });

                    // Return the index.
                    return index;
                }
            }

            // If we get here then we should render without any wrapper.

            // Render the child content.
            index = RenderObject(
                builder,
                index,
                eventTarget,
                propValue,
                prop,
                viewModel
                );

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method iterates over all the public properties on the specified
        /// object and renders each one.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="propValue">The propValue to use for the operation.</param>
        /// <param name="prop">The property info to use for the operation.</param>
        /// <param name="viewModel">The viewModel to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private int RenderObject(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
                // Get the child properties on the propValue.
                var childProps = (null == prop)
                    ? propValue?.GetType().GetProperties().Where(x => x.CanWrite && x.CanRead)
                    : prop.PropertyType.GetProperties().Where(x => x.CanWrite && x.CanRead);

                // Did we find any properties?
                if (null != childProps)
                {
                    // Loop through the child properties.
                    foreach (var childProp in childProps)
                    {
                        // TODO : deal with collection types, at some point.

                        // Is the property a 'primitive' type?
                        if (childProp.PropertyType.IsPrimitive ||
                            childProp.PropertyType == typeof(string) ||
                            childProp.PropertyType == typeof(decimal) ||
                            childProp.PropertyType == typeof(DateTime) ||
                            childProp.PropertyType == typeof(Nullable<DateTime>) ||
                            childProp.PropertyType == typeof(TimeSpan) ||
                            childProp.PropertyType == typeof(Nullable<TimeSpan>) ||
                            childProp.PropertyType == typeof(MudColor))
                        {
                            // Render the property as a 'primitive'.
                            index = RenderPrimitives(
                                builder,
                                index++,
                                eventTarget,
                                propValue,
                                childProp,
                                viewModel
                                );
                        }

                        // Is the property an object type?
                        else if (childProp.PropertyType.IsClass)
                        {
                            // Get the value of the property - we'll need
                            //  this value in place of the propValue reference,
                            //  when we start rendering any child properties.
                            var childValue = childProp.GetValue(propValue);

                            // Anything to render?
                            if (null != propValue)
                            {
                                // Render the properties on the object.
                                index = RenderObjectWrapped(
                                    builder,
                                    index++,
                                    eventTarget,
                                    childValue,
                                    childProp,
                                    viewModel
                                    );
                            }
                        }

                        // Is the property an unknown type?
                        else
                        {
                            // Panic!!
                            throw new FormGenerationException(
                                message: $"Failed to render property: '{childProp.Name}', " +
                                    $"of type: '{childProp.PropertyType.Name}'. The form " +
                                    $"generator doesn't have logic to deal with this type."
                                );
                        }
                    }
                }

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders the specified property as a primitive.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property info to use for the operation.</param>
        /// <param name="viewModel">The viewModel to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        ///// chaining calls together.</returns>
        private static int RenderPrimitives(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Is the property of type string?
            if (prop.PropertyType == typeof(string))
            {
                // Render the string property.
                index = RenderString(
                    builder,
                    index,
                    eventTarget,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Is the property of type numeric?
            else if (prop.PropertyType == typeof(int) ||
                     prop.PropertyType == typeof(long) ||
                     prop.PropertyType == typeof(decimal) ||
                     prop.PropertyType == typeof(float) ||
                     prop.PropertyType == typeof(double) ||
                     prop.PropertyType == typeof(byte))
            {
                // Render the numeric property.
                index = RenderNumeric(
                    builder,
                    index,
                    eventTarget,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Is the property of type bool?
            else if (prop.PropertyType == typeof(bool))
            {
                // Render the boolean property.
                index = RenderBool(
                    builder,
                    index,
                    eventTarget,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Is the property of type datetime?
            else if (prop.PropertyType == typeof(DateTime) ||
                     prop.PropertyType == typeof(Nullable<DateTime>))
            {
                // Render the datetime property.
                index = RenderDateTime(
                    builder,
                    index,
                    eventTarget,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Is the property of type timespan?
            else if (prop.PropertyType == typeof(TimeSpan) ||
                     prop.PropertyType == typeof(Nullable<TimeSpan>))
            {
                // Render the timespan property.
                index = RenderTimeSpan(
                    builder,
                    index,
                    eventTarget,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Is the property of type MudColor?
            else if (prop.PropertyType == typeof(MudColor))
            {
                // Render the mudcolor property.
                index = RenderColor(
                    builder,
                    index,
                    eventTarget,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders the specified boolean property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property info to use for the operation.</param>
        /// <param name="viewModel">The viewModel to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderBool(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Should we render the property as a MudCheckBox?
            var mudCheckBoxAttr = prop.GetCustomAttribute<RenderMudCheckBoxAttribute>();
            if (null != mudCheckBoxAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudCheckBox<bool>(
                    index,
                    eventTarget,
                    mudCheckBoxAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Should we render the property as a MudSwitch?
            var mudSwitchAttr = prop.GetCustomAttribute<RenderMudSwitchAttribute>();
            if (null != mudSwitchAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudSwitch<bool>(
                    index,
                    eventTarget,
                    mudSwitchAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders the specified property as a numeric field.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property info to use for the operation.</param>
        /// <param name="viewModel">The viewModel to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderNumeric(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Should we render a slider?
            var mudSliderAttr = prop.GetCustomAttribute<RenderMudSliderAttribute>();
            if (null != mudSliderAttr)
            {
                // Is the property an integer?
                if (prop.PropertyType == typeof(int))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudSlider<int>(
                        index,
                        eventTarget,
                        mudSliderAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a long integer?
                else if (prop.PropertyType == typeof(long))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudSlider<long>(
                        index,
                        eventTarget,
                        mudSliderAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a decimal?
                else if (prop.PropertyType == typeof(decimal))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudSlider<decimal>(
                        index,
                        eventTarget,
                        mudSliderAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a float?
                else if (prop.PropertyType == typeof(float))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudSlider<float>(
                        index,
                        eventTarget,
                        mudSliderAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a double?
                else if (prop.PropertyType == typeof(double))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudSlider<double>(
                        index,
                        eventTarget,
                        mudSliderAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a byte?
                else if (prop.PropertyType == typeof(byte))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudSlider<byte>(
                        index,
                        eventTarget,
                        mudSliderAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }
            }

            // Should we render a numeric?
            var mudNumericFieldAttr = prop.GetCustomAttribute<RenderMudNumericFieldAttribute>();
            if (null != mudNumericFieldAttr)
            {
                // Is the property an integer?
                if (prop.PropertyType == typeof(int))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<int>(
                        index,
                        eventTarget,
                        mudNumericFieldAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a long integer?
                else if (prop.PropertyType == typeof(long))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<long>(
                        index,
                        eventTarget,
                        mudNumericFieldAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a decimal?
                else if (prop.PropertyType == typeof(decimal))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<decimal>(
                        index,
                        eventTarget,
                        mudNumericFieldAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a float?
                else if (prop.PropertyType == typeof(float))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<float>(
                        index,
                        eventTarget,
                        mudNumericFieldAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a double?
                else if (prop.PropertyType == typeof(double))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<double>(
                        index,
                        eventTarget,
                        mudNumericFieldAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }

                // Is the property a byte?
                else if (prop.PropertyType == typeof(byte))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<byte>(
                        index,
                        eventTarget,
                        mudNumericFieldAttr,
                        propValue,
                        prop,
                        viewModel
                        );
                }
            }

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders the specified string property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property info to use for the operation.</param>
        /// <param name="viewModel">The viewModel to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderString(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Should we render the property inside a text field?
            var mudTextFieldAttr = prop.GetCustomAttribute<RenderMudTextFieldAttribute>();
            if (null != mudTextFieldAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudTextField<string>(
                    index,
                    eventTarget,
                    mudTextFieldAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Should we render the property inside a select field?
            var mudSelectFieldAttr = prop.GetCustomAttribute<RenderMudSelectAttribute>();
            if (null != mudSelectFieldAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudSelect<string>(
                    index,
                    eventTarget,
                    mudSelectFieldAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Should we render the property inside an auto complete field?
            var mudAutocompleteAttr = prop.GetCustomAttribute<
                RenderMudAutocompleteAttribute
                >();
            if (null != mudAutocompleteAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudAutocomplete<string>(
                    index,
                    eventTarget,
                    mudAutocompleteAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Should we render the property inside a radio group?
            var mudRadioGroupAttr = prop.GetCustomAttribute<RenderMudRadioGroupAttribute>();
            if (null != mudRadioGroupAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudRadioGroup<string>(
                    index,
                    eventTarget,
                    mudRadioGroupAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }
            
            // Should we render the property inside an alert?
            var mudAlertAttr = prop.GetCustomAttribute<RenderMudAlertAttribute>();
            if (null != mudAlertAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudAlert(
                    index,
                    eventTarget,
                    mudAlertAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }
                        
            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders the specified datetime property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property info to use for the operation.</param>
        /// <param name="viewModel">The viewModel to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderDateTime(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Should we render the property inside a date picker?
            var mudDatePickerAttr = prop.GetCustomAttribute<RenderMudDatePickerAttribute>();
            if (null != mudDatePickerAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudDatePicker(
                    index,
                    eventTarget,
                    mudDatePickerAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders the specified timespan property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property info to use for the operation.</param>
        /// <param name="viewModel">The viewModel to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderTimeSpan(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Should we render the property inside a time picker?
            var mudTimePickerAttr = prop.GetCustomAttribute<RenderMudTimePickerAttribute>();
            if (null != mudTimePickerAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudTimePicker(
                    index,
                    eventTarget,
                    mudTimePickerAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Return the index.
            return index;
        }

        // *******************************************************************

        /// <summary>
        /// This method renders the specified color property.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="propValue">The property value to use for the operation.</param>
        /// <param name="prop">The property info to use for the operation.</param>
        /// <param name="viewModel">The viewModel to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderColor(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object propValue,
            PropertyInfo prop,
            object viewModel
            )
        {
            // Should we render the property inside a color picker?
            var mudColorPickerAttr = prop.GetCustomAttribute<RenderMudColorPickerAttribute>();
            if (null != mudColorPickerAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudColorPicker(
                    index,
                    eventTarget,
                    mudColorPickerAttr,
                    propValue,
                    prop,
                    viewModel
                    );
            }

            // Return the index.
            return index;
        }

        #endregion
    }
}
