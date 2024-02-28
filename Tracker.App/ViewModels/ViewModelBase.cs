// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using CommunityToolkit.Mvvm.ComponentModel;
using Tracker.App.Services;

namespace Tracker.App.ViewModels;

public abstract class ViewModelBase : ObservableRecipient, IViewModel
{
    private bool isRefreshRequired = true;

    protected readonly IMessengerService messengerService;

    protected ViewModelBase(IMessengerService messengerService)
        : base(messengerService.Messenger)
    {
        this.messengerService = messengerService;
        IsActive = true;
    }

    public async Task OnAppearingAsync()
    {
        if (isRefreshRequired)
        {
            await LoadDataAsync();

            isRefreshRequired = false;
        }
    }

    protected virtual Task LoadDataAsync()
        => Task.CompletedTask;
}
