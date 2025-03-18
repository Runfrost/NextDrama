using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NextDrama.Models
{
    public class TvShowResponse
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("results")]
        public List<TvShow> Results { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }

    public class TvShow
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("original_name")]
        public string OriginalName { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        public string FullPosterUrl => $"https://image.tmdb.org/t/p/w500{PosterPath}"; // ✅ Fixar bildladdning

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonPropertyName("vote_average")]
        public float VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("popularity")]
        public float Popularity { get; set; }

        [JsonPropertyName("origin_country")]
        public List<string> OriginCountry { get; set; } = new List<string>(); // 🔹 Säkerställer att listan inte är null

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; } = new List<int>(); // 🔹 Säkerställer att listan inte är null

        [JsonPropertyName("adult")]
        public bool Adult { get; set; }

        // Ny egenskap som returnerar en kommaseparerad sträng av alla genrer
        public string GenreString => string.Join(", ", GenreIds.Select(id => GetGenreName(id)));

        // Ny egenskap som returnerar en kommaseparerad sträng av alla ursprungsländer
        public string OriginCountryString => string.Join(", ", OriginCountry);

        // En metod för att få genre-namn från genre-id (det här måste du definiera för att få korrekta namn)
        private string GetGenreName(int genreId)
        {
            // Här kan du skapa en metod för att hämta genre-namn från genre-id.
            // T.ex. genom att använda en hårdkodad lista, en API-anrop eller någon annan metod.
            // För nu, här är ett exempel där vi bara ger ett exempel:
            var genreNames = new Dictionary<int, string>
        {
            { 28, "Action" },
            { 35, "Comedy" },
            { 18, "Drama" }
        };

            return genreNames.ContainsKey(genreId) ? genreNames[genreId] : "Unknown";
        }
    }



}

