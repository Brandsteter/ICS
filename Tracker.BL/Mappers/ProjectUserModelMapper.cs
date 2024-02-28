// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;

namespace Tracker.BL.Mappers;

public class ProjectUserModelMapper : ModelMapperBase<ProjectUserEntity, ProjectUserListModel, ProjectUserListModel>,
    IProjectUserModelMapper
{
    public override ProjectUserListModel MapToListModel(ProjectUserEntity? entity) => entity is null
        ? ProjectUserListModel.Empty
        : new ProjectUserListModel
        {
            Id = entity.Id,
            ProjectId = entity.ProjectId,
            UserId = entity.UserId
        };

    public override ProjectUserListModel MapToDetailModel(ProjectUserEntity? entity) => entity is null
        ? ProjectUserListModel.Empty
        : new ProjectUserListModel
        {
            Id = entity.Id,
            ProjectId = entity.ProjectId,
            UserId = entity.UserId,
        };

    public override ProjectUserEntity MapToEntity(ProjectUserListModel model) => new() { Id = model.Id, ProjectId = model.ProjectId, UserId = model.UserId };
}
