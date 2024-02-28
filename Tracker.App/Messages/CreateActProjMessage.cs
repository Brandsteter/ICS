// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.App.Messages;
public record CreateActProjMessage
{
    public required Guid UserId { get; init; }
}
