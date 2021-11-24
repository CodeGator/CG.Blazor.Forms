using CG.Blazor.Forms.Attributes;
using CG.Validations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace CG.Blazor.Forms.Services
{
    /// <summary>
    /// This class is a default implementation of the <see cref="IFormGenerator"/>
    /// interface.
    /// </summary>
    internal class FormGenerator : IFormGenerator
    {
        // *******************************************************************
        // Fields.
        // *******************************************************************

        #region Fields

        /// <summary>
        /// This field contains a reference to a logger.
        /// </summary>
        private readonly ILogger<IFormGenerator> _logger;

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="FormGenerator"/>
        /// class.
        /// </summary>
        /// <param name="logger">The logger to use for the service.</param>
        public FormGenerator(
            ILogger<IFormGenerator> logger
            )
        {
            // Validate the parameters before attempting to use them
            Guard.Instance().ThrowIfNull(logger, nameof(logger));

            // Save the references.
            _logger = logger;
        }

        #endregion

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

            try
            {
                // Get the type of the viewModel.
                var viewModelType = viewModel?.GetType();

                // Let the world know what we're doing.
                _logger.LogDebug(
                    "Rendering HTML for a '{ViewModelType}' view-model.",
                    viewModelType.Name
                    );

                var index = 0;

                // Create the path we'll use to track our position in the object tree.
                var path = new Stack<object>(
                    new[] { viewModel }
                    );

                // Step 1, ensure we render any validation attributes on the view-model type.

                // Look for any form validation attributes (except a summary attribute).
                var vAttrs = viewModelType.GetCustomAttributes<FormValidationAttribute>()
                    .Where(x => !x.GetType().IsAssignableTo(typeof(RenderValidationSummaryAttribute)));

                // Did we find too many?
                if (vAttrs.Count() > 1)
                {
                    // Let the world know what we're doing.
                    _logger.LogDebug(
                        "More than one form validation attribute was found on: '{ViewModelType}' view-model.",
                        viewModelType.Name
                        );
                }

                // Loop through the attributes.
                foreach (var vAttr in vAttrs)
                {
                    // Allow the attribute to do it's thing.
                    index = vAttr.Generate(
                        builder,
                        index,
                        eventTarget,
                        path,
                        null,
                        _logger
                        );
                }

                // Step 2, ensure we render any non validation attributes on the view-model type.

                // Look for any form generation attributes (except for validation attributes).
                var attrs = viewModelType.GetCustomAttributes<FormGeneratorAttribute>()
                    .Where(x => !x.GetType().IsAssignableTo(typeof(FormValidationAttribute)));

                // Loop through the attributes.
                foreach (var attr in attrs)
                {
                    // Allow the attribute to do it's thing.
                    index = attr.Generate(
                        builder,
                        index,
                        eventTarget,
                        path,
                        null,
                        _logger
                        );
                }

                // Step 3, ensure we supply a missing attribute, if needed, to render properties.

                // Ensure the view-model had at least one RenderObject attribute.
                if (false == attrs.OfType<RenderObjectAttribute>().Any())
                {
                    // Let the world know what we're doing.
                    _logger.LogDebug(
                        "The '{ViewModelType}' class is not decorated with a RenderObject attribute! " +
                        "Manually supplying the missing attribute in order to render the form.",
                        viewModelType.Name
                        );

                    // If we get here then we've been handed a view-model that doesn't contain
                    //   a RenderObject attribute. So, we'll supply the attribute here and
                    //   manually render the properties on the object.
                    new RenderObjectAttribute().Generate(
                        builder,
                        index,
                        eventTarget,
                        path,
                        null,
                        _logger
                        );
                }

                // Step 4, ensure we render a validation summary, if needed.

                // Look for a summary attribute.
                var sAttr = viewModelType.GetCustomAttribute<RenderValidationSummaryAttribute>();
                
                // Did we find one?
                if (null != sAttr)
                {
                    // Allow the attribute to do it's thing.
                    index = sAttr.Generate(
                        builder,
                        index,
                        eventTarget,
                        path,
                        null,
                        _logger
                        );
                }
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
    }
}
