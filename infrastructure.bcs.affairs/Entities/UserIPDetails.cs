using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Entities
{
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
        
}
