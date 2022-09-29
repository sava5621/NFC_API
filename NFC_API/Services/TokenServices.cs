using Newtonsoft.Json;
using NFC_API.Model;
using System.Security.Cryptography;
using System.Text;

namespace NFC_API.Services
{
    public class TokenServices
    {
        public static bool CheckTokenAsync(UserRefreshToken refreshToken, string salt)
        {
            string token = Encript(JsonConvert.SerializeObject(refreshToken.GetRefreshDict(), Formatting.Indented) + salt);
            if (token == refreshToken.RefreshToken) return true;
            else return false;
        }
        public static bool CheckTokenAsync(UserAccessToken accessToken, string salt)
        {
            string token = Encript(JsonConvert.SerializeObject(accessToken.GetAccessDict(), Formatting.Indented) + salt);
            if(token == accessToken.AccessToken) return true;
            else return false;
        }
        //может не нужно
        static async Task<UserAccessToken> GetAccessTokenAsync()
        {
            return null;
        }
        static async Task<UserRefreshToken> GetRefreshTokenAsync()
        {
            return null;
        }
        public static UserRefreshToken GetRefreshTokenAsync(UserModelDB user, int minLife)
        {
            UserRefreshToken userRefreshToken = new()
            {
                Name = user.login,
                CreationTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddMinutes(minLife)
            };
            userRefreshToken.AccessToken =
                Encript(JsonConvert.SerializeObject(userRefreshToken.GetDict(), Formatting.Indented)+user.salt);
            userRefreshToken.RefreshToken =
                Encript(JsonConvert.SerializeObject(userRefreshToken.GetAccessDict(), Formatting.Indented) + user.salt);

            return userRefreshToken;
            
        }
        private static string Encript(string data)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
