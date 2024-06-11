using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionExample
{
    static class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            #region Singleton
            Console.WriteLine("Singleton Example:");
            var singleton1 = serviceProvider.GetService<IRandomNumberGenerator>();
            var singleton2 = serviceProvider.GetService<IRandomNumberGenerator>();
            Console.WriteLine($"Random Number 1: {singleton1.GetRandomNumber()}");
            Console.WriteLine($"Random Number 2: {singleton2.GetRandomNumber()}");
            Console.WriteLine($"Same instance: {singleton1 == singleton2}");
            #endregion

            #region Transient
            Console.WriteLine("\nTransient Example:");
            var transient1 = serviceProvider.GetService<IRandomNumberGenerator>();
            var transient2 = serviceProvider.GetService<IRandomNumberGenerator>();
            Console.WriteLine($"Random Number 1: {transient1.GetRandomNumber()}");
            Console.WriteLine($"Random Number 2: {transient2.GetRandomNumber()}");
            Console.WriteLine($"Same instance: {transient1 == transient2}");
            #endregion

            #region Scoped
            Console.WriteLine("\nScoped Example:");
            using (var scope = serviceProvider.CreateScope())
            {
                var scoped1 = scope.ServiceProvider.GetService<IRandomNumberGenerator>();
                var scoped2 = scope.ServiceProvider.GetService<IRandomNumberGenerator>();
                Console.WriteLine($"Random Number 1: {scoped1.GetRandomNumber()}");
                Console.WriteLine($"Random Number 2: {scoped2.GetRandomNumber()}");
                Console.WriteLine($"Same instance within scope: {scoped1 == scoped2}");
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var scoped3 = scope.ServiceProvider.GetService<IRandomNumberGenerator>();
                Console.WriteLine($"Random Number in new scope: {scoped3.GetRandomNumber()}");
            }
            #endregion

            Console.ReadLine();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRandomNumberGenerator, RandomNumberGenerator>(); // Singleton registration
            services.AddTransient<IRandomNumberGenerator, RandomNumberGenerator>(); // Transient registration
            services.AddScoped<IRandomNumberGenerator, RandomNumberGenerator>();    // Scoped registration
        }
    }
}
