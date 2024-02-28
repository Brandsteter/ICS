// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.DAL.Entities;

namespace Tracker.Common.Tests.Seeds;

public static class UserActivitySeeds
{
    public static readonly UserActivityEntity UserActivityEntity1 = new()
    {
        Id = Guid.NewGuid(),
        UserId = UserSeeds.UserWithActivity.Id,
        User = UserSeeds.UserWithActivity,
        ActivityId = ActivitySeeds.TimeActivity.Id,
        Activity = ActivitySeeds.TimeActivity
    };

    public static readonly UserActivityEntity UserActivityEntity2 = new()
    {
        Id = Guid.NewGuid(),
        UserId = UserSeeds.UserWithActivity.Id,
        User = UserSeeds.UserWithActivity,
        ActivityId = ActivitySeeds.Playing.Id,
        Activity = ActivitySeeds.Playing
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<UserActivityEntity>().HasData(
            UserActivityEntity1 with { Activity = null, User = null },
            UserActivityEntity2 with { Activity = null, User = null }
        );
}
