using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.bcs.affairs.Models
{
    public class vmUserDetails
    {
        [Key]
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsFirstLogin { get; set; }
        public bool? UserStatus { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? TransactionDate { get; set; }
    }

    public class vmUser
    {
        [Key, Required(ErrorMessage = "Email Address is required"), StringLength(50), DisplayName("Email Address")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "New Password is required"), DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "New Password is required"), DisplayName("First Name")]
        public string FirstName { get; set; }

        public string CreatedUser { get; set; }

    }
   
}
