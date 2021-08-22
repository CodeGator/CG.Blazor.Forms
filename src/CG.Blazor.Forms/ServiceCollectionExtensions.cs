using CG.Blazor.Forms.Services;
using CG.Validations;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IServiceCollection"/>
    /// type.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method registers specific <see cref="IFormGenerator"/> strategies 
        /// for dynamically generating forms.
        /// </summary>
        /// <returns>The value of the <paramref name="serviceCollection"/>
        /// parameter, for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// a required argument is missing or invalid.</exception>
        public static IServiceCollection AddFormGeneration(
            this IServiceCollection serviceCollection
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection));

            // Register the form generator.
            serviceCollection.AddSingleton<IFormGenerator, FormGenerator>();

            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
