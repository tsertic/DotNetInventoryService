using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi.Models

{
    public class InventoryItem
    {

        public int Id { get; set; }


        [Required(ErrorMessage ="Article Name is required.")]
        [StringLength(100,ErrorMessage ="Max length 100 characthers")]
        public string? name { get; set; }

        [Range(0,int.MaxValue,ErrorMessage ="Quanitity cannot be negative.")]
        public int Quantity { get; set; }

        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }

    }
}
