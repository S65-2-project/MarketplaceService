using System;
using System.ComponentModel.DataAnnotations;

namespace marketplaceservice.Models
{
    public class CreateProductModel
    {
        [Required] public Guid Guid { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
    }
}