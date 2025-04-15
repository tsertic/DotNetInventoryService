using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi.Models

{
    public class InventoryItem
    {

        public int Id { get; set; }


        public string? Name { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

    }
}
