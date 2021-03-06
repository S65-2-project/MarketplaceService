﻿using System;
using System.ComponentModel.DataAnnotations;
using MarketplaceService.Domain;

namespace MarketplaceService.Models
{
    public class CreateDAppOfferModel
    {
        [Required] public string Title { get; set; }
        [Required] public User Provider { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int OfferLengthInMonths { get; set; }
        [Required] public int LiskPerMonth { get; set; }
        [Required] public int DelegatesNeededForOffer { get; set; }
        [Required] public string Region { get; set; }
        [Required] public DateTime DateStart { get; set; }
        [Required] public DateTime DateEnd { get; set; }
        
    }
}