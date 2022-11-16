
namespace CG.Blazor.Forms.Attributes;

/// <summary>
/// This class is an attribute that, when applied to a numeric property, 
/// causes the form generator to render the property as a progress HTML element.
/// </summary>
/// <remarks>
/// <para>
/// This attribute is only valid when placed on a property of type: numeric.
/// Which means, byte, int, long, float, double, or decimal.
/// </para>
/// </remarks>
/// <example>
/// Here is an example of decorating a model property to render a progress:
/// <code>
/// using CG.Blazor.Forms.Attributes;
/// class MyModel
/// {
///     [RenderMeter]
///     public float MyProperty { get;set; }
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Property)]
public class RenderProgressAttribute : HtmlAttribute
{
    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This property contains the lower numeric bound of the high end of the 
    /// measured range. This must be less than the maximum value (max attribute), 
    /// and it also must be greater than the low value and minimum value (low 
    /// attribute and min attribute, respectively), if any are specified. If 
    /// unspecified, or if greater than the maximum value, the high value is 
    /// equal to the maximum value.
    /// </summary>
    public string High { get; set; }

    /// <summary>
    /// This property contains the upper numeric bound of the low end of the 
    /// measured range. This must be greater than the minimum value (min attribute), 
    /// and it also must be less than the high value and maximum value (high attribute 
    /// and max attribute, respectively), if any are specified. If unspecified, or if 
    /// less than the minimum value, the low value is equal to the minimum value.
    /// </summary>
    public string Low { get; set; }

    /// <summary>
    /// This property indicates the optimal numeric value. It must be within the range 
    /// (as defined by the min attribute and max attribute). When used with the low 
    /// attribute and high attribute, it gives an indication where along the range is 
    /// considered preferable. For example, if it is between the min attribute and the 
    /// low attribute, then the lower range is considered preferred. The browser may 
    /// color the meter's bar differently depending on whether the value is less than 
    /// or equal to the optimum value.
    /// </summary>
    public string Optimum { get; set; }

    /// <summary>
    /// This property indicates the upper numeric bound of the measured range. 
    /// This must be greater than the minimum value (min attribute), if specified. 
    /// If unspecified, the maximum value is 1.
    /// </summary>
    public string Max { get; set; }

    /// <summary>
    /// This property indicates the lower numeric bound of the measured range. 
    /// This must be less than the maximum value (max attribute), if specified. 
    /// If unspecified, the minimum value is 0.
    /// </summary>
    public string Min { get; set; }

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
    /// This constructor creates a new instance of <see cref="RenderProgressAttribute"/>
    /// class.
    /// </summary>
    public RenderProgressAttribute()
    {
        // Set default values.
        High = string.Empty;
        Low = string.Empty;
        Max = string.Empty;
        Min = string.Empty;
        Optimum = string.Empty;
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
        if (false == string.IsNullOrEmpty(High))
        {
            // Add the property value.
            attr[nameof(High)] = High;
        }

        // Does this property have a non-default value?
        if (false == string.IsNullOrEmpty(Low))
        {
            // Add the property value.
            attr[nameof(Low)] = Low;
        }

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

        // Does this property have a non-default value?
        if (false == string.IsNullOrEmpty(Optimum))
        {
            // Add the property value.
            attr[nameof(Optimum)] = Optimum;
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
            // If we get here then we are trying to render a progress HTML element
            //   and bind it to the specified numeric property.

            // Should never happen, but, pffft, check it anyway.
            if (path.Count < 2)
            {
                // Let the world know what we're doing.
                logger.LogDebug(
                    "RenderProgressAttribute::Generate called with a shallow path!"
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
                    "RenderProgressAttribute::Generate called with a null model!"
                    );

                // Return the index.
                return index;
            }

            // Get the model's type.
            var modelType = model.GetType();

            // Get the property's parent.
            var propParent = path.Skip(1).First();

            // We only render progress controls against numeric types.
            if (modelType == typeof(byte) ||
                modelType == typeof(int) ||
                modelType == typeof(long) ||
                modelType == typeof(float) ||
                modelType == typeof(double) ||
                modelType == typeof(decimal))
            {
                // Let the world know what we're doing.
                logger.LogDebug(
                    "Rendering property: '{PropName}' as a progress element.",
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
                                "progress",
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
                    "Not rendering property: '{PropName}' on: '{ObjName}' " +
                    "because we only render progress elements on properties " +
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
                message: "Failed to render a progress element! " +
                    "See inner exception(s) for more detail.",
                innerException: ex
                );
        }
    }

    #endregion
}
