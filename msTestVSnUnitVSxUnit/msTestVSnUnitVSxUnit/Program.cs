using Microsoft.Extensions.DependencyInjection;
using System;

namespace msTestVSnUnitVSxUnit
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            RegisterServices();

            var mathService = _serviceProvider.GetService<IMathOperations>();

            int? sum = mathService.Sum(10, 20);
            int? sub = mathService.Sub(20, 10);
            int? mul = mathService.Mul(1, 2);
            int? div = mathService.Div(20, 10);

            DisposeServices();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();

            collection.AddScoped<IMathOperations, MathOperations>();
            collection.AddScoped<ISqlRepository, SqlRepository>();

            _serviceProvider = collection.BuildServiceProvider();
        }
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
