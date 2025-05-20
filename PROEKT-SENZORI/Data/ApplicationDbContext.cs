using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROEKT_SENZORI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<DATA> DATA { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DATA>().ToTable("DATA");
   

    }
}
