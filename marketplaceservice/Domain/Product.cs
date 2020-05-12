using System;
using System.ComponentModel.DataAnnotations;

namespace marketplaceservice.Domain
{
    public class Product
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
    }
}