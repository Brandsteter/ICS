// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;

namespace Tracker.BL.Mappers;

public class UserActivityModelMapper : ModelMapperBase<UserActivityEntity, UserActivityListModel, UserActivityListModel>,
    IUserActivityModelMapper
{
    public override UserActivityListModel MapToListModel(UserActivityEntity? entity) => entity is null
        ? UserActivityListModel.Empty
        : new UserActivityListModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ActivityId = entity.ActivityId,
        };

    public override UserActivityListModel MapToDetailModel(UserActivityEntity? entity) => entity is null
        ? UserActivityListModel.Empty
        : new UserActivityListModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ActivityId = entity.ActivityId,
        };

    public override UserActivityEntity MapToEntity(UserActivityListModel model) => new() { Id = model.Id, UserId = model.UserId, ActivityId = model.ActivityId };
}
