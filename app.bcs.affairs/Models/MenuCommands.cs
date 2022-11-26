namespace app.bcs.affairs.Models
{
    public partial class MenuCommands
    {
        public int Id { get; set; }
        public int MenusId { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string Route { get; set; }
        public string Type { get; set; }
        public string Tags { get; set; }
        public string I18n { get; set; }
        public string Icon { get; set; }
        public bool Disabled { get; set; }
    }
}
