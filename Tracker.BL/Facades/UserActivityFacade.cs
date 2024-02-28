// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;
using Tracker.DAL.Mappers;
using Tracker.DAL.UnitOfWork;

namespace Tracker.BL.Facades;

public class UserActivityFacade : FacadeBase<UserActivityEntity, UserActivityListModel, UserActivityListModel, UserActivityEntityMapper>, IUserActivityFacade
{
    public UserActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserActivityModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }
}
