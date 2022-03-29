using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.ViewModels
{
    public class ProductViewModel
    {
       
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("CategoryId")]
        public virtual CategoryViewModel Category { get; set; }

        public virtual List<OrderViewModel> Orders { get; set; }
    }
}