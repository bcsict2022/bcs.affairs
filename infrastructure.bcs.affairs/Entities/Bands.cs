using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Entities
{
    public partial class Bands
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
    }

    public partial class UserBands
    {
        [Key] 
        public string UserId { get; set; }
        public int BandId { get; set; }
    }
    public partial class UserBandLists
    {
        [Key]
        public string UserId { get; set; }
        public int BandId { get; set; }
        public string BandName { get; set; }
    }
}
