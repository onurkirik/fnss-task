using System.Reflection.Metadata;
using FnssTask.Application.Abstraction;
using FnssTask.Application.Repositories;
using FnssTask.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace FnssTask.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRepository<Article>, ArticleRepository>();
        serviceCollection.AddScoped<IRepository<Category>, CategoryRepository>();
        serviceCollection.AddScoped<IRepository<Comment>, CommentRepository>();

        return serviceCollection;
    }
}
