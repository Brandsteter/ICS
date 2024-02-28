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

[QueryProperty(nameof(Project), nameof(Project))]
[QueryProperty(nameof(UserId), "Id")]
public partial class ProjectEditViewModel : ViewModelBase, IRecipient<ProjectCreateMessage>
{
    private readonly IProjectFacade projectFacade;
    private readonly IProjectUserFacade projectUserFacade;
    private readonly INavigationService navigationService;

    public Guid UserId { get; set; }
    public ProjectDetailModel Project { get; set; } = ProjectDetailModel.Empty;

    public ProjectEditViewModel(
        IProjectFacade projectFacade,
        IProjectUserFacade projectUserFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        this.projectFacade = projectFacade;
        this.projectUserFacade = projectUserFacade;
        this.navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync(Guid Id)
    {
        Project = await projectFacade.SaveAsync(Project with { });
        if (!(await projectUserFacade.Contains(UserId, Project.Id)))
        {
            await projectUserFacade.SaveAsync(new ProjectUserListModel() { ProjectId = Project.Id, UserId = UserId });
            messengerService.Send(new ProjectUserAddMessage { UserId = UserId, Project = Project });
        }
        messengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });
        await navigationService.GoToAsync("//user");
        messengerService.Send(new UserSelectMessage { UserId = UserId });
    }

    public void Receive(ProjectCreateMessage message)
    {
        UserId = message.UserId;
    }
}
