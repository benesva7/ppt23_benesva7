using System.Xml.Linq;

namespace Blazor.ViewModels
{
    public class VybaveniVm
    {



        public string name { get; set; } 
        public DateTime boughtDateTime { get; set; }
        public DateTime lastRevisionDateTime { get; set; }
        public string IsRevisionNeeded
        {
            get
            {
                return (DateTime.Now - lastRevisionDateTime).TotalDays > 730 ? "Ano" : "Ne";
            }
        }

        public VybaveniVm(string name, DateTime boughtDateTime, DateTime lastRevisionDateTime)
        {
            this.name = name;
            if (lastRevisionDateTime < boughtDateTime)
            {
                this.boughtDateTime = lastRevisionDateTime;
                this.lastRevisionDateTime = boughtDateTime;
            }
            else
            {
                this.boughtDateTime = boughtDateTime;
                this.lastRevisionDateTime = lastRevisionDateTime;
            }
        }
        

            public static List<VybaveniVm> VratRandSeznam(int pocet)
            {
            // return new List<VybaveniVm>() { new VybaveniVm() { Name = "dkdk" }, new VybaveniVm() { Name = "KDIEI" } };
            List<VybaveniVm> list = new List<VybaveniVm>();

            for (int i = 0; i < pocet; i++)
            {

                string name = RandomString();
                DateTime boughtDateTime = RandomDay(new DateTime(2000, 1, 1));
                DateTime lastRevisionDateTime = RandomDay(DateTime.Now.AddYears(-4));

                list.Add(new VybaveniVm(name, boughtDateTime, lastRevisionDateTime));
            }
            return list;
            }
        private static DateTime RandomDay(DateTime start)
        {
            //DateTime start = new DateTime(2000, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(new Random().Next(range));
        }
        

        public static string RandomString()
        {
            int length = new Random().Next(10, 21); // generuje náhodné číslo v rozsahu 10-20 (včetně)

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }


    }
}

