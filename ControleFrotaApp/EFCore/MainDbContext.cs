using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ControleFrota.Domain;
using Microsoft.EntityFrameworkCore;

namespace ControleFrota.EFCore
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {

        }


        public MainDbContext()
        {
        }

        public DbSet<Envio> Envios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
        }
    }
}
