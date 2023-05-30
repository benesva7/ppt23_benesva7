using Ppt23.Shared;

namespace Ppt23.Api.Data
{
    public class Pracovnik
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;

        string[] jobTitles = { "doktor", "doktorka", "zdravotní sestra", "zdravotní bratr" };

        public Pracovnik()
        {
            Random random = new Random();
            Id = Guid.NewGuid();
            Name = VybaveniVm.RandomString();
            JobTitle = jobTitles[random.Next(jobTitles.Length)];
        }
    }

}
