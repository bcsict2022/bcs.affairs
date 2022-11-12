using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Entities
{
    public class BethelTypes
    {
        [Key]
        public int Id { get; set; }
        public string BethelTypeDescription { get; set; }
    }
}
