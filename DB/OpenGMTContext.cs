using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace OpenGMT
{
    public class OpenGMTContext : DbContext
    {
        private string _basePath;
        private string _dbFile;

        public OpenGMTContext(IWebHostEnvironment environment)
        {
            _basePath = Path.Combine(environment.ContentRootPath, "data");
            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
            _dbFile = Path.Combine(_basePath, "OpenGMT.sqlite");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={_dbFile}");
    
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Session> Sessions { get; set; }

        internal void ImportJson()
        {
            string dataFile = Path.Combine(_basePath, "creatures.json");
            if (Directory.Exists(_basePath) && System.IO.File.Exists(dataFile))
            {
                string data = System.IO.File.ReadAllText(dataFile);
                var importedCreates = JsonSerializer.Deserialize<Creature[]>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                foreach (var c in importedCreates)
                {
                    var existing = Creatures.FirstOrDefault(x => x.Id == c.Id);
                    if (existing != null)
                        Creatures.Remove(existing);
                }
                SaveChanges();
                Creatures.AddRange(importedCreates);
/*
                foreach (var d in importedCreates)
                {
                    if (d.Attacks != null)
                        foreach (var a in d.Attacks)
                        {
                            a.Id = 0;
                            this.Entry<Attack>(a).State = EntityState.Added;
                        }

                    
                    this.Entry<Creature>(d).State = EntityState.Modified;
                }*/
                SaveChanges();
            }
            
            
            /*modelBuilder.Entity<Creature>().HasData(
                new Creature
                {
                    AuthorId = 1,
                    FirstName = "William",
                    LastName = "Shakespeare"
                }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, AuthorId = 1, Title = "Hamlet" },
                new Book { BookId = 2, AuthorId = 1, Title = "King Lear" },
                new Book { BookId = 3, AuthorId = 1, Title = "Othello" }
            );*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Creature>()
                .HasMany(x => x.Attacks)
                .WithOne(x => x.Creature)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    } 
}