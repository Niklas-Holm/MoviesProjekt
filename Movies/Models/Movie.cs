using System;

namespace Movies.Models;

public enum MovieGenre
{
    Other,
    Action,
    Comedy,
    Drama,
    Horror,
    SciFi,
    Romance,
    Documentary
}

public class Movie
{
    private static readonly Random _random = new Random();
    public int Id { get; private set; }
    public string Title { get; set; }
    public int DurationInMin { get; set; }
    public MovieGenre Genre { get; set; }

    public Movie()
    {
        Id = _random.Next(1, 1_000_000);
    }
}