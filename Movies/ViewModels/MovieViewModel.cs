using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Movies.Models;

namespace Movies.ViewModels;

public class MovieViewModel : INotifyPropertyChanged
{
    private readonly Storage _storage;

    public string Greeting => "Movies!";

    private string _title = string.Empty;
    public string Title
    {
        get => _title;
        set { if (SetField(ref _title, value)) SaveMovieCommand_RaiseCanExecute(); }
    }

    private int _durationInMin;
    public int DurationInMin
    {
        get => _durationInMin;
        set { if (SetField(ref _durationInMin, value)) SaveMovieCommand_RaiseCanExecute(); }
    }
    
    public IReadOnlyList<MovieGenre> Genres { get; } = Enum.GetValues<MovieGenre>();

    private MovieGenre _genre = MovieGenre.Action;
    public MovieGenre Genre
    {
        get => _genre;
        set => SetField(ref _genre, value);
    }

    public ObservableCollection<Movie> Movies { get; }

    public ICommand SaveMovieCommand { get; }

    public MovieViewModel()
    {
        var fileHandler = new FileHandler();
        _storage = new Storage(fileHandler);

        Movies = new ObservableCollection<Movie>(_storage.GetMovies());

        SaveMovieCommand = new RelayCommand(SaveMovie, CanSaveMovie);
    }

    private bool CanSaveMovie() =>
        !string.IsNullOrWhiteSpace(Title) && DurationInMin > 0;

    private void SaveMovieCommand_RaiseCanExecute()
    {
        if (SaveMovieCommand is RelayCommand rc) rc.RaiseCanExecuteChanged();
    }

    private void SaveMovie()
    {
        var movie = new Movie
        {
            Title = this.Title.Trim(),
            DurationInMin = this.DurationInMin,
            Genre = this.Genre
        };

        _storage.Add(movie);

        Movies.Add(movie);

        Title = string.Empty;
        DurationInMin = 0;
        Genre = MovieGenre.Action;
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