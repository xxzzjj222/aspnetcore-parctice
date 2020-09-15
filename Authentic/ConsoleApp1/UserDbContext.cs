using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class UserDbContext:DbContext
    {
        public UserDbContext(DbContextOptions options)
            :base(options)
        {
            Database.EnsureCreated();
            if (Users.Find("User1")!=null)
            {
                return;
            }
            Users.Add(new User("User1", "123"));
            Users.Add(new User("User2", "123"));
            Roles.Add(new Role("Admin"));
            UserRoles.Add(new UserRole
            {
                NormalizedUserName = "USER1",
                NormalizedRoleName = "ADMIN"
            });
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(user => user.NormalizedUserName);
                builder.HasMany(user => user.Roles).WithOne().HasForeignKey(userRole =>
                      userRole.NormalizedUserName);
            });
            modelBuilder.Entity<Role>(builder =>
            {
                builder.HasKey(role => role.NormalizedRoleName);
                builder.HasMany(role => role.Users).WithOne().HasForeignKey(userRole =>
                      userRole.NormalizedRoleName);
            });
            modelBuilder.Entity<UserRole>(builder =>
                builder.HasKey(userRole => new { userRole.NormalizedUserName, userRole.NormalizedRoleName }));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
