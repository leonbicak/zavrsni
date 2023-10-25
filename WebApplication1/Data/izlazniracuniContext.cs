using izlazniracuni.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace izlazniracuni.Data
{
    public class izlazniracuniContext : DbContext
    {
        public izlazniracuniContext(DbContextOptions<izlazniracuniContext> opcije)
            : base(opcije)
        {
        }
        public DbSet<kupac2> kupac2 { get; set; }
        public DbSet<ugovor> ugovor { get; set; }

        public DbSet<izlazni_racun> izlazni_racun { get; set; }


        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            // implementacija veze 1:n
            modelBuilder.Entity<ugovor>().HasOne(u => u.kupac2);

            modelBuilder.Entity<izlazni_racun>().HasOne(i => i.ugovor);

        }
    }
}
