// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using CommunityToolkit.Mvvm.Input;
using Tracker.App.Services;

namespace Tracker.App.ViewModels
{
    public partial class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public MainPageViewModel(
            INavigationService navigationService,
            IMessengerService messengerService)
            : base(messengerService)
        {
            this.navigationService = navigationService;
        }

        [RelayCommand]
        private async Task GoToCreateUserAsync()
        {
            await navigationService.GoToAsync("/createUser");
        }

        [RelayCommand]
        private async Task GoToChooseUserAsync()
        {
            await navigationService.GoToAsync("/chooseUser");
        }
    }
}
