using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Movies.Models;

namespace Movies.ViewModels;

public class MovieViewModel : INotifyPropertyChanged
{
    public string Greeting => "Movies!";
    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set => SetField(ref _title, value);
    }

    private int _durationInMin;
    public int DurationInMin
    {
        get => _durationInMin;
        set => SetField(ref _durationInMin, value);
    }
    
    public IReadOnlyList<MovieGenre> Genres { get; } = Enum.GetValues<MovieGenre>();

    // Valgt genre (init til noget ikke-null)
    private MovieGenre _genre = MovieGenre.Action;
    public MovieGenre Genre
    {
        get => _genre;
        set => SetField(ref _genre, value);
    }
    
    public ObservableCollection<Movie> Movies { get; }

    public MovieViewModel()
    {
        Movies = new ObservableCollection<Movie>(Storage.GetMovies());
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        return true;
    }
}