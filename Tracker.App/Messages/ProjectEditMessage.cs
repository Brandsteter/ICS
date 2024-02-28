// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Tracker.App.Messages;
public record ProjectEditMessage
{
    public required Guid ProjectId { get; init; }
}
