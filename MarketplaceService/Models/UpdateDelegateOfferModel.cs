using System.ComponentModel.DataAnnotations;
using MarketplaceService.Domain;
using Microsoft.VisualBasic;

namespace MarketplaceService.Models
{
    public class UpdateDelegateOfferModel
    {
        [Required] public string Title { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int LiskPerMonth { get; set; } 
        [Required] public int AvailableForInMonths { get; set; }
        [Required] public string Region { get; set; }

    }
}