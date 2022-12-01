using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Entities
{
    public class Countries
    {
        [Key]
        public string Id { get; set; }
        public string CountryDescription { get; set; }
        public string RegionId { get; set; }
    }
    public class Regions
    {
        [Key]
        public string Id { get; set; }
        public string RegionDescription { get; set; }
    }
    public class Departments
    {
        [Key]
        public int Id { get; set; }
        public string DepartmentDescription { get; set; }
    }

}
