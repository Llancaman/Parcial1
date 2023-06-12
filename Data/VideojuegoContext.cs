using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parcial1.Models;

namespace Parcial1.Data
{
    public class VideojuegoContext : IdentityDbContext
    {
        public VideojuegoContext (DbContextOptions<VideojuegoContext> options)
            : base(options)
        {
        }

        public DbSet<Parcial1.Models.Videojuego> Videojuego { get; set; } = default!;
        public DbSet<Parcial1.Models.Genero> Genero { get; set; } = default!;
        public DbSet<Parcial1.Models.Plataforma> Plataforma { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
