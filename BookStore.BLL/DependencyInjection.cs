using BookStore.BLL.Utilities.AutoMapperProfile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BookStore.BLL.Services.IServices;
using BookStore.BLL.Services;

namespace BookStore.BLL;

public static class DependencyInjection
{
    public static void RegisterBLL(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AutoMapperProfile)));
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IOrderService, OrderService>();
    }
}