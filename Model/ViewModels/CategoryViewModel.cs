using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public virtual List<ProductViewModel>? Products { get; set; }
    }
}