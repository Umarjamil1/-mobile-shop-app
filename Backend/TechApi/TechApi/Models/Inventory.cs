namespace TechApi.Models
{
    public class Inventory
    {
        public int ProductID { get; set; }
        public string Productname { get; set; } = string.Empty;
        public int Avaliblestock { get; set; }
        public int Reorderstock { get; set; }
    }
}
