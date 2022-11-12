using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Entities
{
    public class Bethels
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

    public class BethelLists
    {
        [Key]
        public string Id { get; set; }
        public int BethelTypeId { get; set; }
        public string BethelTypeDescription { get; set; }
        public string BethelDescription { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string CountryId { get; set; }
        public string CountryDescription { get; set; }
        public string StatesProvince { get; set; }
        public string LocalCouncil { get; set; }
        public string ZipPostCode { get; set; }
        public string BcsZone { get; set; }
        public string Town { get; set; }
        public string UserId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
