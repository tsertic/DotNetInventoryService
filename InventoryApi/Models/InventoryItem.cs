namespace InventoryApi.Models
{
    public class InventoryItem
    {

        public int Id { get; set; }
        public string? name { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }

    }
}
