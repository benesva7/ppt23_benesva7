using Ppt23.Shared;

namespace Ppt23.Api.Data
{
    public class Revize
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateTime { get; set; }
        public Guid VybaveniId { get; set; }
        public Vybaveni Vybaveni { get; set; } = null!;
    }
}
