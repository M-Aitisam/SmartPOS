﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibraryEntities
{
    public class BusinessModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessID { get; set; }

        // Navigation properties for the steps
        public GeneralInformation GeneralInformation { get; set; } = new();
        public BusinessDetails BusinessDetails { get; set; } = new();
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public List<Discount> DefaultDiscounts { get; set; } = new List<Discount>();
    }
}