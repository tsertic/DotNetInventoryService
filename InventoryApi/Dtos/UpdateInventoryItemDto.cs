using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Dtos
{
    public class UpdateInventoryItemDto
    {
        [Required(ErrorMessage ="Article name is required.")]
        [StringLength(100,ErrorMessage ="Max length is 100 charachters.")]
        public string? Name { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="Quantity cannot be negative")]
        public int Quantity { get; set; }

        [Range(0.00,(double)decimal.MaxValue,ErrorMessage ="Price cannot be negative")]
        public decimal Price { get; set; }
    }
}
