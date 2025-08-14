using System;
using System.Collections.Generic;
using Movies.Models;

namespace Movies.ViewModels;

public class MovieViewModel
{
    public string Greeting { get; } = "Movies!";
    public string Title { get; }
    
    private MovieGenre _genre;
    public IEnumerable<MovieGenre> Genres { get; } = Enum.GetValues<MovieGenre>();
    
    public MovieGenre Genre
    {
        get => _genre;
        set
        {
            if (_genre != value)
            {
                _genre = value;
            }
        }
    }
    
    public int DurationInMin { get; set; }
}