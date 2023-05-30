using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ppt23.Shared
{
    public class UkonyVm
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string PracovnikName { get; set; } = string.Empty;

    }
}
