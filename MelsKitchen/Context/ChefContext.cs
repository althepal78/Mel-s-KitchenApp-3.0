using MelsKitchen.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MelsKitchen.Context
{
    public class ChefContext : DbContext
    {
        public ChefContext(DbContextOptions<ChefContext> options) : base(options) { }

        public DbSet<Chef> Chefs { get; set; }

        public DbSet<Recipe> Recipes { get; set; }


    }
}
