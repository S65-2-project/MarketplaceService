using System;
using System.ComponentModel.DataAnnotations;
using MarketplaceService.Domain;

namespace MarketplaceService.Models
{
    public class CreateDelegateOfferModel
    {
        [Required] public Guid Guid { get; set; }
        [Required] public User Provider { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int LiskPerMonth { get; set; }
        [Required] public string Region { get; set; }
        [Required] public int AvailableForInMonths { get; set; }

    }
}