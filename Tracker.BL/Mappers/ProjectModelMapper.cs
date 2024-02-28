// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;

namespace Tracker.BL.Mappers;

public class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>,
    IProjectModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;
    private readonly IUserModelMapper _userModelMapper;

    public ProjectModelMapper(IActivityModelMapper activityModelMapper, IUserModelMapper userModelMapper)
    {
        _activityModelMapper = activityModelMapper;
        _userModelMapper = userModelMapper;
    }

    public override ProjectListModel MapToListModel(ProjectEntity? entity) => entity is null
        ? ProjectListModel.Empty
        : new ProjectListModel { Id = entity.Id, Name = entity.Name };

    public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity) => entity is null
        ? ProjectDetailModel.Empty
        : new ProjectDetailModel
        {
            Id = entity.Id,
            Name = entity.Name,
            //Selecting from weak entities
            Activities = _activityModelMapper.MapToListModel(
                entity.Activities.Select(e => e.Activity)
                    .OfType<ActivityEntity>()).ToObservableCollection(),
            Users = _userModelMapper.MapToListModel(entity.Users
                .Select(e => e.User)
                .OfType<UserEntity>()).ToObservableCollection(),
        };

    public override ProjectEntity MapToEntity(ProjectDetailModel model) => new() { Id = model.Id, Name = model.Name };
}
