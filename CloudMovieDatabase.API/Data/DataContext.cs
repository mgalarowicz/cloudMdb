using CloudMovieDatabase.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudMovieDatabase.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ActorMovie> ActorMovie { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .Property(e => e.Genre)
                .HasConversion<string>();

            builder.Entity<ActorMovie>(actorMovie => 
            {
                actorMovie.HasKey(am => new {am.ActorId, am.MovieId});

                actorMovie.HasOne(am => am.Actor)
                    .WithMany(a => a.Filmography)
                    .HasForeignKey(am => am.ActorId)
                    .OnDelete(DeleteBehavior.Cascade);

                actorMovie.HasOne(am => am.Movie)
                    .WithMany(m => m.StarringActors)
                    .HasForeignKey(am => am.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}