using System;
using System.ComponentModel.DataAnnotations;
using MarketplaceService.Domain;
using Microsoft.VisualBasic;

namespace MarketplaceService.Models
{
    public class CreateDAppOfferModel
    {
        [Required] public Guid Guid { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int OfferLengthInMonths { get; set; }
        [Required] public int LiskPerMonth { get; set; } 
        [Required] public int DelegatesNeededForOffer { get; set; }
        [Required] public string Region { get; set; }
        [Required] public DateFormat DateStart { get; set; }
        [Required] public DateFormat DateEnd { get; set; }
        
    }
}