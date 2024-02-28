// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Tracker.App.Messages;
using Tracker.App.Services;
using Tracker.App.Services.Interfaces;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Models;

namespace Tracker.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(UserId), nameof(UserId))]
public partial class ProjectDetailViewModel : ViewModelBase, IRecipient<ProjectSelectMessage>
{
    private readonly IProjectFacade projectFacade;
    private readonly IProjectUserFacade projectUserFacade;
    private readonly INavigationService navigationService;
    private readonly IAlertService alertService;

    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public bool SubscribeVisible { get; set; } = true;
    public bool EditVisible { get; set; } = false;
    public ProjectDetailModel? Project { get; set; }

    public ProjectDetailViewModel(
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

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        if ((await projectUserFacade.Contains(UserId, Id)))
        {
            SubscribeVisible = false;
            EditVisible = true;
        }
        Project = await projectFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await navigationService.GoToAsync<ProjectEditViewModel>(
              new Dictionary<string, object?> { [nameof(ProjectEditViewModel.UserId)] = UserId });
    }

    [RelayCommand]
    private async Task SubscribeAsync()
    {
        messengerService.Send(new ProjectUserCreateMessage { ProjectId = Id });
        await navigationService.GoToAsync("//user",
            new Dictionary<string, object?> { ["Id"] = UserId });
        messengerService.Send(new UserSelectMessage { UserId = UserId });
    }

    /*[RelayCommand]

    private async Task DeleteAsync()
    {
        if (Project is not null)
        {
            try
            {
                await projectFacade.DeleteAsync(Project.Id);
                messengerService.Send(new ActivityDeleteMessage());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Chyba", "Nelze smazat danou aktivitu.");
            }
        }
    }
    */

    public async void Receive(ProjectSelectMessage message)
    {
        Id = message.ProjectId;
        await LoadDataAsync();
    }
}
