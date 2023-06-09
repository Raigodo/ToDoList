﻿using Microsoft.AspNetCore.Identity;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Roles;

namespace ToDoList.API.Services.Seeder;

public class AdminSeeder
{
    public static async Task SeedAsync(WebApplication app)
    {
        using (var serviceScope = app.Services.CreateScope())
        {
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<UserEntity>>();
            if (userManager == null)
                throw new Exception("UserManager is not available");

            if (await userManager.FindByNameAsync("Admin") == null)
            {
                var newAdmin = new UserEntity
                {
                    Nickname = "Executioner",
                    UserName = "Admin",
                    Email = "admin@example.com",
                };

                var result = await userManager.CreateAsync(newAdmin, "P@55w0rd");
                await userManager.AddToRoleAsync(newAdmin, ApiUserRoles.Admin);
            }
        }
    }
}
