using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Maui.Storage;
using NextDrama.Models;

namespace NextDrama.Services
{
    public static class UserListService
    {
        private const string UserListKey = "UserSeriesList";

        // 🔹 Lägger till en serie i listan och sparar
        public static void AddToUserList(TvShow show, string category)
        {
            var userList = GetUserList();

            if (!userList.ContainsKey(show.Id))
            {
                userList[show.Id] = (show, category);
                SaveUserList(userList);
            }
        }

        // 🔹 Hämtar listan från Preferences och hanterar JSON-fel
        public static Dictionary<int, (TvShow Show, string Category)> GetUserList()
        {
            try
            {
                if (Preferences.ContainsKey(UserListKey))
                {
                    string json = Preferences.Get(UserListKey, "");

                    // 🔥 Kontrollera om JSON är tom
                    if (string.IsNullOrWhiteSpace(json))
                    {
                        return new Dictionary<int, (TvShow, string)>();
                    }

                    // 🔹 Försöker avkoda JSON till rätt format
                    return JsonSerializer.Deserialize<Dictionary<int, (TvShow, string)>>(json) ?? new();
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"⚠️ JSON Error: {ex.Message}");

                // 🔥 Om JSON är trasig: Radera och returnera en tom lista
                Preferences.Remove(UserListKey);
            }

            return new Dictionary<int, (TvShow, string)>();
        }

        // 🔹 Sparar listan tillbaka i Preferences
        private static void SaveUserList(Dictionary<int, (TvShow Show, string Category)> userList)
        {
            try
            {
                string json = JsonSerializer.Serialize(userList);
                Preferences.Set(UserListKey, json);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"⚠️ JSON Save Error: {ex.Message}");
            }
        }

        // 🔹 Rensar listan manuellt (exempel för en knapp)
        public static void ClearUserList()
        {
            Preferences.Remove(UserListKey);
            Console.WriteLine("🗑️ Raderade användarens listor!");
        }
    }
}

