using Microsoft.Extensions.DependencyInjection;
using ScalableWeb.Diff.Application.Services;
using ScalableWeb.Diff.Provider.Services;

namespace Promob.Manager.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add all instances of classes and interfaces that will be used in the application.
        /// </summary>        
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IDiffService), typeof(DiffService));
        }
    }
}