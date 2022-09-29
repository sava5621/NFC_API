using System.Xml.Linq;

namespace NFC_API.Model
{
    public class UserRefreshToken : UserAccessToken
    {
        public string RefreshToken { get; set; }
        public Dictionary<string, string> GetRefreshDict()
        {
            if (RefreshToken == null)
            {
                Dictionary<string, string> dict = GetAccessDict();
                dict.Add("RefreshToken", RefreshToken);
                return dict;
            }
            return null;
        }
        public UserAccessToken GetAccessToken()
        {
            return new UserAccessToken()
{
                Name = Name,
                AccessToken = AccessToken,
                CreationTime = CreationTime,
                ExpirationTime = ExpirationTime
            };
        }
    }
}
