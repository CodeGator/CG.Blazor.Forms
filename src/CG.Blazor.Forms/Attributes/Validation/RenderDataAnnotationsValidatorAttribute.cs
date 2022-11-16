﻿
namespace CG.Blazor.Forms.Attributes;

/// <summary>
/// This class is an attribute that, when applied to a class, causes
/// the form generator to render a <see cref="DataAnnotationsValidator"/>
/// tag, within the form.
/// </summary>
/// <remarks>
/// <para>
/// This attribute is only valid when placed on the class definition for
/// the top-level model class.
/// </para>
/// </remarks>
/// <example>
/// Here is an example of decorating a model class to render a data annotations
/// validator:
/// <code>
/// using CG.Blazor.Forms.Attributes;
/// 
/// [RenderDataAnnotationsValidator]
/// class MyModel
/// {
///     
/// }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class RenderDataAnnotationsValidatorAttribute : FormValidationAttribute
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
            // Should never happen, but, pffft, check it anyway.
            if (false == path.Any())
            {
                // Let the world know what we're doing.
                logger.LogDebug(
                    "RenderDataAnnotationsValidatorAttribute::Generate called with an empty path!"
                    );

                // Return the index.
                return index;
            }

            // Let the world know what we're doing.
            logger.LogDebug(
                "Rendering a data annotations validator for the '{ObjType}' view-model.",
                path.First().GetType().Name
                );

            // Render the data annotations validator.
            builder.RenderUIComponent<DataAnnotationsValidator>(index++);

            // Return the index.
            return index;
        }
        catch (Exception ex)
        {
            // Provide better context for the error.
            throw new FormGenerationException(
                message: "Failed to render a data annotations validator! " +
                    "See inner exception(s) for more detail.",
                innerException: ex
                );
        }
    }

    #endregion
}
