using BookStore.DAL.Repositories.IRepositories;
using BookStore.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using BookStore.DAL.DataContext;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL;

public static class DependencyInjection
{
    public static void RegisterDAL(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<BookStoreDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("MyDbConnection"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
