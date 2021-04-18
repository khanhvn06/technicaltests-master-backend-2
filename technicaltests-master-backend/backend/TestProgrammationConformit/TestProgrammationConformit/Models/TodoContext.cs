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


        protected override void OnModelCreating(ModelBuilder modelBuilder)//Config one to many
        {
            modelBuilder.Entity<Commentaire>()
                .HasOne<Evenement>(s => s.Evenement)
                .WithMany(g => g.Commentaire)
                .HasForeignKey(s => s.EvenementId);

           /* modelBuilder.Entity<Evenement>()
                .HasMany<Commentaire>(g => g.Commentaire)
                .WithOne(s => s.Evenement)
                .HasForeignKey(s => s.EvenementId)
                .OnDelete(DeleteBehavior.Cascade);*/
        }

    }
}

