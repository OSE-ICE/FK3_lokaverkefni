using System.ComponentModel.DataAnnotations;

namespace FK3_lokaverkefni.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public required int UserId { get; set; }
        public string? Name { get; set; }
        public decimal Distance { get; set; }
        [Url]
        public string? Link { get; set; }
        public string? Region { get; set; }
        public string? Description { get; set; }
        [Url]
        public string? VideoLink { get; set; }

       
    }
}
