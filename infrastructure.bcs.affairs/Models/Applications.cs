using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Models
{
    public class GetProfileMenuPermissions
    {
        [Key]
        public int Id { get; set; }
        public string Parent { get; set; }
        public string Title { get; set; }
    }
    public class ProfileMenus
    {
        public string menuGroup { get; set; }
        public string menuHeads { get; set; }
        public string menuLines { get; set; }
    }
}
