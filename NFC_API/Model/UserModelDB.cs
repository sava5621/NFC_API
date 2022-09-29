using System.ComponentModel.DataAnnotations;

namespace NFC_API.Model
{
    public class UserModelDB
    {
        [Key]
        public int id { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
        public string salt { get; set; }
    }
}
