// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.App.ViewModels;
using Tracker.Common.Enums;

namespace Tracker.App.Views.Activity;

public partial class myActivities
{
    protected ActivityListViewModel ViewModel { get; set; }

    public myActivities(ActivityListViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();

        BindingContext = ViewModel = viewModel;
    }

    private async void DatePickerStartDateSelected(object sender, DateChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            if(DatePickerEnd is not null)
                DatePickerEnd.MinimumDate = e.NewDate;

            await ViewModel.LoadFilterAsync();
        }
    }

    private async void DatePickerEndDateSelected(object sender, DateChangedEventArgs e)
    {
        if (ViewModel != null)
        {
            if (DatePickerStart is not null)
                DatePickerStart.MaximumDate = e.NewDate;

            await ViewModel.LoadFilterAsync();
        }
    }

    private void FilterChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        DatePickerEnd.Date = DateTime.Now;
        DateFilterType filter = ViewModel.SelectedFilter.ID;

        switch (filter) {
            case DateFilterType.Day:
                DatePickerStart.Date = DateTime.Now.AddDays(-1);
                break;
            case DateFilterType.Week:
                DatePickerStart.Date = DateTime.Now.AddDays(-7);
                break;
            case DateFilterType.Month:
                DatePickerStart.Date = DateTime.Now.AddMonths(-1);
                break;
            case DateFilterType.Year:
                DatePickerStart.Date = DateTime.Now.AddYears(-1);
                break;
        }
    }
}
