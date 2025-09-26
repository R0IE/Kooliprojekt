namespace KooliProjekt.Models
{
    public class Beer
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? BreweryType { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? WebsiteUrl { get; set; }
        // Optional image url (not provided by Open Brewery DB) - we will populate a placeholder when missing
        public string? ImageUrl { get; set; }
    }
}
