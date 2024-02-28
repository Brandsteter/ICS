// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Tracker.App.Messages;
using Tracker.App.Models;
using Tracker.App.Services;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Models;
using Tracker.Common.Enums;

namespace Tracker.App.ViewModels;

[QueryProperty(nameof(Activity), nameof(Activity))]
[QueryProperty(nameof(UserId), "Id")]
public partial class ActivityEditViewModel : ViewModelBase
{
    private readonly IActivityFacade activityFacade;
    private readonly IUserActivityFacade userActivityFacade;
    private readonly INavigationService navigationService;

    public Guid UserId { get; set; }
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;

    public ObservableCollection<DataModel<ActivityType>> CathegorySelection { get; set; } = ControlViewModel.Activity;

    public DataModel<ActivityType> SelectedActivityType { get; set; }

    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        IUserActivityFacade userActivityFacade,
        INavigationService navigationService,
        IMessengerService messengerService)
        : base(messengerService)
    {
        this.activityFacade = activityFacade;
        this.userActivityFacade = userActivityFacade;
        this.navigationService = navigationService;
    }


    [RelayCommand]
    private async Task SaveAsync(Guid id)
    {
        Activity.Type = SelectedActivityType is null ? ActivityType.Unspecified : SelectedActivityType.ID; 
        Activity = await activityFacade.SaveAsync(Activity with { });
        await userActivityFacade.SaveAsync(new UserActivityListModel { ActivityId = Activity.Id, UserId = id });
        messengerService.Send(new UserActivityAddMessage { Activity = Activity, UserId = id });
        await navigationService.GoToAsync("//user/activities");
        messengerService.Send(new UserSelectMessage { UserId = id });
    }

    private async Task ReloadDataAsync()
    {
        Activity = await activityFacade.GetAsync(Activity.Id)
            ?? ActivityDetailModel.Empty;
    }
}
