using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace day6_movie.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 60 characters.")]
        public String? Title { get; set; }
        [DataType (DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0", "9999999999999999.99", ErrorMessage = "Price must be non-negative and fit decimal(18,2).")]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please select a genre.")]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
