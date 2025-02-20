﻿using Microsoft.Extensions.DependencyInjection;

namespace InsideTeste.CommandStore
{
    public static class CommandStoreInitializer
    {
        public static IServiceCollection AddCommandStoreDependencyGroup(
             this IServiceCollection services)
        {
            services.AddTransient<IOrderCommandStore, OrderCommandStore>();

            return services;
        }
    }
}
