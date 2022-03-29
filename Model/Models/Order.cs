using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public decimal Amount { get; set; }

        public DateTime OrderDate { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }

    }
}