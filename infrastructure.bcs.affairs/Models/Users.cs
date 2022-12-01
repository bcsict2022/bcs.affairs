using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace infrastructure.bcs.affairs.Models
{
    public class vmBand
    {
        [Required(ErrorMessage = "Permission Group is required")]
        public string GroupName { get; set; }
    }
    public class vmUserBand
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Permission Group is required")]
        public int BandId { get; set; }
    }
    public class vmUser
    {
        public string CreatedUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
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
    public class vmDashboard
    {
        public long totalLogin { get; set; }
        public long totalAggregate { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal cbBankBalance { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal cbCashBalance { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,##0.00}")]
        public decimal cbPaymentBankBalance { get; set; }
        public decimal cbPaymentCashBalance { get; set; }
        public decimal cbReceiptBankBalance { get; set; }
        public decimal cbReceiptCashBalance { get; set; }
    }
    public class vmLoginUserProfile
    {
        [Key]
        public string UserId { get; set; }
        public string DepartmentId { get; set; }
        public int BandId { get; set; }
        public string BandName { get; set; }
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
    public class vmPasswordChange
    {

        [Required(ErrorMessage = "userId is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Former Password is required"), DataType(DataType.Password), DisplayName("Former Password")]
        public string FormerPassword { get; set; }

        [Required(ErrorMessage = "New Password is required"), DataType(DataType.Password), DisplayName("New Password")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Password mismatch"), Required(ErrorMessage = "Confirm Password is required"), DataType(DataType.Password), DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
    public class UserNameDetails
    {
        [Key]
        public string UserFullName { get; set; }
    }
    public class vmEditUser
    {
        [Key]
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int DepartmentId { get; set; }
    }
}
