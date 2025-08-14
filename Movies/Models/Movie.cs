namespace Movies.Models;

public enum MovieGenre
{
    Action,
    Comedy,
    Drama,
    Horror,
    SciFi,
    Romance,
    Documentary,
    Other
}

public class Movie
{
    public int Id { get; private set; }
    public string Title { get; set; }
    public int DurationInMin { get; set; }
    public string Genre { get; set; }

    public Movie()
    {
        Id = _random.Next(1, 1_000_000);
    }
}