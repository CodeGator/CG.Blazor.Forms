
namespace CG.Blazor.Forms.Attributes;

/// <summary>
/// This class is an attribute that, when applied to a class, causes
/// the form generator to render a <see cref="ValidationSummary"/>
/// tag, withing the form.
/// </summary>
/// <remarks>
/// <para>
/// This attribute is only valid when placed on the class definition for
/// the top-level model class.
/// </para>
/// </remarks>
/// <example>
/// Here is an example of decorating a model class to render a validation
/// summary:
/// <code>
/// using CG.Blazor.Forms.Attributes;
/// 
/// [RenderValidationSummary]
/// class MyModel
/// {
///     
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class RenderValidationSummaryAttribute : FormValidationAttribute
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

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
            // Let the world know what we're doing.
            logger.LogDebug(
                "Rendering a validation summary for the form."
                );

            // Render the validation summary.
            builder.RenderUIComponent<ValidationSummary>(index++);

            // Return the index.
            return index;
        }
        catch (Exception ex)
        {
            // Provide better context for the error.
            throw new FormGenerationException(
                message: "Failed to render a validation summary! " +
                    "See inner exception(s) for more detail.",
                innerException: ex
                );
        }
    }

    #endregion
}
