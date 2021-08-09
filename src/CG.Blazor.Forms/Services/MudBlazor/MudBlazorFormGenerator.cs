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
            object model
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfNull(eventTarget, nameof(eventTarget))
                .ThrowIfNull(model, nameof(model));

            var index = 0;
            try
            {
                // Get the type of the model.
                var modelType = model?.GetType();

                // Should we render a data validator?
                var validatorAttr = modelType?.GetCustomAttribute<DataAnnotationsValidatorAttribute>();
                if (null != validatorAttr)
                {
                    // Render the tag in the form.
                    builder.RenderDataAnnotationsValidator(index++);
                }

                // Render the model.
                index = RenderProperties(
                    builder,
                    index,
                    eventTarget,
                    model,
                    null // <-- null to render the model itself.
                    );
            }
            catch (Exception ex)
            {
                // Give the error more context.
                throw new FormGenerationException(
                    message: $"Failed to render the '{model?.GetType().Name}' " +
                        $"model instance. See inner exception(s) for more detail.",
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
        /// This method iterates over all the public properties on the specified
        /// model object and renders each decorated property as an object.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="eventTarget">The event target to use for the 
        /// operation.</param>
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="modelProp">The property info to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private int RenderProperties(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object model,
            PropertyInfo modelProp
            ) 
        {
            // Get the child properties on the model.
            var childProps = (null == modelProp)
                ? model?.GetType().GetProperties().Where(x => x.CanWrite && x.CanRead)
                : modelProp.PropertyType.GetProperties().Where(x => x.CanWrite && x.CanRead);

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
                            model,
                            childProp
                            );
                    }

                    // Is the property an object type?
                    else if (childProp.PropertyType.IsClass)
                    {
                        // Get the value of the property - we'll need
                        //  this value in place of the model reference,
                        //  when we start rendering any child properties.
                        var propValue = childProp.GetValue(model);

                        // Anything to render?
                        if (null != propValue)
                        {
                            // Render the properties on the object.
                            index = RenderProperties(
                                builder,
                                index++,
                                eventTarget,
                                propValue, // <-- in place of the model reference.
                                childProp
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
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="modelProp">The property info to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderPrimitives(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object model,
            PropertyInfo modelProp
            )
        {
            // Is the property of type string?
            if (modelProp.PropertyType == typeof(string))
            {
                // Render the string property.
                index = RenderString(
                    builder,
                    index,
                    eventTarget,
                    model,
                    modelProp
                    );
            }

            // Is the property of type numeric?
            else if (modelProp.PropertyType == typeof(int) ||
                     modelProp.PropertyType == typeof(long) ||
                     modelProp.PropertyType == typeof(decimal) ||
                     modelProp.PropertyType == typeof(float) ||
                     modelProp.PropertyType == typeof(double) ||
                     modelProp.PropertyType == typeof(byte))
            {
                // Render the numeric property.
                index = RenderNumeric(
                    builder,
                    index,
                    eventTarget,
                    model,
                    modelProp
                    );
            }

            // Is the property of type bool?
            else if (modelProp.PropertyType == typeof(bool))
            {
                // Render the boolean property.
                index = RenderBool(
                    builder,
                    index,
                    eventTarget,
                    model,
                    modelProp
                    );
            }

            // Is the property of type datetime?
            else if (modelProp.PropertyType == typeof(DateTime) ||
                     modelProp.PropertyType == typeof(Nullable<DateTime>))
            {
                // Render the datetime property.
                index = RenderDateTime(
                    builder,
                    index,
                    eventTarget,
                    model,
                    modelProp
                    );
            }

            // Is the property of type timespan?
            else if (modelProp.PropertyType == typeof(TimeSpan) ||
                     modelProp.PropertyType == typeof(Nullable<TimeSpan>))
            {
                // Render the timespan property.
                index = RenderTimeSpan(
                    builder,
                    index,
                    eventTarget,
                    model,
                    modelProp
                    );
            }

            // Is the property of type MudColor?
            else if (modelProp.PropertyType == typeof(MudColor))
            {
                // Render the mudcolor property.
                index = RenderColor(
                    builder,
                    index,
                    eventTarget,
                    model,
                    modelProp
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
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="modelProp">The property info to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderBool(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object model,
            PropertyInfo modelProp
            )
        {
            // Should we render the property as a MudCheckBox?
            var mudCheckBoxAttr = modelProp.GetCustomAttribute<RenderMudCheckBoxAttribute>();
            if (null != mudCheckBoxAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudCheckBox<bool>(
                    index,
                    mudCheckBoxAttr,
                    model,
                    modelProp,
                    eventTarget
                    );
            }

            // Should we render the property as a MudSwitch?
            var mudSwitchAttr = modelProp.GetCustomAttribute<RenderMudSwitchAttribute>();
            if (null != mudSwitchAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudSwitch<bool>(
                    index,
                    mudSwitchAttr,
                    model,
                    modelProp,
                    eventTarget
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
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="modelProp">The property info to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderNumeric(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object model,
            PropertyInfo modelProp
            )
        {
            // Should we render a numeric?
            var mudNumericFieldAttr = modelProp.GetCustomAttribute<RenderMudNumericFieldAttribute>();
            if (null != mudNumericFieldAttr)
            {
                // Is the property an integer?
                if (modelProp.PropertyType == typeof(int))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<int>(
                        index,
                        mudNumericFieldAttr,
                        model,
                        modelProp,
                        eventTarget
                        );
                }

                // Is the property a long integer?
                else if (modelProp.PropertyType == typeof(long))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<long>(
                        index,
                        mudNumericFieldAttr,
                        model,
                        modelProp,
                        eventTarget
                        );
                }

                // Is the property a decimal?
                else if (modelProp.PropertyType == typeof(decimal))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<decimal>(
                        index,
                        mudNumericFieldAttr,
                        model,
                        modelProp,
                        eventTarget
                        );
                }

                // Is the property a float?
                else if (modelProp.PropertyType == typeof(float))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<float>(
                        index,
                        mudNumericFieldAttr,
                        model,
                        modelProp,
                        eventTarget
                        );
                }

                // Is the property a double?
                else if (modelProp.PropertyType == typeof(double))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<double>(
                        index,
                        mudNumericFieldAttr,
                        model,
                        modelProp,
                        eventTarget
                        );
                }

                // Is the property a byte?
                else if (modelProp.PropertyType == typeof(byte))
                {
                    // Render the control with bindings to the property.
                    index = builder.RenderMudNumericField<byte>(
                        index,
                        mudNumericFieldAttr,
                        model,
                        modelProp,
                        eventTarget
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
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="modelProp">The property info to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderString(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object model,
            PropertyInfo modelProp
            )
        {
            // Should we render the property inside a text field?
            var mudTextFieldAttr = modelProp.GetCustomAttribute<RenderMudTextFieldAttribute>();
            if (null != mudTextFieldAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudTextField<string>(
                    index,
                    mudTextFieldAttr,
                    model,
                    modelProp,
                    eventTarget
                    );
            }

            // Should we render the property inside an auto complete field?
            var mudAutocompleteAttr = modelProp.GetCustomAttribute<
                RenderMudAutocompleteAttribute
                >();
            if (null != mudAutocompleteAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudAutocomplete<string>(
                    index,
                    mudAutocompleteAttr,
                    model,
                    modelProp,
                    eventTarget
                    );
            }

            // Should we render the property inside a radio group?
            var mudRadioGroupAttr = modelProp.GetCustomAttribute<RenderMudRadioGroupAttribute>();
            if (null != mudRadioGroupAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudRadioGroup<string>(
                    index,
                    mudRadioGroupAttr,
                    model,
                    modelProp,
                    eventTarget
                    );
            }
            
            // Should we render the property inside an alert?
            var mudAlertAttr = modelProp.GetCustomAttribute<RenderMudAlertAttribute>();
            if (null != mudAlertAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudAlert(
                    index,
                    mudAlertAttr,
                    model,
                    modelProp,
                    eventTarget
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
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="modelProp">The property info to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderDateTime(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object model,
            PropertyInfo modelProp
            )
        {
            // Should we render the property inside a date picker?
            var mudDatePickerAttr = modelProp.GetCustomAttribute<RenderMudDatePickerAttribute>();
            if (null != mudDatePickerAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudDatePicker(
                    index,
                    mudDatePickerAttr,
                    model,
                    modelProp,
                    eventTarget
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
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="modelProp">The property info to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderTimeSpan(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object model,
            PropertyInfo modelProp
            )
        {
            // Should we render the property inside a time picker?
            var mudTimePickerAttr = modelProp.GetCustomAttribute<RenderMudTimePickerAttribute>();
            if (null != mudTimePickerAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudTimePicker(
                    index,
                    mudTimePickerAttr,
                    model,
                    modelProp,
                    eventTarget
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
        /// <param name="model">The model to use for the operation.</param>
        /// <param name="modelProp">The property info to use for the operation.</param>
        /// <returns>the value of the <paramref name="builder"/> property, for
        /// chaining calls together.</returns>
        private static int RenderColor(
            RenderTreeBuilder builder,
            int index,
            IHandleEvent eventTarget,
            object model,
            PropertyInfo modelProp
            )
        {
            // Should we render the property inside a color picker?
            var mudColorPickerAttr = modelProp.GetCustomAttribute<RenderMudColorPickerAttribute>();
            if (null != mudColorPickerAttr)
            {
                // Render the control with bindings to the property.
                index = builder.RenderMudColorPicker(
                    index,
                    mudColorPickerAttr,
                    model,
                    modelProp,
                    eventTarget
                    );
            }

            // Return the index.
            return index;
        }

        #endregion
    }
}
