
using Bogus;
using BrightMinds.Core.Models;
using BrightMinds.Services.Helpers.Emails;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace Blendd.Api.Extensions
{
    public static class AppUsersAndRolesSeeding
    {
        public static async Task<WebApplication> SeedUsersAndRoles(this WebApplication application, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var role = new IdentityRole()
                {
                    Name = "Admin"
                };
                await roleManager.CreateAsync(role);
            }


            if (!userManager.Users.Any())
            {
                var users=new List<AppUser>();
                users.Add(new AppUser
                {
                    UserName = "esamabdelnaby",
                    FirstName = "Esam",
                    LastName = "Abd Elnaby",
                    Email = "esamabdelnaby@gmail.com",
                    PhoneNumber = "01008574566",
                    ImageCover= "1727181526008.jpeg"
                });
                users.Add(new AppUser
                {
                    UserName = "osamaelzero",
                    FirstName = "Osama",
                    LastName = "Elzero",
                    Email = "osamaelzero45@gmail.com",
                    PhoneNumber = "0125574566",
                    ImageCover = "1667397065959.jpeg"
                });
                users.Add(new AppUser
                {
                    UserName = "tharwatsamy",
                    FirstName = "Tharwat",
                    LastName = "Samy",
                    Email = "tharawatsamy25@gmail.com",
                    PhoneNumber = "01008578736",
                    ImageCover = "channels4_profile.jpg"
                });

                users.Add(new AppUser
                {
                    UserName = "yahiaelaraby",
                    FirstName = "Yahya",
                    LastName = "Elaraby",
                    Email = "yahiaelaraby118@gmail.com",
                    PhoneNumber = "01008578736",
                    ImageCover = "channels4_profile_2.jpg"
                });





                string path = "wwwroot/files\\UsersImages";
                string[] filePaths = Directory.GetFiles(path);
                var fileNames = new List<string>();
                foreach (var file in filePaths)
                {
                    string fileName = Path.GetFileName(file);
                    fileNames.Add(fileName);
                }
                var userFaker = new Faker<AppUser>()
                  .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                  .RuleFor(a => a.LastName, f => f.Name.LastName())
                  .RuleFor(a => a.Email, f => f.Internet.Email())
                  .RuleFor(a => a.UserName, (f, a) => a.Email.Split('@')[0])
                  .RuleFor(a=>a.PhoneNumber,f=>f.Phone.PhoneNumber())
                  .RuleFor(a => a.ImageCover, f => f.PickRandom(fileNames));

                // .RuleFor(a => a.Password, f => "Password123");
                users.AddRange( userFaker.Generate(40));



                var user = new AppUser()
                {
                    UserName = "ahmmmedomarkhtab",
                   FirstName = "Ahmed",
                   LastName= "Omar Khattab",
                    Email = "ahmmmedomarkhtab@gmail.com",
                    PhoneNumber="01006370511"
                };
                

                var result = await userManager.CreateAsync(user, "Aa1Bb2@Cc3");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                foreach(var u in users )
                {
                    await userManager.CreateAsync(u, "Password123!");
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(u);
                    await userManager.ConfirmEmailAsync(u, token);
                }



            }
            return application;
        }
    }
}
