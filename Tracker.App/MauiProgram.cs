// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Reflection;
using CookBook.App.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Syncfusion.Maui.Core.Hosting;
using Tracker.App.Services;
using Tracker.BL;
using Tracker.DAL;
using Tracker.DAL.Factories;

namespace Tracker.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureSyncfusionCore();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            ConfigureAppSettings(builder);
            builder.Services.Configure<DALOptions>(options => builder.Configuration.GetSection("Tracker:DAL").Bind(options));

            builder.Services.AddSingleton<IDbContextFactory<TrackerDbContext>>(provider =>
            {
                DALOptions dalOptions = provider.GetRequiredService<IOptions<DALOptions>>().Value;
                return new DbContextSqLiteFactory(dalOptions.Sqlite.DatabaseName, false);
            });


#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services
                .AddDALServices(builder.Configuration)
                .AddAppServices()
                .AddBLServices();

            var app = builder.Build();

            app.Services.GetRequiredService<IDbMigrator>().Migrate();
            RegisterRouting(app.Services.GetRequiredService<INavigationService>());

            return app;
        }
        private static void ConfigureAppSettings(MauiAppBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder();

            var assembly = Assembly.GetExecutingAssembly();
            const string appSettingsFilePath = "Tracker.App.appsettings.json";
            using var appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
            if (appSettingsStream is not null)
            {
                configurationBuilder.AddJsonStream(appSettingsStream);
            }

            var configuration = configurationBuilder.Build();
            builder.Configuration.AddConfiguration(configuration);
        }

        private static void RegisterRouting(INavigationService navigationService)
        {
            foreach (var route in navigationService.Routes)
            {
                Routing.RegisterRoute(route.Route, route.ViewType);
            }
        }
    }
}
