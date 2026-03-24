using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Med.Core.Models
{
    public class MedicineHistory
    {
        public long Id { get; set; }
        public long MedicineId { get; set; }
        public Medicine Medicine { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }
}
