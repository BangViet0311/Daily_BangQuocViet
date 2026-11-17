namespace day6_movie.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public String? Name { get; set; }
        public ICollection<Movie> Movie { get; set; } = new List<Movie>();
    }
}
