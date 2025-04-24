using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasneemSami.ToDoList.Database.Data;
using TasneemSami.ToDoList.Database.Models;

namespace TasneemSami.ToDoList.Api
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ToDoListDbContext context)
        {
            
            await context.Database.EnsureCreatedAsync();

          
            if (!context.Users.Any())
            {
                
                var admin = new Users
                {
                    UserName = "admin",
                    PASSWORD = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    ROLE = 1 // 1 = Admin
                };

              
                var user = new Users
                {
                    UserName = "user",
                    PASSWORD = BCrypt.Net.BCrypt.HashPassword("user123"),
                    ROLE = 2 
                };

                context.Users.AddRange(admin, user);
                await context.SaveChangesAsync();

               
                var tasks = new List<Tasks>
            {
                new Tasks
                {
                    TITLE = "Buy groceries",
                    DESCRIPTION = "Milk, eggs, and bread",
                    ISCOMPLETED = false,
                    USERID = user.ID
                },
                new Tasks
                {
                    TITLE = "Read a book",
                    DESCRIPTION = "Finish the last chapter of the novel",
                    ISCOMPLETED = false,
                    USERID = user.ID
                }
            };

                context.TaskS.AddRange(tasks);
                await context.SaveChangesAsync();
            }
        }
    }

}
