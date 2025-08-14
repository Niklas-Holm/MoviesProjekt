using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Movies.Models
{
    public class Storage
    {
        // Kompisition til fileHandler
        private readonly FileHandler _fileHandler;
        // 0..* Movie
        private readonly List<Movie> _movies;

        public Storage(FileHandler fileHandler)
        {
            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
            _movies = _fileHandler.LoadFromStorage() ?? new List<Movie>();
        }

        /// Tilføjer film
        public void Add(Movie movie)
        {
            /// Fejlhåndtering for AddMovie-metoden:
            // // Tjekker om filmen er null
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie), "Movie cannot be null.");
            }
            // Tjekker om filmen har en gyldig titel
            if (string.IsNullOrWhiteSpace(movie.Title))
            {
                throw new ArgumentException("Movie title cannot be null or empty.", nameof(movie));
            }
            // Tjekker om filmen har en gyldig varighed
            if (movie.DurationInMin <= 0)
            {
                throw new ArgumentException("Movie duration must be greater than zero.", nameof(movie));
            }
            // Tjekker om filmen har et gyldigt ID
            if (movie.Id == Guid.Empty)
            {
                throw new ArgumentException("Movie ID cannot be empty.", nameof(movie));
            }
            // Tjekker om filmen allerede findes i listen
            if (_movies.Any(m => m.Id == movie.Id))
            {
                throw new InvalidOperationException("Movie with the same ID already exists.");
            }

            _movies.Add(movie);
            // Storage kalder fileHandler.SaveToStorage for at gemme ændringerne
            _fileHandler.SaveToStorage(_movies);
        }

        // getMovies() : List
        public List<Movie> GetMovies()
        {
            // Returnerer en kopi af listen for at undgå ændringer direkte
            return new List<Movie>(_movies);
        }
    }
}
