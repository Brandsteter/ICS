// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.App.Models
{
    public class DataModel<T>
    {
        public string Name { get; set; }

        public T ID { get; set; }
    }
}
