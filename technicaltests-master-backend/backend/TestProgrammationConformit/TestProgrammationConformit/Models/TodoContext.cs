using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestProgrammationConformit.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {


        }

        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)// relationship one to many
        {
            modelBuilder.Entity<Commentaire>()
                .HasOne(c => c.Evenement)
                .WithMany(e => e.Commentaires)
                .HasForeignKey(c => c.EvenementId);
        }

    }
}

