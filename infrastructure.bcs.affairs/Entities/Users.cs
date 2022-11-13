using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Entities
{
    public partial class Users
    {
        [Key]
        public string EmailAddress { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsFirstLogin { get; set; }
        public byte[] PasswordHashed { get; set; }
        public byte[] SecurityStamp { get; set; }
        public bool? UserStatus { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? TransactionDate { get; set; }
        
    }
}
