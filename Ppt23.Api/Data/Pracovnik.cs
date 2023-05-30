namespace Ppt23.Api.Data
{
    public class Pracovnik
    {
            public Guid Id { get; set; }

            public string Name { get; set; } = string.Empty;

            public string JobTitle { get; set; } = string.Empty;

            public List<Ukony> Ukons { get; set; } = new();
        
    }
}
