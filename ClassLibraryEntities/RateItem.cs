namespace ClassLibraryEntities
{
    public class RateItem
    {

        public string? Name { get; set; }
        public string? Description {  get; set; }
        public decimal BasePrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
        public bool IsActive { get; set; } = false;
        public string? ImageUrl { get; set; } = string.Empty;
        public string? Category { get; set; }
        public int Id { get; set; }
        public string? ActiveDays { get; set; } = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday";

    }
}
