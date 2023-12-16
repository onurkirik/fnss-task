using FnssTask.Application.Abstraction;
using FnssTask.Domain.Entities;
using FnssTask.Persistence.Context;
using FnssTask.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FnssTask.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection serviceCollection)
    {

        serviceCollection.AddScoped<DbContext>();

        serviceCollection.AddScoped<IArticleRepository, ArticleRepository>();

        return serviceCollection;
    }
}
