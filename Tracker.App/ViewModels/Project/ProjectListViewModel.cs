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

[QueryProperty(nameof(UserId), "Id")]
public partial class ProjectListViewModel : ViewModelBase, IRecipient<UserProjectListMessage>, IRecipient<ProjectUserAddMessage>, IRecipient<ProjectDeleteMessage>
{
    private readonly IProjectFacade projectFacade;
    private readonly INavigationService navigationService;

    public Guid UserId { get; set; }
    private bool AllProjects = false;
    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        this.projectFacade = projectFacade;
        this.navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        if (UserId == Guid.Empty || AllProjects)
        {
            Projects = await projectFacade.GetAsync();
        }
        else
        {
            Projects = await projectFacade.GetUserProjectListAsync(UserId);
        }
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<ProjectDetailViewModel>(
              new Dictionary<string, object?> { [nameof(ProjectDetailViewModel.Id)] = id, [nameof(UserId)] = UserId });
        messengerService.Send(new ProjectSelectMessage { ProjectId = id });
    }

    [RelayCommand]
    private async Task ShowAllAsync()
    {
        AllProjects = !AllProjects;
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task GoToCreateProjectAsync()
    {
        await navigationService.GoToAsync("//user/create/project",
            new Dictionary<string, object?>() { ["Id"] = UserId });
        messengerService.Send(new ProjectCreateMessage { UserId = UserId });
    }

    public async void Receive(UserProjectListMessage message)
    {
        UserId = message.UserId;
        AllProjects = false;
        await LoadDataAsync();
    }

    public async void Receive(ProjectUserAddMessage message)
    {
        UserId = message.UserId;
        await LoadDataAsync();
    }

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
