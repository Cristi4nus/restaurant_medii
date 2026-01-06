using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using restaurant_medii.Models;

namespace restaurant_medii.Data
{
    public class restaurant_mediiContext : DbContext
    {
        public restaurant_mediiContext (DbContextOptions<restaurant_mediiContext> options)
            : base(options)
        {
        }

        public DbSet<restaurant_medii.Models.Produs> Produs { get; set; } = default!;
        public DbSet<restaurant_medii.Models.Categorie> Categorie { get; set; } = default!;
    }
}
