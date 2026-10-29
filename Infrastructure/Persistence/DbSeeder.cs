namespace Infrastructure.Persistence;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Ensure the database and schema are created
        await context.Database.EnsureCreatedAsync();

        // Check if students already exist to make seeding idempotent
        if (await context.Students.AnyAsync())
        {
            return; // Database has already been seeded, exit early
        }

        var students = new List<Student>
        {
            Student.Create("John", "Doe", "john.doe@example.com", new DateTime(2002, 5, 14)),
            Student.Create("Jane", "Smith", "jane.smith@example.com", new DateTime(2003, 8, 22))
        };

        await context.Students.AddRangeAsync(students);
        await context.SaveChangesAsync();
    }
}