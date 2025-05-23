﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibraryEntities
{
    public class Product
    {
        public string? ProductUrduName { get; set; }
        public string? ProductTitle { get; set; }
        public string? ProductCode { get; set; }
        public decimal ProductPrice { get; set; }
        public bool IsActive { get; set; } = true;
        public string? ImageUrl { get; set; }
        public bool IsSelected { get; set; }
        public string ActiveDays { get; set; } = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday";
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }


        public int CategoryID { get; set; }
        public int? SubCategoryID { get; set; }
        public int? NestedSubCategoryID { get; set; }

        [ForeignKey("CategoryID")]

        public BusinessCategory? Category { get; set; }

        [ForeignKey("SubCategoryID")]
        public BusinessSubItem? SubCategory { get; set; }

    }
}
