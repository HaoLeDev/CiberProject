using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.ViewModels
{

    public class OrderViewModel
    {
       
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public decimal Amount { get; set; }

        public DateTime OrderDate { get; set; }

        public CustomerViewModel? Customer { get; set; }

        public ProductViewModel? Product { get; set; }

    }
}