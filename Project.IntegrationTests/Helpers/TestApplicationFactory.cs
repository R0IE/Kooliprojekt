using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace KooliProjekt.IntegrationTests.Helpers
{
    public class TestApplicationFactory<TTestStartup> : WebApplicationFactory<TTestStartup> where TTestStartup : class
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            // Compute web project content root before creating the HostBuilder so
            // the hosting environment uses the correct physical file provider.
            var projectDir = Directory.GetCurrentDirectory();
            string solutionRoot = null;
            var dir = new DirectoryInfo(projectDir);
            for (int i = 0; i < 6 && dir != null; i++)
            {
                var sln = Path.Combine(dir.FullName, "Kooliprojekt.sln");
                if (File.Exists(sln))
                {
                    solutionRoot = dir.FullName;
                    break;
                }
                dir = dir.Parent;
            }

            string webProjectDir;
            if (!string.IsNullOrEmpty(solutionRoot))
            {
                webProjectDir = Path.Combine(solutionRoot, "Project");
            }
            else
            {
                var candidate1 = Path.GetFullPath(Path.Combine(projectDir, "..", "Project"));
                var candidate2 = Path.GetFullPath(Path.Combine(projectDir, "..", "..", "Project"));
                if (Directory.Exists(candidate1)) webProjectDir = candidate1;
                else if (Directory.Exists(candidate2)) webProjectDir = candidate2;
                else webProjectDir = projectDir;
            }

            // Log chosen content root for debugging test host startup
            System.Console.WriteLine($"TestApplicationFactory: projectDir={projectDir}");
            System.Console.WriteLine($"TestApplicationFactory: solutionRoot={solutionRoot}");
            System.Console.WriteLine($"TestApplicationFactory: webProjectDir={webProjectDir}");
            System.Console.WriteLine($"TestApplicationFactory: webProjectDir exists={Directory.Exists(webProjectDir)}");

            // Some hosting initialization (static web assets / host environment) may
            // probe for a folder named after the integration test assembly under the
            // solution root; create a fallback folder if it doesn't exist so the
            // PhysicalFileProvider constructor won't throw during host build.
            if (!string.IsNullOrEmpty(solutionRoot))
            {
                var fallbackDir = Path.Combine(solutionRoot, "KooliProjekt.IntegrationTests");
                if (!Directory.Exists(fallbackDir))
                {
                    Directory.CreateDirectory(fallbackDir);
                    System.Console.WriteLine($"TestApplicationFactory: created fallbackDir={fallbackDir}");
                }
            }

                var host = Host.CreateDefaultBuilder()
                                // ensure the host thinks the application is the real web project so
                                // static web assets and other file-provider based lookups use the
                                // web project's files instead of test project's files.
                                .ConfigureHostConfiguration(cfg =>
                                {
                                    cfg.AddInMemoryCollection(new Dictionary<string, string>
                                    {
                                        ["applicationName"] = "KooliProjekt",
                                        ["contentRoot"] = webProjectDir
                                    });
                                })
                                .UseContentRoot(webProjectDir)
                            .ConfigureWebHost(builder =>
                            {
                                builder.UseStartup<TTestStartup>();
                            })
                            .ConfigureAppConfiguration((context, conf) =>
                            {
                                var configPath = Path.Combine(webProjectDir, "appsettings.json");
                                System.Console.WriteLine($"TestApplicationFactory: configPath={configPath}, exists={File.Exists(configPath)}");
                                if (File.Exists(configPath)) conf.AddJsonFile(configPath);
                            });

            return host;
        }
    }
}
