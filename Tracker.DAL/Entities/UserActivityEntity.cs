// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.DAL.Entities;

public record UserActivityEntity : IEntity
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public UserEntity? User { get; init; }
    public required Guid ActivityId { get; set; }
    public ActivityEntity? Activity { get; init; }
}
