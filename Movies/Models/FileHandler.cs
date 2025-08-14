using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class FileHandler
    {
        // Stien til JSON-filen
        private readonly string _filePath;
        // Indstillinger til JSON-serialisering
        private readonly JsonSerializerOptions _jsonOptions;

        // Opretter en ny FileHandler.
        // Hvis FilePath er null eller tom, bruges standardstien
        public FileHandler(string? FilePath = null)
        {
            // Hvis FilePath er null eller tom, bruges standardstien
            _filePath = string.IsNullOrWhiteSpace(FilePath) ? GetDefaultPath() : FilePath!;
            // Sikrer at mappen til filen eksisterer
            EnsureDirectoryExists(_filePath);

            // Initialiserer JSON-serialiseringsindstillinger
            _jsonOptions = new JsonSerializerOptions
            {
                // Indstiller at JSON skal formateres pænt
                WriteIndented = true,
                // gør så enum konverteres til string
                Converters = { new JsonStringEnumConverter() },
            };
        }

        /// Loader listen af film fra storage. Returnerer tom liste hvis filmen ikke findes
        public List<Movie> LoadFromStorage()
        {
            try
            {
                // Tjekker om filen findes eller returner tom liste
                if (!File.Exists(_filePath))
                    return new List<Movie>();

                // Læser filen og deserialiserer JSON til en liste af Movie objekter
                using var stream = File.OpenRead(_filePath);
                // Deserialiserer JSON til en liste af Movie objekter
                var movies = JsonSerializer.Deserialize<List<Movie>>(stream, _jsonOptions);
                // Returnerer listen af film eller en tom liste hvis deserialisering fejler
                return movies ?? new List<Movie>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading movies from storage: {ex.Message}");
                return new List<Movie>();
            }
        }

        /// Gemmer listen af film
        public void SaveToStorage(IEnumerable<Movie> movies)
        {
            if (movies == null)
            {
                throw new ArgumentNullException(nameof(movies), "Movies cannot be null.");
            }
            // Skriv til *.tmp og erstat atomisk, så en afbrudt skrivning ikke korrumperer filen
            // Opret sti til temp fil
            var tmp = _filePath + ".tmp";
            // Konverterer listen af film til JSON
            var json = JsonSerializer.Serialize(movies, _jsonOptions);
            // Skriver JSON til den midlertidige fil
            File.WriteAllText(tmp, json);
            // Erstat gamle fil med nye
            File.Copy(tmp, _filePath, overwrite: true);
            // Sletter den midlertidige fil
            File.Delete(tmp);
        }

        /// Henter standardstien hvor det skal gemmes
        private static string GetDefaultPath()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var directory = Path.Combine(appData, "MoviesApp");
            return Path.Combine(directory, "movies.json");
        }

        // Sikrer at mappen til filen eksisterer ellers opretets den
        private static void EnsureDirectoryExists(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

    }
}
