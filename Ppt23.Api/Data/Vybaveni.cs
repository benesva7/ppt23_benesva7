using System.ComponentModel.DataAnnotations;

namespace Ppt23.Api.Data;

public class Vybaveni
{

    public Guid Id { get; set; }
    public string Name { get; set; } = "";

    public double Price { get; set; }

    public DateTime BoughtDateTime { get; set; }

    public DateTime LastRevisionDateTime { get; set; }
    

}