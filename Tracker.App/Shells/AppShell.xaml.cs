// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using CommunityToolkit.Mvvm.Input;
using Tracker.App.Services;
using Tracker.App.ViewModels;

namespace Tracker.App.Shells;

public partial class AppShell
{
    private readonly INavigationService navigationService;

    public AppShell(INavigationService navigationService)
    {
        this.navigationService = navigationService;

        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToMainAsync()
        => await navigationService.GoToAsync<MainPageViewModel>();
}
