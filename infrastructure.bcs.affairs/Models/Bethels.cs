using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Models
{
    public partial class vmBethel
    {
        [Required(ErrorMessage = "Bethel Type is required")]
        public int BethelTypeId { get; set; }        

        [Required(ErrorMessage = "Bethel Description is required")]
        public string BethelDescription { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string CountryId { get; set; }

        [Required(ErrorMessage = "State/Province is required")]
        public string StatesProvince { get; set; }

        [Required(ErrorMessage = "Local Council is required")]
        public string LocalCouncil { get; set; }

        public string ZipPostCode { get; set; }

        [Required(ErrorMessage = "BcsZone is required")]
        public string BcsZone { get; set; }

        [Required(ErrorMessage = "Town is required")]
        public string Town { get; set; }

        [Required(ErrorMessage = "User Name is required"), StringLength(50)]
        public string UserId { get; set; }
    }
    public partial class vmBethelEdit
    {
        [Required(ErrorMessage = "Bethel Type is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Bethel Type is required"), DisplayName("Bethel Type")]
        public int BethelTypeId { get; set; }

        [Required(ErrorMessage = "Bethel Description is required"), DisplayName("Bethel")]
        public string BethelDescription { get; set; }

        [Required(ErrorMessage = "Address is required"), DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Address - Optional")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "Country is required"), DisplayName("Country")]
        public string CountryId { get; set; }

        [Required(ErrorMessage = "State/Province is required"), DisplayName("State/Province")]
        public string StatesProvince { get; set; }

        [Required(ErrorMessage = "Local Council is required"), DisplayName("Local Council")]
        public string LocalCouncil { get; set; }

        [DisplayName("Zip or Post Code")]
        public string ZipPostCode { get; set; }

        [Required(ErrorMessage = "BcsZone is required"), DisplayName("Bcs Zone")]
        public string BcsZone { get; set; }

        [Required(ErrorMessage = "Town is required"), DisplayName("Town")]
        public string Town { get; set; }

        [Required(ErrorMessage = "User Name is required"), StringLength(50)]
        public string UserId { get; set; }
    }
    public class vmBethelLists
    {
        [Key]
        public string Id { get; set; }
        public int BethelTypeId { get; set; }
        public string BethelDescription { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string CountryId { get; set; }
        public string StatesProvince { get; set; }
        public string LocalCouncil { get; set; }
        public string ZipPostCode { get; set; }
        public string BcsZone { get; set; }
        public string Town { get; set; }
        public string UserId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
    public class BethelDistinctLists
    {
        [Key]
        public string Description { get; set; }
    }
    public partial class populations
    {
        [Key]
        public string name { get; set; }

    }
}
