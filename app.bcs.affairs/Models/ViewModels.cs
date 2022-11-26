using System.ComponentModel.DataAnnotations;

namespace app.bcs.affairs.Models
{
    public class vmSetupTable1
    {
        [Required(ErrorMessage = "Description is required"), StringLength(200)]
        public string Description { get; set; }
    }
}
