// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.App.Models;
using Tracker.App.ViewModels;
using Tracker.App.Views;
using Tracker.App.Views.Activity;
using Tracker.App.Views.Project;
using Tracker.App.Views.User;

namespace Tracker.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//main",typeof(MainPage),typeof(MainPageViewModel)),
        new("//main/createUser",typeof(createUser),typeof(UserEditViewModel)),
        new("//main/chooseUser",typeof(chooseUser),typeof(UserListViewModel)),

        new("//user/projects/detail/edit",typeof(EditProject),typeof(ProjectEditViewModel)),
        new("//user",typeof(userMainPage),typeof(UserDetailViewModel)),
        new("//user/edit",typeof(editUser),typeof(UserEditViewModel)),
        new("//user/projects",typeof(chooseProject),typeof(ProjectListViewModel)),
        new("//user/projects/detail",typeof(projectName),typeof(ProjectDetailViewModel)),
        new("//user/activities",typeof(myActivities),typeof(ActivityListViewModel)),
        new("//user/activities/detail",typeof(activityInfo),typeof(ActivityDetailViewModel)),
        new("//user/create",typeof(CreateActProj),typeof(CreateActProjViewModel)),
        new("//user/create/activity",typeof(createAct),typeof(ActivityEditViewModel)),
        new("//user/create/project",typeof(createProject),typeof(ProjectEditViewModel)),

    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}
