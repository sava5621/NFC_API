using Microsoft.EntityFrameworkCore;
using NFC_API.Model;
using NFC_API.OldModel;

namespace NFC_API.Services
{
    public class DataServices
    {
        dbContext db;
        public DataServices(dbContext db)
        {
            this.db = db;
        }
        public async Task<List<RFIDModelDB>> GetData()
        {
            return await db.RFID.ToListAsync();
        }
    }
}
