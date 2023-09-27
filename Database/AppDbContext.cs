using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FitFusion.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitFusion.Database
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<ExerciciosModel> Exercicios { get; set; }

        public DbSet<TreinoModel> Treinos { get; set; }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<UsuarioModel>()
                .HasOne(u => u.Cargo)
                .WithMany(c => c.Usuarios)
                .HasForeignKey(u => u.CargoID);

            base.OnModelCreating(mb);
        }


    }
}