// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Tracker.App.Messages;
using Tracker.App.Services;

namespace Tracker.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class CreateActProjViewModel
    : ViewModelBase,
    IRecipient<CreateActProjMessage>
{
    private readonly INavigationService navigationService;

    public Guid Id { get; set; }

    public CreateActProjViewModel(
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        this.navigationService = navigationService;
    }

    [RelayCommand]
    private async Task GoToCreateActivityAsync(Guid id)
    {
        await navigationService.GoToAsync("/activity",
            new Dictionary<string, object?>() { [nameof(Id)] = Id });
    }

    [RelayCommand]
    private async Task GoToCreateProjectAsync()
    {
        await navigationService.GoToAsync("/project",
            new Dictionary<string, object?>() { [nameof(Id)] = Id });
        messengerService.Send(new ProjectCreateMessage { UserId = Id });
    }

    public void Receive(CreateActProjMessage message)
    {
        Id = message.UserId;
    }
}
