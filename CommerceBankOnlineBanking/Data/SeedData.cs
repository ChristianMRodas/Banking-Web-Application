using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CommerceBankOnlineBanking.Data;
using CommerceBankOnlineBanking.Models;

using System;
using System.Linq;

namespace CommerceBankOnlineBanking.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BankingContext(serviceProvider.GetRequiredService<DbContextOptions<BankingContext>>()))
            {
                context.Database.EnsureCreated();
                // User table, if empty, seed  with admin first?
                /*if (!context.User.Any()){
                    context.AddRange(new User{
                        Id = new Guid(),
                        FirstName = "Admin",
                        LastName = "Pass",
                        UserName = "AdminPass",
                        Password = "@Adm1n"
                        }
                    );
                    
                
                }
                context.SaveChanges();
                */
            }
        }
    }
}
