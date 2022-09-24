using System.ComponentModel.DataAnnotations;

namespace NFC_API.OldModel
{
    public class RFIDModelDB : RFIDModel
    {
        [Key]
        public int id { get; set; }
    }
}
