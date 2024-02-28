// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.DAL.Entities;

namespace Tracker.DAL.Seeds;
public static class UserSeeds
{
    public static readonly UserEntity User0 = new()
    {
        Id = Guid.NewGuid(),
        Name = "Antonín",
        Surname = "Zeman",
        ImgUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Rotating_earth_%28large%29.gif/200px-Rotating_earth_%28large%29.gif",
        Activities = null,
        Projects = null
    };


    public static readonly UserEntity User1 = new()
    {
        Id = Guid.NewGuid(),
        Name = "František",
        Surname = "Fiala",
        ImgUrl = null,
        Activities = null,
        Projects = null
    };


    public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<UserEntity>().HasData(User0, User1);
}
