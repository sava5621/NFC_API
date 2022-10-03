using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NFC_API.OldModel;
using NFC_API.Services;

namespace NFC_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly dbContext db;

        public DataController(dbContext dbContext)
        {
            db = dbContext;
            
        }
        [HttpGet("Hi")]
        public async Task<string> Hi()
        {
            return "Hi";
        }
        [HttpGet("RemoveAt")]
        public async Task<ActionResult> RemoveAt(string key)
        {
            db.RFID.Remove(await db.RFID.FirstOrDefaultAsync(x => x.key == key));
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("Add")]
        public async Task<ActionResult> Add(string key,
                                            string title,
                                            string about,
                                            long tiameCreate,
                                            long tiameEnd)
        {
            db.RFID.Add(new RFIDModelDB()
            {
                title = title,
                key = key,
                about = about,
                tiameCreate = new DateTime(tiameCreate),
                tiameEnd = new DateTime(tiameEnd)
            });
            db.SaveChanges();
            return Ok();
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> Get()
        {
            List<RFIDAnsverModel> ansver = new();
            foreach (var i in await db.RFID.ToListAsync())
            {
                ansver.Add(new RFIDAnsverModel
                {
                    about = i.about,
                    key = i.key,
                    title = i.title,
                    tiameCreate = i.tiameCreate.Ticks,
                    tiameEnd = i.tiameEnd.Ticks
                });
            }
            return base.Content(JsonConvert.SerializeObject(ansver),
                     "application/json; charset=utf-8");
        }
        [HttpGet("Random")]
        public async Task<ActionResult> Random(int today, int tomorow, int other)
        {
            Random random = new Random();
            db.RFID.RemoveRange(await db.RFID.ToArrayAsync());
            await db.SaveChangesAsync();
            int count = 1;

            for (int i = 0; i < today; i++)
            {
                db.RFID.Add(new RFIDModelDB()
                {
                    title = $"Метка #{count}",
                    about = $"Описание для метки #{count}",
                    key = random.Next().ToString(),
                    tiameCreate = DateTime.Now.AddDays(-2.5),
                    tiameEnd = DateTime.Now.AddDays(-2 + random.NextDouble())

                });
                count++;
            }
            for (int i = 0; i < tomorow; i++)
            {
                db.RFID.Add(new RFIDModelDB()
                {
                    title = $"Метка #{count}",
                    about = $"Описание для метки #{count}",
                    key = random.Next().ToString(),
                    tiameCreate = DateTime.Now.AddDays(-2.5),
                    tiameEnd = DateTime.Now.AddDays(random.NextDouble())
                });
                count++;
            }
            for (int i = 0; i < other; i++)
            {
                db.RFID.Add(new RFIDModelDB()
                {
                    title = $"Метка #{count}",
                    about = $"Описание для метки #{count}",
                    key = random.Next().ToString(),
                    tiameCreate = DateTime.Now.AddDays(-2.5),
                    tiameEnd = DateTime.Now.AddDays(1 + random.NextDouble())
                });
                count++;
            }
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
