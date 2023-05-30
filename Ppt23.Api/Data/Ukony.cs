namespace Ppt23.Api.Data
{
    public class Ukony
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime ActionDateTime { get; set; }
        public Guid VybaveniId { get; set; }
        public Vybaveni Vybaveni { get; set; } = null!;
        public Guid PracovnikId { get; set; }
        public Pracovnik Pracovnik { get; set; } = null!;
    }
}
