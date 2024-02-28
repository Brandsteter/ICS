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

public class ProjectUserFacade : FacadeBase<ProjectUserEntity, ProjectUserListModel, ProjectUserListModel, ProjectUserEntityMapper>, IProjectUserFacade
{
    public ProjectUserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectUserModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    public async Task<bool> Contains(Guid UserId, Guid ProjectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ProjectUserEntity> entities = uow.GetRepository<ProjectUserEntity, ProjectUserEntityMapper>().Get().Where(p => p.UserId == UserId).Where(p => p.ProjectId == ProjectId).ToList();
        if (entities is null || entities.Count == 0)
        {
            return false;
        }
        return true;
    }
}
