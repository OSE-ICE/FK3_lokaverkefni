namespace FK3_lokaverkefni.Models.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<Route> RouteId { get; set; } = new List<Route>();
    }
}
