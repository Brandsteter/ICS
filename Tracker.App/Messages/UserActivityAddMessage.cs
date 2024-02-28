// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Models;

namespace Tracker.App.Messages;
public record UserActivityAddMessage
{
    public required Guid UserId { get; init; }
    public required ActivityDetailModel Activity { get; init; }
}
