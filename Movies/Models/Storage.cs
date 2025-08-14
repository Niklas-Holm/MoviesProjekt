using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Avalonia.Diagnostics.Screenshots;

namespace Movies.Models
{
    internal class Storage
    {
        private readonly FileHandler _fileHandler;
        private readonly List<Movie> _movies;

        public Storage(FileHandler fileHandler)
        {
            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
            _movies = _fileHandler.LoadFromStorage() ?? new List<Movie>();
        }

        /// Tilføjer film
        public void Add(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie), "Movie cannot be null.");
            }
            if (string.IsNullOrWhiteSpace(movie.Title))
            {
                throw new ArgumentException("Movie title cannot be null or empty.", nameof(movie));
            }
            if (movie.DurationInMin <= 0)
            {
                throw new ArgumentException("Movie duration must be greater than zero.", nameof(movie));
            }
            if (movie.Id <= 0)
            {
                throw new ArgumentException("Movie ID must be greater than zero.", nameof(movie));
            }

            _movies.Add(movie);
            Save();
        }
    }
}
