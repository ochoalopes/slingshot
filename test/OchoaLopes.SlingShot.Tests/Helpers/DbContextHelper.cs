using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OchoaLopes.SlingShot.Infra.Context;

namespace OchoaLopes.SlingShot.Tests.Helpers
{
    public static class DbContextHelper
    {
        public static DbContextOptions<SlingShotContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<SlingShotContext>();
            builder.UseInMemoryDatabase("InMemoryTestDatabase")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
