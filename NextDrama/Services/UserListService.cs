using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Maui.Storage;
using NextDrama.Models; // 🔹 Se till att detta finns

namespace NextDrama.Services
{
    public static class UserListService
    {
        private const string UserListKey = "UserSeriesList";

        public static void AddToUserList(TvShow show, string category)
        {
            var userList = GetUserList();
            if (!userList.ContainsKey(show.Id))
            {
                userList[show.Id] = (show, category);
                SaveUserList(userList);
            }
        }

        public static Dictionary<int, (TvShow Show, string Category)> GetUserList()
        {
            if (Preferences.ContainsKey(UserListKey))
            {
                string json = Preferences.Get(UserListKey, "");
                return JsonSerializer.Deserialize<Dictionary<int, (TvShow, string)>>(json) ?? new();
            }
            return new();
        }

        private static void SaveUserList(Dictionary<int, (TvShow Show, string Category)> userList)
        {
            string json = JsonSerializer.Serialize(userList);
            Preferences.Set(UserListKey, json);
        }
    }
}

