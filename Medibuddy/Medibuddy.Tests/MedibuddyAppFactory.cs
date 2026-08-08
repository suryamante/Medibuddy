using System.Collections.Generic;
using Medibuddy.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Medibuddy.Tests;

/// <summary>
/// Boots the real app pipeline against a private, isolated in-memory SQLite database.
/// Each factory instance gets a uniquely named shared-cache database, kept alive by the
/// app's singleton connection factory for the lifetime of the host.
/// </summary>
public class MedibuddyAppFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString =
        $"Data Source=medibuddy_test_{Guid.NewGuid():N};Mode=Memory;Cache=Shared";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["DatabaseProvider"] = "Sqlite",
                ["ConnectionStrings:SqliteConnectionString"] = _connectionString
            });
        });
    }

    /// <summary>Forces host creation so the schema initializer runs before the first request.</summary>
    public HttpClient CreateConfiguredClient()
    {
        HttpClient client = CreateClient();
        _ = Services.GetRequiredService<IDbConnectionFactory>();
        return client;
    }
}
