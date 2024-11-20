namespace FK3_lokaverkefni.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? SSID { get; set; }

        public List<Route> RouteId { get; set; } = new List<Route>();
    }
}
