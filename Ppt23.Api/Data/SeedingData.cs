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
            }

            await db.SaveChangesAsync();
        }
    }
}
