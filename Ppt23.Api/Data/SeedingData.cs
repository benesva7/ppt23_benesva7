using Mapster;
using Ppt23.Api.Data;
using Ppt23.Shared;


namespace Ppt23.Api.Data
{
    public class SeedingData
    {
        PptDbContext db;
        public SeedingData(PptDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task SeedData()
        {
            if (!db.Vybavenis.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var vyb = new VybaveniVm();
                    vyb.Name = VybaveniVm.RandomString();
                    var en = vyb.Adapt<Vybaveni>();


                    db.Vybavenis.Add(en);


                }
                db.SaveChanges();
                foreach (var item in db.Vybavenis)
                {
                    var novaRevize = new Revize
                    {
                        DateTime = VybaveniVm.RandomDay(item.BoughtDateTime),
                        VybaveniId = item.Id,
                        Name = item.Name,

                    };
                    db.Revizes.Add(novaRevize);

                }
                int toSkip = Random.Shared.Next(0, db.Pracovniks.Count());
                var pracovnik = db.Pracovniks.Skip(toSkip).Take(1).First();
                foreach (var num in Enumerable.Range(0, Random.Shared.Next(0, 20)))
                {

                    Ukony ukon = new()
                    {
                        Name = VybaveniVm.RandomString(),
                        ActionDateTime = DateTime.Now,
                        PracovnikId = pracovnik.Id,
                        Pracovnik = pracovnik
                };
                    db.Ukonys.Add(ukon);
                    
                }
                foreach (var num in Enumerable.Range(0, Random.Shared.Next(0, 20)))
                {
                    Pracovnik pracovni = new Pracovnik();
                   

                db.Pracovniks.Add(pracovni);
                }
            }

            await db.SaveChangesAsync();
        }
    }
}
