// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.BL.Facades;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Models;
using Tracker.Common.Enums;
using Tracker.Common.Tests;
using Tracker.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace Tracker.BL.Tests;

public sealed class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSUT;
    private readonly IUserFacade _userFacadeSUT;
    private readonly IUserActivityFacade _userActivityFacadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
        _userActivityFacadeSUT = new UserActivityFacade(UnitOfWorkFactory, UserActivityModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = @"nevim",
            Start = new DateTime(2023, 4, 8),
            End = new DateTime(2023, 4, 10),
            Type = ActivityType.Testing,
            Description = @"nevim",

        };

        var _ = await _activityFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededActivity()
    {
        var activities = await _activityFacadeSUT.GetAsync();
        var activity = activities.Single(i => i.Id == ActivitySeeds.TestingUpdate.Id);

        DeepAssert.Equal(ActivityModelMapper.MapToListModel(ActivitySeeds.TestingUpdate), activity);
    }

    [Fact]
    public async Task GetById_SeededActivity()
    {
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.Testing.Id);

        DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeeds.Testing), activity);
    }

    [Fact]
    public async Task GetByFilter_FromSeeded_UserActivities()
    {
        //Arrange

        var userModel = new UserDetailModel()
        {
            Name = "Peter",
            Surname = "Griffin",
            ImgUrl = null,
        };
        var activityModel = new ActivityDetailModel()
        {
            Start = DateTime.Now.AddDays(1),
            End = DateTime.Now.AddMonths(1),
            Name = "stay",
            Type = ActivityType.Testing
        };
        var activityModel2 = new ActivityDetailModel()
        {
            Start = DateTime.Now.AddMonths(2),
            End = DateTime.Now.AddMonths(3),
            Name = "filter",
            Type = ActivityType.Testing
        };


        var userSaved = await _userFacadeSUT.SaveAsync(userModel with { Activities = default });
        var activitySaved = await _activityFacadeSUT.SaveAsync(activityModel);
        var activitySaved2 = await _activityFacadeSUT.SaveAsync(activityModel2);
        await _userActivityFacadeSUT.SaveAsync(new UserActivityListModel { ActivityId = activitySaved.Id, UserId = userSaved.Id });
        await _userActivityFacadeSUT.SaveAsync(new UserActivityListModel { ActivityId = activitySaved2.Id, UserId = userSaved.Id });

        var Start = DateTime.Now;
        var End = DateTime.Now.AddMonths(2);

        var activityExpected = new ActivityListModel()
        {
            Id = activitySaved.Id,
            Start = activitySaved.Start,
            End = activitySaved.End,
            Name = activitySaved.Name
        };

        IEnumerable<ActivityListModel> activities = await _activityFacadeSUT.GetActivityListAsync(userSaved.Id, Start, End);
        List<ActivityListModel> activityList = activities.ToList();
        List<ActivityListModel> activityListExpected = new List<ActivityListModel>();
        activityListExpected.Add(activityExpected);
        Assert.Equal(activityListExpected, activityList);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.EmptyActivity.Id);

        Assert.Null(activity);
    }

    [Fact]
    public async Task SeededActivity_DeleteById_Deleted()
    {
        await _activityFacadeSUT.DeleteAsync(ActivitySeeds.Testing.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Activities.AnyAsync(i => i.Id == ActivitySeeds.Testing.Id));
    }

    [Fact]
    public async Task NewActivity_InsertOrUpdate_ActivityAdded()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = @"nevim",
            Start = new DateTime(2023, 4, 8),
            End = new DateTime(2023, 4, 10),
            Type = ActivityType.Testing,
            Description = @"nevim",
        };

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
        DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));
    }

    [Fact]
    public async Task SeededActivity_InsertOrUpdate_ActivityUpdated()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = ActivitySeeds.Testing.Id,
            Name = @"nevim",
            Start = ActivitySeeds.Testing.Start,
            End = ActivitySeeds.Testing.End,
            Type = ActivitySeeds.Testing.Type,
            Description = ActivitySeeds.Testing.Description,
        };
        activity.End = new DateTime(2023, 4, 10);
        activity.Description += "updated";

        //Act
        await _activityFacadeSUT.SaveAsync(activity);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
        DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));
    }
}
