using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace app.bcs.affairs.Models
{
    public class vmBand
    {
        [Required(ErrorMessage = "Permission Group is required"), DisplayName("Profile")]
        public string GroupName { get; set; }
    }
    public partial class Bands
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
    }
    public class vmUserBand
    {
        [Required(ErrorMessage = "User Name is required"), DisplayName("User Name")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Permission Group is required"), DisplayName("Profile")]
        public int BandId { get; set; }
    }
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
        public int DepartmentId { get; set; }
        public string DepartmentDescription { get; set; }
        public string UserFullName { get; set; }
    }
    public class vmUser
    {
        [Key, Required(ErrorMessage = "Email Address is required"), EmailAddress, StringLength(50), DisplayName("Email Address")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Last Name is required"), DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required"), DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Department is required"), DisplayName("First Name")]
        public int DepartmentId { get; set; }

        public string CreatedUser { get; set; }

    }
    public class vmUser2
    {
        [Key, Required(ErrorMessage = "Email Address is required"), EmailAddress, StringLength(50), DisplayName("Email Address")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Last Name is required"), DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First Name is required"), DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Department is required"), DisplayName("First Name")]
        public int DepartmentId { get; set; }
    }
    public class vmLogin
    {
        [Key, Required(ErrorMessage = "User Name is required"), DisplayName("User Name(Army Number)")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password), DisplayName("Password")]
        public string Password { get; set; }
    }
    public class vmLoginTransaction
    {
        public string UserId { get; set; }
        public string TransactionType { get; set; }
    }
    public class vmLoginUserProfile
    {
        [Key]
        public string UserId { get; set; }
        public string DepartmentId { get; set; }
        public int BandId { get; set; }
        public string BandName { get; set; }
    }
    public partial class UserIPDetails
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public string City { get; set; }
        public string AccuracyRadius { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimeZone { get; set; }
        public string Continent { get; set; }
        public string StateName { get; set; }
        public string Country { get; set; }
        public string RegisteredCountry { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class vmPasswordChange
    {

        [Required(ErrorMessage = "Former Password is required"), DataType(DataType.Password), DisplayName("Former Password")]
        public string FormerPassword { get; set; }

        [Required(ErrorMessage = "New Password is required"), DataType(DataType.Password), DisplayName("New Password")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Password mismatch"), Required(ErrorMessage = "Confirm Password is required"), DataType(DataType.Password), DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
    public class PasswordChange
    {
        public string UserId { get; set; }
        public string FormerPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class vmEditUser
    {
        [Key]
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int DepartmentId { get; set; }
    }
    public class UserNameDetails
    {
        [Key]
        public string UserFullName { get; set; }
    }
}
