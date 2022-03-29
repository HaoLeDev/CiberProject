using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        public virtual List<Order>? Orders { get; set; }
    }
}