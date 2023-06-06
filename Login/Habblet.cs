using RetroCounter.Models;
using System.Text.RegularExpressions;

namespace RetroCounter.Login
{
    public class Habblet
    {
        public static async Task<string> Login(HotelModel? hotel, UserModel? user ) 
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36");

            string body = await client.GetStringAsync("https://www.habblet.city/");
            string _asteroid = Regex.Match(body, "<input type=\"hidden\" name=\"_asteroid\" value=\"(.+)\">").Groups[1].Value;
            string usersOnline = Regex.Match(body, "<a style=\"border-left: 0 !important;\"><strong>(.+)</strong> Habblets no hotel!</a>").Groups[1].Value;

            if (usersOnline.Contains(','))
                usersOnline = usersOnline.Replace(",", "");

            hotel.UsersCountCMS = usersOnline;

            Dictionary<string, string> form = new Dictionary<string, string>
            {
                ["credentials_username"] = user.UserName,
                ["credentials_password"] = user.Password,
                ["_asteroid"] = _asteroid
            };

            await client.PostAsync("https://www.habblet.city/account/submit", new FormUrlEncodedContent(form));

            string profileBody = await client.GetStringAsync("https://www.habblet.city/me");
            string clientBody = await client.GetStringAsync("https://www.habblet.city/hotel");

            string sso = Regex.Match(clientBody, "\"sso.ticket\": \"(.+)\"").Groups[1].Value;
            return sso;
        }
    }
}