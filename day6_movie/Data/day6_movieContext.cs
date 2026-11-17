using Microsoft.EntityFrameworkCore;

public class day6_movieContext(DbContextOptions<day6_movieContext> options) : DbContext(options)
{
    public DbSet<day6_movie.Models.Movie> Movie { get; set; } = default!;
    public DbSet<day6_movie.Models.Genre> Genre { get; set; } = default!;
}
