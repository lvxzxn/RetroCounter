using Newtonsoft.Json;

using RetroCounter.Models;
using RetroCounter.Models.Lella;

using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

namespace RetroCounter.Login
{
    public class Lella
    {
        public static async Task<string> Login(HotelModel? hotel, UserModel? user) 
        {
            string json = "{\"username\":\""+user.UserName+"\",\"password\":\""+user.Password+"\"}";
            using HttpClient client = new();

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string apiUsersOnline = await client.GetStringAsync("https://lella.ws/api/users_online");
            var users = JsonConvert.DeserializeObject<UsersOnlineModel>(apiUsersOnline);

            hotel.UsersCountCMS = users.count.ToString();

            var response = await client.PostAsync("https://lella.ws/api/user_login", new StringContent(json, Encoding.UTF8, "application/json"));
            var _user = JsonConvert.DeserializeObject<UserDataModel>(await response.Content.ReadAsStringAsync());

            string clientBody = await client.GetStringAsync($"https://lella.ws/clients/flash.php?token={_user.Token}");
            string sso = Regex.Match(clientBody, "\"sso.ticket\": \"(.+)\"").Groups[1].Value;

            return sso;

        }
    }
}
