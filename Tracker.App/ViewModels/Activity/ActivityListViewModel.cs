// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Tracker.App.Messages;
using Tracker.App.Models;
using Tracker.App.Services;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Models;
using Tracker.Common;
using Tracker.Common.Enums;

namespace Tracker.App.ViewModels;

[QueryProperty(nameof(UserId), "Id")]
public partial class ActivityListViewModel : ViewModelBase, IRecipient<UserSelectMessage>, IRecipient<UserActivityAddMessage>, IRecipient<ActivityDeleteMessage>
{
    private readonly IActivityFacade activityFacade;
    private readonly INavigationService navigationService;

    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now.AddMonths(1);

    public Guid UserId { get; set; }
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    public ObservableCollection<DataModel<DateFilterType>> DateFilters { get; set; } = ControlViewModel.DateFilters;

    public DataModel<DateFilterType> SelectedFilter { get; set; }

    public ActivityListViewModel(
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

        if (UserId != Guid.Empty)
        {
            Activities = await activityFacade.GetActivityListAsync(UserId, StartDate, EndDate);
        }
    }

    public async Task LoadFilterAsync()
    {
        await base.LoadDataAsync();

        if (UserId != Guid.Empty)
        {
            Activities = await activityFacade.GetActivityListAsync(UserId, StartDate, EndDate);
        }
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });
        messengerService.Send(new ActivitySelectMessage { ActivityId = id });
    }

    [RelayCommand]
    private async Task GoToCreateActivityAsync(Guid id)
    {
        await navigationService.GoToAsync("//user/create/activity",
            new Dictionary<string, object?>() { ["Id"] = UserId });
    }

    public async void Receive(UserSelectMessage message)
    {
        UserId = message.UserId;
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(UserActivityAddMessage message)
    {
        await LoadDataAsync();
    }
}
