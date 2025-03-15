﻿using System.Text;
using System.Text.Json;
namespace OpenShock.Desktop.RepositoryServer;

public static class ConfigurationExtensions
{
    public static T GetAndRegisterOpenShockConfig<T>(this WebApplicationBuilder builder) where T : class
    {
#if DEBUG
        Console.WriteLine(builder.Configuration.GetDebugView());
#endif

        var config = builder.Configuration
            .Get<T>() ?? throw new Exception("Couldn't bind config, check config file");

        var openshockSection = builder.Configuration.GetChildren()
            .FirstOrDefault(x => x.Key.Equals("openshock", StringComparison.InvariantCultureIgnoreCase));

        if (openshockSection != null)
        {
            openshockSection.Bind(config);
        }

        MiniValidation.MiniValidator.TryValidate(config, true, true, out var errors);
        if (errors.Count > 0)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Error validating config, please fix your configuration / environment variables");
            sb.AppendLine("Found the following errors:");
            foreach (var error in errors)
            {
                sb.AppendLine($"Error on field [{error.Key}] reason: {string.Join(", ", error.Value)}");
            }

            Console.WriteLine(sb.ToString());
            Environment.Exit(-10);
        }

#if DEBUG
        Console.WriteLine(JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true }));
#endif

        builder.Services.AddSingleton<T>(config);

        return config;
    }
}
