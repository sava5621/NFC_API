namespace NFC_API.Model
{
    public class UserAccessToken : UserTokenBase
    {
        public string AccessToken { get; set; }

        public Dictionary<string, string> GetAccessDict()
        {
            if (AccessToken != null)
            {
                Dictionary<string, string> dict = GetDict();
                dict.Add("AccessToken", AccessToken);
                return dict;
            }
            return null;
        }
    }
}
