using CG.Validations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;

namespace CG.Blazor.Forms
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="RenderTreeBuilder"/>
    /// type.
    /// </summary>
    public static partial class RenderTreeBuilderExtensions
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
        /// any content for the component.</param>
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
                // Loop through the attributes.
                foreach (var attr in attributes)
                {
                    // Add the standard attribute.
                    builder.AddAttribute(
                        index++,
                        attr.Key,
                        attr.Value
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
                    (RenderFragment)(contentBuilder =>
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
        /// This method renders the specified UI element.
        /// </summary>
        /// <param name="builder">The builder to use for the operation.</param>
        /// <param name="index">The index to use for the operation.</param>
        /// <param name="elementType">The type of element to render.</param>
        /// <param name="contentDelegate">An optional delegate for building 
        /// any content for the component.</param>
        /// <param name="attributes">Optional table of named attributes.</param>
        /// <returns>The index after rendering is complete.</returns>
        public static int RenderUIElement(
            this RenderTreeBuilder builder,
            int index,
            string elementType,
            Action<RenderTreeBuilder> contentDelegate = null,
            IDictionary<string, object> attributes = null
            ) 
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(builder, nameof(builder))
                .ThrowIfNullOrEmpty(elementType, nameof(elementType))
                .ThrowIfLessThanZero(index, nameof(index));

            // Open the HTML tag.
            builder.OpenElement(
                index++,
                elementType
                );

            // Are any attributes specified?
            if (null != attributes)
            {
                // Loop through the attributes.
                foreach (var attr in attributes)
                {
                    // Add the standard attribute.
                    builder.AddAttribute(
                        index++,
                        attr.Key,
                        attr.Value
                        );
                }
            }

            // Should we render child content?
            if (null != contentDelegate)
            {
                // Render the child content
                builder.AddContent(
                    index++,
                    (RenderFragment)(contentBuilder =>
                        contentDelegate(contentBuilder)
                        )
                    );
            }

            // Close the HTML tag.
            builder.CloseElement();

            // Make the HTML purdy.
            builder.AddMarkupContent(index++, "\r\n    ");

            // Return the index.
            return index;
        }

        #endregion
    }
}
