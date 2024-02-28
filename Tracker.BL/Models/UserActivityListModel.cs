// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.BL.Models;

public record UserActivityListModel : ModelBase
{
    public required Guid UserId { get; set; }
    public required Guid ActivityId { get; set; }

    public static UserActivityListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        UserId = Guid.Empty,
        ActivityId = Guid.Empty,
    };
}
