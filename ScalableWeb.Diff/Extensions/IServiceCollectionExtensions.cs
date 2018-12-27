using Microsoft.Extensions.DependencyInjection;
using ScalableWeb.Diff.Application.Services;
using ScalableWeb.Diff.Provider.Services;

namespace Promob.Manager.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IDiffService), typeof(DiffService));
        }
    }
}