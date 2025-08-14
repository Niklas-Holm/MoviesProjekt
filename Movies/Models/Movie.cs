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
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public int DurationInMin { get; set; }
    public MovieGenre Genre { get; set; }
}