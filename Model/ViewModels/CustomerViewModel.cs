using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        public virtual List<OrderViewModel>? Orders { get; set; }
    }
}