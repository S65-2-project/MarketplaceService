using System;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceService.Domain
{
    public class DelegateOffer
    {
        [Required] public Guid Id { get; set; }
        [Required] public User Provider { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Region { get; set; }
        [Required] public int LiskPerMonth { get; set; }
        [Required] public int AvailableForInMonths { get; set; }
    }
}