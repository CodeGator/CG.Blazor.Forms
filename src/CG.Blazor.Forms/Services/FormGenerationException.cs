
namespace CG.Blazor.Forms.Services;

/// <summary>
/// This class represents a form generation exception.
/// </summary>
[Serializable]
public class FormGenerationException : Exception
{
    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="FormGenerationException"/>
    /// class.
    /// </summary>
    public FormGenerationException()
    {

    }

    // *******************************************************************

    /// <summary>
    /// This constructor creates a new instance of the <see cref="FormGenerationException"/>
    /// class.
    /// </summary>
    /// <param name="message">The message to use for the exception.</param>
    /// <param name="innerException">An optional inner exception reference.</param>
    public FormGenerationException(
        string message,
        Exception innerException
        ) : base(message, innerException)
    {

    }

    // *******************************************************************

    /// <summary>
    /// This constructor creates a new instance of the <see cref="FormGenerationException"/>
    /// class.
    /// </summary>
    /// <param name="message">The message to use for the exception.</param>
    public FormGenerationException(
        string message
        ) : base(message)
    {

    }

    // *******************************************************************

    /// <summary>
    /// This constructor creates a new instance of the <see cref="FormGenerationException"/>
    /// class.
    /// </summary>
    /// <param name="info">The serialization info to use for the exception.</param>
    /// <param name="context">The streaming context to use for the exception.</param>
    public FormGenerationException(
        SerializationInfo info,
        StreamingContext context
        ) : base(info, context)
    {

    }

    #endregion
}
