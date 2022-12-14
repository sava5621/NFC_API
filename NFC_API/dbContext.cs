using Microsoft.EntityFrameworkCore;
using NFC_API.Model;
using NFC_API.OldModel;
using System.Collections.Generic;

namespace NFC_API
{
    public class dbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<RFIDModelDB>? RFID { get; set; }
        public DbSet<UserModelDB>? USER { get; set; }
        public dbContext(DbContextOptions<dbContext> options)
           : base(options) { }
    }
}
