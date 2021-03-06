﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Repository;
using Serilog;
using Tests.IntegrationTests;

[SetUpFixture]
public class OneTimeTestServerSetup
{
    private static TestServer _testServer;
    internal static HttpClient Client;
    internal static Database Database;

    [OneTimeSetUp]
    public async Task Before()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

        _testServer = new TestServer(TestServerBuilder);
        await _testServer.Host.StartAsync();
        Client = _testServer.CreateClient();
        Database = _testServer.Host.Services.GetRequiredService<Database>();
    }

    [OneTimeTearDown]
    public async Task After()
    {
        await _testServer?.Host?.StopAsync();
        _testServer?.Dispose();
        Client?.Dispose();
    }

    private static IWebHostBuilder TestServerBuilder => new WebHostBuilder()
        .UseTestServer()
        .UseConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(ConfigurationValues)
            .Build()
        )
        .UseSerilog(new LoggerConfiguration()
            .WriteTo.NUnitOutput()
            .CreateLogger()
        )
        .UseStartup<Host.Startup>()
        .ConfigureTestServices(services =>
        {
            services.AddDbContext<Database>(options =>
            {
                options.UseInMemoryDatabase("Database");
            });
            services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, MockAuthenticatedUser>("BasicAuthentication", null);
        });

    private static Dictionary<string, string> ConfigurationValues => new Dictionary<string, string> 
    {
        
    };
}
