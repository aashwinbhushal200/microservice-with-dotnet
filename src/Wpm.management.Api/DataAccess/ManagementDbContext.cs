using Microsoft.EntityFrameworkCore;

namespace Wpm.management.Api.DataAccess
{
    public class ManagementDbContext(DbContextOptions<ManagementDbContext> options) : DbContext(options)
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Breed> Breeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Breed>().HasData([new Breed(1, "Beagle"), new Breed(2, "staffored")]);

            modelBuilder.Entity<Pet>().HasData([new Pet() { Id=1, Age=3, Name="BeagNmae", BreedId=1 },
                new Pet() { Id=2, Age=13, Name="BeagNmae", BreedId=1 },
            new Pet() { Id=3, Age=3, Name="Stoffoe", BreedId=2 }]);
        }

    }
    public static class ManagementDbContextExtension
    {
        public static void EnsureDbIsCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ManagementDbContext>();
            context!.Database.EnsureCreated();
        }
    }

    public class Pet
    {
        public int Id { get; set; }
        public int Age {  get; set; }
        public string Name { get; set; }
        public int BreedId {  get; set; }
        public Breed Breed { get; set; }

    }
    public record Breed(int id,string name);

}
