using System.ComponentModel.DataAnnotations;

namespace marketplaceservice.Models
{
    public class UpdateProductModel
    {
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
    }
}