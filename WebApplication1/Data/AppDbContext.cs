using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{

    public DbSet<Actor> Actors { get; set; }
    public DbSet<Actor_Movie> Actors_Movies { get; set; }
    public DbSet<Producer> Producers { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    //DbCont4ext classı bazı optionlarbekler.(DbContextOptions generic tipte bir class istiyor. DbContexten miras alan bir classı generic kısma yazmak istiyor. sonra bunu base classa option olarak ilet.
    {


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor_Movie>().HasKey(ma => new
        {
            ma.ActorId,
            ma.MovieId
        });

        modelBuilder.Entity<Actor_Movie>()
            .HasOne(m => m.Movie)
            .WithMany(ma => ma.Actors_Movies)
            .HasForeignKey(ma => ma.MovieId);

        modelBuilder.Entity<Actor_Movie>()
          .HasOne(m => m.Actor)
          .WithMany(ma => ma.Actors_Movies)
          .HasForeignKey(ma => ma.ActorId);



        base.OnModelCreating(modelBuilder);
    }


}

