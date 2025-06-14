﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryEntities
{
    public class BusinessDetails
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Business Name is required")]
        public string BusinessName { get; set; } = string.Empty;

        public string LogoPath { get; set; } = string.Empty;
        public byte[] LogoData { get; set; } = Array.Empty<byte>();

        [Required(ErrorMessage = "Business Type is required")]
        public string BusinessType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Area is required")]
        public string Area { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
        public bool HasWiFi { get; set; }
        public bool HasOutdoorSeating { get; set; }

        [Required]
        public string ReceiptHeader { get; set; } = @"
      SUPERMARKET
  Point of Sale System
-----------------------------
Aitisam Ahmed
Contact: 0347-1536817
Islamabad, G-9
Shop No. 258, Lorem Ipsum Plaza
City Index: 02025

-----------------------------";

        [Required]
        public string ReceiptFooter { get; set; } = @"
-----------------------------
THANKS FOR Comming!
Visit us again soon!
";

        public string TaxNumber { get; set; } = "";
     
        public bool IncludeBarcode { get; set; } = true;
        public bool IncludeQRCode { get; set; } = true;
        public string? FeedbackUrl { get; set; }
        public string? ManagerName { get; set; } = "";
        public decimal DefaultTaxRate { get; set; } = 15;

        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string? Password { get; set; }



        public bool EnableDiscounts { get; set; } = true;
        public List<Discount> DefaultDiscounts { get; set; } = new List<Discount>();

    }
}
