// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Syncfusion.Maui.Calendar;
using Tracker.App.ViewModels;

namespace Tracker.App.Views.Activity;

public partial class createAct
{
    protected ActivityEditViewModel ViewModel { get; set; }

    public createAct(ActivityEditViewModel viewModel)
        : base(viewModel)
    {
        {
            InitializeComponent();

            BindingContext = ViewModel = viewModel;
        }
    }

    private void UpdateCalendar(DateTime? newTime, bool start) {
        if (Calendar.SelectedDateRange == null)
            Calendar.SelectedDateRange = new CalendarDateRange(newTime, newTime);
        else if (start && Calendar.SelectedDateRange.StartDate != newTime)
            Calendar.SelectedDateRange.StartDate = newTime;
        else if (!start && Calendar.SelectedDateRange.EndDate != newTime)
            Calendar.SelectedDateRange.EndDate = newTime;
    }

    private void DataPickerStartDateSelected(object sender, DateChangedEventArgs e)
    {
        UpdateCalendar(e.NewDate, true);
        DataPickerEnd.MinimumDate = e.NewDate;
    }

    private void DataPickerEndDateSelected(object sender, DateChangedEventArgs e)
    {
        UpdateCalendar(e.NewDate, false);
        DataPickerStart.MaximumDate = e.NewDate;
    }

    private void SfCalendar_SelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
    {
        CalendarDateRange? range = e.NewValue is CalendarDateRange ? (CalendarDateRange)e.NewValue : null;

        if (range != null)
        {
            if (this.ViewModel.Activity.Start != range.StartDate && range.StartDate is not null)
            {
                var date = (DateTime)range.StartDate;
                this.ViewModel.Activity.Start = date;
                DataPickerStart.Date = date;
            }

            if (this.ViewModel.Activity.End != range.EndDate && range.EndDate is not null)
            {
                var date = (DateTime)range.EndDate;
                this.ViewModel.Activity.End = date;
                DataPickerEnd.Date = date;
            }
        }
    }
}
