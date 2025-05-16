using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibraryEntities
{
    public class BusinessModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusinessID { get; set; }

        // General Information
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Parent Name is required")]
        public string? ParentName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Pin Code is required")]
        [RegularExpression(@"^\d{4,6}$", ErrorMessage = "Pin Code must be 4-6 digits")]
        public string? PinCode { get; set; }
       
        public string? TimeZone { get; set; } = "IST - Indian Standard Time - GMT +5:30";

        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Invalid Phone Number")]
        public string? PhoneNumber { get; set; }

        // Business Information
        [Required(ErrorMessage = "Business Name is required")]
        public string? BusinessName { get; set; }

        public string? LogoPath { get; set; }
        public required byte[] LogoData { get; set; }

        [Required(ErrorMessage = "Business Type is required")]
        public string? BusinessType { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Area is required")]
        public string? Area { get; set; }

        public string? Location { get; set; }
        public bool HasWiFi { get; set; }
        public bool HasOutdoorSeating { get; set; }
        public bool IsActive { get; set; } = true;
    }
}