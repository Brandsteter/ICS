// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.DAL.Entities;

namespace Tracker.DAL.Mappers;

public class UserActivityEntityMapper : IEntityMapper<UserActivityEntity>
{
    public void MapToExistingEntity(UserActivityEntity existingEntity, UserActivityEntity newEntity)
    {
        existingEntity.UserId = newEntity.UserId;
        existingEntity.ActivityId = newEntity.ActivityId;
    }
}
