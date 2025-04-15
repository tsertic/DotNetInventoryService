using System.ComponentModel.DataAnnotations;

namespace InventoryApi.Dtos
{
    public class CreateInventoryItemDto
    {
        [Required(ErrorMessage ="Arcticle name is required.")]
        [StringLength(100,ErrorMessage ="Name can be maximum 100 charachters")]
        public string? Name { get; set; }

        [Range(0,int.MaxValue,ErrorMessage ="Only positive numbers")]
        public int Quantity { get; set; }

        [Range(0.00 , (double)decimal.MaxValue,ErrorMessage ="Price cannot be negative")]
        public decimal Price { get; set; }
    }
}
