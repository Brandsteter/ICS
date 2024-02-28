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
public partial class ActivityDetailViewModel : ViewModelBase, IRecipient<ActivitySelectMessage>
{
    private readonly IActivityFacade activityFacade;
    private readonly INavigationService navigationService;
    private readonly IAlertService alertService;

    public Guid Id { get; set; }
    public ActivityDetailModel? Activity { get; set; }

    public ActivityDetailViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        this.activityFacade = activityFacade;
        this.navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activity = await activityFacade.GetAsync(Id);
    }

    [RelayCommand]

    private async Task DeleteAsync()
    {
        if (Activity is not null)
        {
            try
            {
                await activityFacade.DeleteAsync(Activity.Id);
                messengerService.Send(new ActivityDeleteMessage());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Chyba", "Nelze smazat danou aktivitu.");
            }
        }
    }

    public async void Receive(ActivitySelectMessage message)
    {
        Id = message.ActivityId;
        await LoadDataAsync();
    }

}
