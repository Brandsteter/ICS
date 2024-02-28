// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;
using Tracker.DAL.Mappers;
using Tracker.DAL.UnitOfWork;

namespace Tracker.BL.Facades;

public class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>, IProjectFacade
{
    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }
    protected override string IncludesNavigationPathDetail =>
        $"{nameof(ProjectEntity.Activities)}.{nameof(ActivityProjectEntity.Activity)}";

    public virtual async Task<IEnumerable<ProjectListModel>> GetUserProjectListAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<ProjectEntity> projects = uow
            .GetRepository<ProjectEntity, ProjectEntityMapper>()
            .Get();
        IQueryable<ProjectUserEntity> links = uow
            .GetRepository<ProjectUserEntity, ProjectUserEntityMapper>()
            .Get();

        IQueryable<ProjectEntity> result = from p in projects
                                           join l in links on p.Id equals l.ProjectId
                                           where l.UserId == id
                                           select p;
        List<ProjectEntity> entities = await result.ToListAsync();
        return ModelMapper.MapToListModel(entities);
    }
}
