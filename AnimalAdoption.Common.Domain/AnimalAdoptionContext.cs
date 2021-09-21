using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalAdoption.Common.Domain
{
    public class AnimalAdoptionContext: DbContext
    {
        public AnimalAdoptionContext(DbContextOptions<AnimalAdoptionContext> options) : base(options) 
        {
        }

        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Animal>()
                .HasKey(x => x.Id);
        }
    }
}
