// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.App.ViewModels;

namespace Tracker.App.Views.Project;

public partial class EditProject
{
    public EditProject(ProjectEditViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
