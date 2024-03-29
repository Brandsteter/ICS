﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Models;
using Tracker.DAL.Entities;

namespace Tracker.BL.Facades.Interfaces;

public interface IProjectUserFacade : IFacade<ProjectUserEntity, ProjectUserListModel, ProjectUserListModel>
{
    Task<bool> Contains(Guid UserId, Guid ProjectId);
}
