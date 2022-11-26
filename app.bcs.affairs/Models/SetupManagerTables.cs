using System.ComponentModel.DataAnnotations;

namespace app.bcs.affairs.Models
{
    public class Countries
    {
        [Key]
        public string Id { get; set; }
        public string CountryDescription { get; set; }
    }

    public class Departments
    {
        [Key]
        public int Id { get; set; }
        public string DepartmentDescription { get; set; }
    }
    public class BethelTypes
    {
        [Key]
        public int Id { get; set; }
        public string BethelTypeDescription { get; set; }
    }
}
