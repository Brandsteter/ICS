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

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserDetailViewModel
    : ViewModelBase,
    IRecipient<UserSelectMessage>,
    IRecipient<UserEditMessage>,
    IRecipient<UserActivityAddMessage>,
    IRecipient<ActivityEditMessage>,
    IRecipient<ActivityDeleteMessage>,
    IRecipient<ActivityProjectAddMessage>,
    IRecipient<ActivityProjectEditMessage>,
    IRecipient<ActivityProjectDeleteMessage>,
    IRecipient<ProjectUserAddMessage>,
    IRecipient<ProjectUserCreateMessage>
{
    private readonly IUserFacade userFacade;
    private readonly IProjectUserFacade projectUserFacade;
    private readonly INavigationService navigationService;

    public Guid Id { get; set; }
    public UserDetailModel? User { get; set; }

    public UserDetailViewModel(
        IUserFacade userFacade,
        IProjectUserFacade projectUserFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        this.userFacade = userFacade;
        this.projectUserFacade = projectUserFacade;
        this.navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        User = await userFacade.GetAsync(Id);
    }

    private async Task DeleteAsync()
    {
        if (User is not null)
        {
            await userFacade.DeleteAsync(User.Id);


            navigationService.SendBackButtonPressed();
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (User is not null)
        {
            await navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(UserEditViewModel.User)] = User with { }, ["ImgUrl"] = User.ImgUrl });
        }
    }

    [RelayCommand]
    private async Task GoToProjectListAsync()
    {
        if (User is not null)
        {
            await navigationService.GoToAsync("/projects",
                new Dictionary<string, object?> { [nameof(ProjectListViewModel.UserId)] = User.Id });
            messengerService.Send(new UserProjectListMessage { UserId = User.Id });
        }
    }

    [RelayCommand]
    private async Task GoToActivityListAsync(Guid id)
    {
        if (User is not null)
        {
            await navigationService.GoToAsync("/activities",
                new Dictionary<string, object?> { [nameof(ActivityListViewModel.UserId)] = id });
            messengerService.Send(new UserSelectMessage { UserId = id });
        }
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        if (User is not null)
        {
            await navigationService.GoToAsync("/create");
            messengerService.Send(new CreateActProjMessage() { UserId = User.Id });
        }
    }

    [RelayCommand]
    private async Task GoToChangeUserAsync()
    {
        if (User is not null)
        {
            await navigationService.GoToAsync("//main");
        }
    }

    public async void Receive(UserSelectMessage message)
    {
        Id = message.UserId;
        await LoadDataAsync();
    }
    public async void Receive(UserEditMessage message)
    {
        if (message.UserId == User?.Id)
        {
            await LoadDataAsync();
        }
    }
    public async void Receive(UserActivityAddMessage message)
    {
        if (message.UserId == User?.Id)
        {
            await LoadDataAsync();
        }
    }
    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }
    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
    public async void Receive(ActivityProjectAddMessage message)
    {
        await LoadDataAsync();
    }
    public async void Receive(ActivityProjectEditMessage message)
    {
        await LoadDataAsync();
    }
    public async void Receive(ActivityProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
    public async void Receive(ProjectUserAddMessage message)
    {
        await LoadDataAsync();
    }
    public async void Receive(ProjectUserCreateMessage message)
    {
        await projectUserFacade.SaveAsync(new ProjectUserListModel() { ProjectId = message.ProjectId, UserId = User.Id });
        await LoadDataAsync();
    }

}
