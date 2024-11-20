using FK3_lokaverkefni.Models;
using FK3_lokaverkefni.Models.DTO;


namespace FK3_lokaverkefni.Data.Interfaces
{
    public interface IRepository
    {
        Task<Models.Route> AddRouteAsync(Models.Route route);
        Task<List<Models.Route>> GetAllRoutesAsync();
        Task<Models.Route> GetRouteByIdAsync(int id);
        Task<Models.Route> UpdateRouteAsync(int id, Models.Route route);
        Task<bool> DeleteRouteAsync(int id);
        Task<List<UserDTO>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task<User> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
        Task<List<Event>> GetAllEventsAsync();
        Task AddEventAsync(Event ev);
        Task<Event> UpdateEventAsync(int id, Event ev);
        Task<bool> DeleteEventAsync(int id);
    }
}
