using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
