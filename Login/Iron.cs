using RetroCounter.Models;
using System.Text.RegularExpressions;

namespace RetroCounter.Login
{
    public class Iron
    {
        public static async Task<string> Login(UserModel? user)
        {

            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36");

            Dictionary<string, string> form = new Dictionary<string, string> 
            {
                ["username"] = user.UserName,
                ["password"] = user.Password,
                ["token"] = ""
            };

            await client.PostAsync("https://ironhotel.biz/callback/bruteLogin", new FormUrlEncodedContent(form));

            string clientBody = await client.GetStringAsync("https://ironhotel.biz/client");
            string sso = Regex.Match(clientBody, "\"sso.ticket\": \"(.+)\",").Groups[1].Value;

            return sso;
        }
    }
}
