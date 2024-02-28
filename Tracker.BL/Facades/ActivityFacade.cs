// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;
using Tracker.DAL.Mappers;
using Tracker.DAL.UnitOfWork;

namespace Tracker.BL.Facades;

public class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>, IActivityFacade
{
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    { }

    private async Task<UserEntity?> GetUserEntity(Guid userId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        UserEntity? entity = await uow
            .GetRepository<UserEntity, UserEntityMapper>()
            .Get()
            .FirstOrDefaultAsync(e => e.Id == userId);
        return entity;
    }

    public virtual async Task<IEnumerable<ActivityListModel>> GetActivityListAsync(Guid userId)
    {

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<ActivityEntity> activities = uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get();
        IQueryable<UserActivityEntity> links = uow
            .GetRepository<UserActivityEntity, UserActivityEntityMapper>()
            .Get();

        IQueryable<ActivityEntity> result = from a in activities
                                            join l in links on a.Id equals l.ActivityId
                                            where l.UserId == userId
                                            select a;
        List<ActivityEntity> entities = await result.ToListAsync();

        return entities is null
            ? new ObservableCollection<ActivityListModel>()
            : ModelMapper.MapToListModel(entities);
    }

    public virtual async Task<IEnumerable<ActivityListModel>> GetActivityListAsync(Guid userId, DateTime? from, DateTime? to)
    {
        if (from is null) from = DateTime.MinValue;
        if (to is null) to = DateTime.MaxValue;

        UserEntity? entity = await GetUserEntity(userId);

        if (entity is null) { return new ObservableCollection<ActivityListModel>(); }

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<ActivityEntity> activities = uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get();
        IQueryable<UserActivityEntity> links = uow
            .GetRepository<UserActivityEntity, UserActivityEntityMapper>()
            .Get();

        IQueryable<ActivityEntity> result = from a in activities
                                            join l in links on a.Id equals l.ActivityId
                                            where l.UserId == userId
                                            select a;
        List<ActivityEntity> entities = await result.ToListAsync();

        IEnumerable<ActivityEntity> Activities = entities.Where(e => (e.Start.CompareTo(from) >= 0) && (e.End.CompareTo(to) <= 0))
                .OrderBy(e => e.Start);

        return ModelMapper.MapToListModel(Activities);
    }
}
