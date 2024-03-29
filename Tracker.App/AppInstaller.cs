﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using CommunityToolkit.Mvvm.Messaging;
using Tracker.App.Services;
using Tracker.App.Shells;
using Tracker.App.ViewModels;
using Tracker.App.Views;

namespace Tracker.App;

public static class AppInstaller
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<AppShell>();

        services.AddSingleton<IMessenger>(_ => StrongReferenceMessenger.Default);
        services.AddSingleton<IMessengerService, MessengerService>();

        //  services.AddSingleton<IAlertService, AlertService>();

        services.Scan(selector => selector
           .FromAssemblyOf<App>()
           .AddClasses(filter => filter.AssignableTo<ContentPageBase>())
           .AsSelf()
           .WithTransientLifetime());

        services.Scan(selector => selector
           .FromAssemblyOf<App>()
           .AddClasses(filter => filter.AssignableTo<IViewModel>())
           .AsSelfWithInterfaces()
           .WithTransientLifetime());

        services.AddTransient<INavigationService, NavigationService>();

        return services;
    }
}
