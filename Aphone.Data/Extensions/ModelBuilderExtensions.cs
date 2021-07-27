using Aphone.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aphone.Data.Extensions
{
    public static class  ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var roleId = new  Guid("83936C8B-1A07-4747-9F33-F5A07815267F") ;
            var adminId = new  Guid("A01BB0D8-1CC6-4118-8A2D-BCDE8470D0B3") ;
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin1",
                NormalizedUserName = "admin1",
                Email = "anh453138@gmail.com",
                NormalizedEmail = "anh453138@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234$"),
                SecurityStamp = string.Empty,
                FirstName = "Anh",
                LastName = "Nguyen",
                Dob = new DateTime(1999, 05, 20)
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
