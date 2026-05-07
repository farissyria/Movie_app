namespace Cinema.Application.DTOs
{
    public class MovieDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<string> Genres { get; set; } = new List<string>();
        public int DurationMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }

        public string Language { get; set; } = string.Empty;
    }    

    public class CreateMovieDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
       
        public List<string> Genres { get; set; } = new List<string>();
        public int DurationMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; } = 0;
        public string PosterUrl { get; set; } = string.Empty;
        public string Language { get; set; } = "English";
    }

    public class UpdateMovieDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
      
        public List<string> Genres { get; set; } = new List<string>();
        public int DurationMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
