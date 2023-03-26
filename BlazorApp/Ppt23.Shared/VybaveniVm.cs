using System.ComponentModel.DataAnnotations;

namespace Ppt23.Shared
{
    public class VybaveniVm
    {
        [Required(ErrorMessage = "❗Pole Název nesmí být prázdné❗")]
        [MinLength(5, ErrorMessage = "❗Délka u pole \"{0}\" musí být alespoň {1} znaků❗")]
        [Display(Name = "Název")]
        public string Name { get; set; } = "";
        [Required(ErrorMessage = "❗Pole Cena nesmí být prázdné❗")]
        [Range(0, 10000001, ErrorMessage = "❗Cena musí být mezi 0 až 10,000,000❗")]
        public int Price { get; set; }
        [Required(ErrorMessage = "❗Pole Datum nákupu nesmí být prázdné❗")]
        [DataType(DataType.Date, ErrorMessage = "❗Datum nákupu musí být zadán ve správném tvaruv❗")]
        public DateTime BoughtDateTime { get; set; }
        [Required(ErrorMessage = "❗Pole Datum poslední revize nesmí být prázdné❗")]
        [DataType(DataType.Date, ErrorMessage = "❗Datum poslední revize musí být zadán ve správném tvaru❗")]
        [CustomValidation(typeof(VybaveniVm), nameof(Validation))]
        public DateTime LastRevisionDateTime { get; set; }
        public bool IsRevisionNeeded { get => LastRevisionDateTime < DateTime.Now.AddYears(-2);}
        public bool IsInEditMode { get; set; }
        public Guid Id { get; set; }

        public VybaveniVm(string Name,int Price, DateTime BoughtDateTime, DateTime LastRevisionDateTime, Guid Id)
        {

            this.Name = Name;
            this.Price = Price;
            this.BoughtDateTime = BoughtDateTime;
            this.LastRevisionDateTime = LastRevisionDateTime; 
            if (LastRevisionDateTime < this.BoughtDateTime)
            {
                this.BoughtDateTime = LastRevisionDateTime;
                this.LastRevisionDateTime = this.BoughtDateTime;
            }
            else
            {
                this.BoughtDateTime = this.BoughtDateTime;
                this.LastRevisionDateTime = LastRevisionDateTime;
            }
            this.Id = Id;

        }
        public VybaveniVm Copy()
        {
            VybaveniVm to = new(Name, Price, BoughtDateTime, LastRevisionDateTime,Id);
            to.IsInEditMode = IsInEditMode;
            return to;
        }
        public void MapTo(VybaveniVm? to)
        {
            if (to == null) return;
            to.Name = Name;
            to.Price = Price;
            to.BoughtDateTime = BoughtDateTime;
            to.LastRevisionDateTime = LastRevisionDateTime;
            to.Id = Id;
        }
        public static ValidationResult? Validation(DateTime LastRevisionDatetime, ValidationContext validationContext)
        {
            var vm = (VybaveniVm)validationContext.ObjectInstance;

            if (LastRevisionDatetime < vm.BoughtDateTime)
            {
                return new ValidationResult("❗Datum poslední revize nesmí předcházet datu zakoupení vybavení❗");
            }

            return ValidationResult.Success;
        }


        public static List<VybaveniVm> VratRandSeznam(int pocet)
            {
            // return new List<VybaveniVm>() { new VybaveniVm() { Name = "dkdk" }, new VybaveniVm() { Name = "KDIEI" } };
            List<VybaveniVm> list = new List<VybaveniVm>();

            for (int i = 0; i < pocet; i++)
            {

                string Name = RandomString();
                int Price = Random.Shared.Next(10000001);
                DateTime BoughtDateTime = RandomDay(new DateTime(2000, 1, 1));
                DateTime LastRevisionDateTime = RandomDay(DateTime.Now.AddYears(-4));
                Guid Id = Guid.NewGuid();

                list.Add(new VybaveniVm(Name,Price, BoughtDateTime, LastRevisionDateTime,Id));
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

