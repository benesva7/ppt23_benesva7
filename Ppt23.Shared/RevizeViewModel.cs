
namespace Ppt23.Shared;


public class RevizeViewModel
{
    public string Name { get; set; } = "";

    public Guid Id { get; set; }

    public static List<RevizeViewModel> NahodnySeznam(int v)
    {
        List<RevizeViewModel> list = new();

        Random random = new();

        for (int i = 0; i < v - 1; i++)
        {
            var r = new RevizeViewModel { Id = Guid.NewGuid(), Name = VybaveniVm.RandomString() };
            list.Add(r);
        }
        return list;
    }
}
