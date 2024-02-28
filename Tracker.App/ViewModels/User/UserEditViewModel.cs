// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Tracker.App.Messages;
using Tracker.App.Services;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Models;

namespace Tracker.App.ViewModels;

[QueryProperty(nameof(User), nameof(User))]
[QueryProperty(nameof(ImgUrl), nameof(ImgUrl))]
public partial class UserEditViewModel : ViewModelBase, IRecipient<UserEditMessage>
{
    private readonly IUserFacade userFacade;
    private readonly INavigationService navigationService;

    public string? ImgUrl { get; set; }

    public UserDetailModel User { get; set; } = UserDetailModel.Empty;

    public UserEditViewModel(
        IUserFacade userFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        this.userFacade = userFacade;
        this.navigationService = navigationService;
    }
    [RelayCommand]
    private async Task SaveAsync()
    {
        User.ImgUrl = ImgUrl;
        await userFacade.SaveAsync(User with { });
        messengerService.Send(new UserEditMessage { UserId = User.Id });
        navigationService.SendBackButtonPressed();
    }

    public async void Receive(UserEditMessage message)
    {
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        User = await userFacade.GetAsync(User.Id)
            ?? UserDetailModel.Empty;
    }
}
