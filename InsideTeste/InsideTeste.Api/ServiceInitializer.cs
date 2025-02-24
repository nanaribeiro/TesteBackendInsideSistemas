namespace InsideTeste.Api
{
    public static class ServiceInitializer
    {
        public static IServiceCollection AddServiceDependencyGroup(
             this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
