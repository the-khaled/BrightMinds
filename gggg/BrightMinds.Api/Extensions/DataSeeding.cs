using Bogus;

using BrightMinds.Core.Models;
using BrightMinds.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System.Runtime.CompilerServices;

namespace BrightMinds.Api.Extensions
{
    public static class DataSeeding
    {
        public static async Task<WebApplication> SeedAppData(this WebApplication application, BrightMindsContext context)
        {
            var categories = new string[]
           {
    "Programming and Development",
    "Data Science and Analytics",
    "Artificial Intelligence and Machine Learning",
    "Business and Entrepreneurship",
    "Marketing and Digital Marketing",
    "Graphic Design and Creative Arts",
    "Health and Wellness",
    "Personal Development and Self-Improvement",
    "Language Learning",
    "Cybersecurity and IT",
    "Cloud Computing and DevOps",
    "Web Development",
    "Mobile App Development",
    "Finance and Investing",
    "Engineering and Technology",
    "Photography and Video Production",
    "Project Management",
    "Education and Teaching",
    "Law and Legal Studies",
    "Environmental Science and Sustainability"
};

            var courseNames = new List<string>
        {
            "Introduction to Computer Science",
            "Advanced Mathematics",
            "Digital Marketing",
            "Data Structures and Algorithms",
            "Artificial Intelligence",
            "Web Development",
            "Business Administration",
            "Graphic Design",
            "Cybersecurity Fundamentals",
            "Introduction to Psychology",
            "Principles of Economics",
            "Cloud Computing Basics",
            "Creative Writing Workshop",
            "Game Development",
            "Machine Learning for Beginners",
            "Introduction to Robotics",
            "Environmental Science",
            "Software Testing and Quality Assurance",
            "Mobile App Development",
            "Project Management Essentials",
            "Blockchain Technology",
            "Digital Transformation",
            "Network Security",
            "Biotechnology Basics",
            "Sustainable Energy Solutions",
            "Quantum Computing Introduction",
            "Data Science with Python",
            "Ethical Hacking",
            "Healthcare Informatics",
            "Astronomy and Astrophysics",
            "International Relations",
            "Music Theory Fundamentals",
            "Video Production and Editing",
            "Entrepreneurship 101",
            "Human Resources Management",
            "Artificial Neural Networks",
            "Big Data Analytics",
            "Cultural Anthropology",
            "Social Media Strategy",
            "History of Modern Art"
        };



            if (!context.Categories.Any())
            {
                var autherFaker = new Faker<Category>()
                   .RuleFor(a => a.Name, f => f.PickRandom(categories));

                var genratedCategories = autherFaker.Generate(10);
                context.Categories.AddRange(genratedCategories);
                await context.SaveChangesAsync();

            }


            if (!context.Instructors.Any())
            {
                var Ids=await context.Users.Select(u=>u.Id).Take(20).ToListAsync();

                var autherFaker = new Faker<Instructor>()
                    .RuleFor(a => a.JobTitle, f => f.Name.JobTitle())
                    .RuleFor(a => a.Qualifications, f => f.Lorem.Paragraph());
                
                   

                var instructors = autherFaker.Generate(20);
                for (int i = 0; i < instructors.Count; i++)
                    instructors[i].UserId = Ids[i];

                context.Instructors.AddRange(instructors);
                await context.SaveChangesAsync();
            }
            if (!context.Courses.Any())
            {
                var instructorIds =await context.Instructors.Select(i => i.UserId).ToListAsync();
                var categoryIds = await context.Categories.Select(i => i.Id).ToListAsync();

                string path = "wwwroot\\files\\CourseImages";
                string[] filePaths = Directory.GetFiles(path);
                var fileNames = new List<string>();
                foreach (var file in filePaths)
                {
                    string fileName = Path.GetFileName(file);
                    fileNames.Add(fileName);
                }


                var courseFaker = new Faker<Course>()
                       .RuleFor(b=>b.Name, f => f.PickRandom(courseNames))
                       .RuleFor(b=>b.Price,f=>f.Finance.Amount(50,1000))
                       .RuleFor(b => b.Description, b => b.Lorem.Paragraph())
                       .RuleFor(c => c.PictureUrl, f => f.PickRandom(fileNames))
                       .RuleFor(c => c.CreatedDate, f => DateOnly.FromDateTime(DateTime.Now))
                       .RuleFor(c => c.UpdatedDate, f => DateOnly.FromDateTime(DateTime.Now))
                       .RuleFor(a => a.CategoryId, f => f.PickRandom(categoryIds))
                       .RuleFor(a => a.InstructorId, f => f.PickRandom(instructorIds));
                var courses = courseFaker.Generate(50);
                context.Courses.AddRange(courses);
                await context.SaveChangesAsync();

            }
            //if (!context.Books.Any())
            //{
            //    var authorsIds = await context.Authors.Select(a => a.Id).ToListAsync();
            //    var genersIds = await context.Geners.Select(g => g.Id).ToListAsync();
            //    string path = "wwwroot\\files\\Images";
            //    string[] filePaths = Directory.GetFiles(path);
            //    var fileNames = new List<string>();
            //    foreach (var file in filePaths)
            //    {
            //        string fileName = Path.GetFileName(file);
            //        fileNames.Add(fileName);
            //    }
            //    var bookFaker = new Faker<Book>()
            //        .RuleFor(b => b.Title, b => b.Name.FullName())
            //        .RuleFor(b => b.GenerId, b => b.PickRandom(genersIds))
            //        .RuleFor(b => b.AuthorId, b => b.PickRandom(authorsIds))
            //        .RuleFor(b => b.Description, b => b.Lorem.Paragraph())
            //        .RuleFor(b => b.PictureUrl, b => b.PickRandom(fileNames))
            //        .RuleFor(b => b.Price, b => decimal.Parse(b.Commerce.Price(20.0m, 250.0m)));
            //    var books = bookFaker.Generate(300);
            //    context.Books.AddRange(books);
            //    await context.SaveChangesAsync();
            //}

            return application;
        }
    }
}
//var authors = new string[]
//{
//    "Ahmed Omar Khattab",
//       "Mohamed Adel Abo Elenien",
//       "Mo Salah Egypt",
//       "Aya Adel Salem",
//       "Howayda Moauwad",
//       "Asser Mostafa Hassan",
//       "Ahmed Khaled Tawfik",
//       "Taha hessien ",
//       "Menna Adel Salem",
//       "Youssif Ashraf Saasd",
//       "Youssief Ramadan Abo Doka"

//   }