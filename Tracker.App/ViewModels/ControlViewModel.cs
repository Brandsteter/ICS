// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.ObjectModel;
using Tracker.App.Models;
using Tracker.Common.Enums;

namespace Tracker.App.ViewModels;

public class ControlViewModel
{
    public static ObservableCollection<DataModel<ActivityType>> Activity { get; set; } = new ObservableCollection<DataModel<ActivityType>> {
            new DataModel<ActivityType>() { Name = "Unspecified", ID = ActivityType.Unspecified },
            new DataModel<ActivityType>() { Name = "Programming", ID = ActivityType.Programming },
            new DataModel<ActivityType>() { Name = "Testing", ID = ActivityType.Testing },
            new DataModel<ActivityType>() { Name = "School", ID = ActivityType.School },
            new DataModel<ActivityType>() { Name = "Sport", ID = ActivityType.Sport },
            new DataModel<ActivityType>() { Name = "Shopping", ID = ActivityType.Shopping },
            new DataModel<ActivityType>() { Name = "Meal", ID = ActivityType.Meal },
            new DataModel<ActivityType>() { Name = "Travel", ID = ActivityType.Travel },
        };

    public static ObservableCollection<DataModel<DateFilterType>> DateFilters { get; set; } = new ObservableCollection<DataModel<DateFilterType>> {
            new DataModel<DateFilterType>() { Name = "Year", ID = DateFilterType.Year },
            new DataModel<DateFilterType>() { Name = "Month", ID = DateFilterType.Month },
            new DataModel<DateFilterType>() { Name = "Week", ID = DateFilterType.Week },
            new DataModel<DateFilterType>() { Name = "Day", ID = DateFilterType.Day }
        };
}
