using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Models
{
    public class vmSetupTable1
    {
        [Required(ErrorMessage = "Description is required"), StringLength(200)]
        public string Description { get; set; }
    }
}
