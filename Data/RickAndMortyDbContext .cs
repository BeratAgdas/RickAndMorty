using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class RickAndMortyDbContext: DbContext
    {
        public RickAndMortyDbContext(DbContextOptions<RickAndMortyDbContext> dbContextOptions): base(dbContextOptions) 
        {   
        }

        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterOrigin> CharacterOrigins { get; set; }

        public DbSet<CharacterLocation> CharacterLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<Episode>();
        }
    }
}
