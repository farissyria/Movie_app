using Cinema.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Data
{
    public class DbSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly MongoDbContext _context;

        public DbSeeder(UserManager<User> userManager, RoleManager<Roles> roleManager, MongoDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task SeedAllAsync()
        {
            await SeedRolesAsync();
            await SeedAdminUserAsync();
            await SeedMoviesAsync();
        }
        private async Task SeedMoviesAsync()
        {
            // Check if movies already exist
            var existingMoviesCount = await _context.Movies.CountDocumentsAsync(_ => true);
            if (existingMoviesCount > 0)
            {
                Console.WriteLine($"Movies already exist ({existingMoviesCount} movies found). Skipping seeding.");
                return;
            }

            var movies = GetSampleMovies();

            foreach (var movie in movies)
            {
                await _context.Movies.InsertOneAsync(movie);
                Console.WriteLine($"✅ Added movie: {movie.Title}");
            }

            Console.WriteLine($"🎬 Successfully seeded {movies.Count} movies!");
        }

        private List<Movie> GetSampleMovies()
        {
            return new List<Movie>
            {
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "The Shawshank Redemption", 
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    Genres = new List<string> { "Drama" }, 
                    DurationMinutes = 142, 
                    ReleaseDate = new DateTime(1994, 10, 14),  
                    Rating = 9.3, 
                    Language = "English",
             
                },
                new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = "Se7en",
                    Description = "Two detectives hunt a serial killer who uses the seven deadly sins as his motives.",  // Fixed description
                    Genres = new List<string> { "Crime", "Drama", "Thriller" }, 
                    ReleaseDate = new DateTime(1995, 9, 22), 
                    Rating = 8.6,  
                    Language = "English",
               
                },
               
            };
        }
        private async Task SeedRolesAsync()
        {
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new Roles
                    {
                        Name = role,
                        Description = $"{role} role for cinema system"
                    });
                    Console.WriteLine($"Created role: {role}");
                }
            }
        }

        private async Task SeedAdminUserAsync()
        {
            var adminEmail = "admin@cinema.com";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(adminUser, "Admin@123456");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("✅ Created Admin user");
                }
                else
                {
                    Console.WriteLine("❌ Failed to create Admin user");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"- {error.Description}");
                    }
                }
            }
        }
    }
}