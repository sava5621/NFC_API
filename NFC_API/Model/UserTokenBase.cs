namespace NFC_API.Model
{
    public class UserTokenBase
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }

        public Dictionary<string, string> GetDict()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Name", Name);
            dict.Add("CreationTime", CreationTime.Ticks.ToString());
            dict.Add("ExpirationTime", ExpirationTime.Ticks.ToString());
            return dict;
        }
    }
}
