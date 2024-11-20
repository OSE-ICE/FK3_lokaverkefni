using FK3_lokaverkefni.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using FK3_lokaverkefni.Models;
using Route = FK3_lokaverkefni.Models.Route;
using FK3_lokaverkefni.Models.DTO;


namespace FK3_lokaverkefni.Data
{
    public class EmtbicelandRepository : IRepository
    {
        private readonly EmtbicelandDBContext? _dbContext;

        public EmtbicelandRepository()
        {
            _dbContext = new EmtbicelandDBContext();
        }

        public async Task<List<Route>> GetAllRoutesAsync()
        {
            List<Route> routes;

            using (var db = _dbContext)
            {
                routes = await db.Routes.ToListAsync();
            }

            List<Route> listToReturn = new List<Route>();

            foreach (Route rout in routes)
            {
                Route routeToAdd = new Route
                {
                    RouteId = rout.RouteId,
                    UserId = rout.UserId,
                    Name = rout.Name,
                    Distance = rout.Distance,
                    Link = rout.Link,
                    Region = rout.Region,
                    Description = rout.Description,
                    VideoLink = rout.VideoLink,
                };
                listToReturn.Add(routeToAdd);
            }
            return listToReturn;
        }

        public async Task<Route> GetRouteByIdAsync(int id)
        {
            Route route;
            using (var db = _dbContext)
            {
                route = await db.Routes.FirstOrDefaultAsync(r => r.RouteId == id);
            }
            if (route == null)
            {
                return null;
            }
            Route routeToReturn = new Route
            {
                RouteId = route.RouteId,
                UserId = route.UserId,
                Name = route.Name,
                Distance = route.Distance,
                Link = route.Link,
                Region = route.Region,
                Description = route.Description,
                VideoLink = route.VideoLink,
            };
            return routeToReturn;
        }

        public async Task<Route> AddRouteAsync(Route route)
        {
            using (var db = _dbContext)
            {
                await db.Routes.AddAsync(route);
                await db.SaveChangesAsync();
            }
            return route;
        }

        public async Task<Route> UpdateRouteAsync(int id, Models.Route route)
        {
            using (var db = _dbContext)
            {
                Route rout = await db.Routes.FirstOrDefaultAsync(r => r.RouteId == id);
                if (rout == null)
                {
                    return null;
                }
                rout.UserId = route.UserId;
                rout.Name = route.Name;
                rout.Distance = route.Distance;
                rout.Link = route.Link;
                rout.Region = route.Region;
                rout.Description = route.Description;
                rout.VideoLink = route.VideoLink;
                await db.SaveChangesAsync();
                return rout;
            }
        }

        public async Task<bool> DeleteRouteAsync(int id)
        {
           var route =await _dbContext.Routes.FindAsync(id);
            if (route == null)
            {
                return false;
            }
            _dbContext.Routes.Remove(route);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync()
        {
            List<User> users;
            using (var db = _dbContext)
            {
                users = await db.Users.Include(u => u.RouteId).ToListAsync();
            }
            List<UserDTO> listToReturn = new List<UserDTO>();
            foreach (User user in users)
            {
                List<Route> routes = user.RouteId.Select(r => new Route
                {
                    RouteId = r.RouteId,
                    UserId = r.UserId,
                    Name = r.Name,
                    Distance = r.Distance,
                    Link = r.Link,
                    Region = r.Region,
                    Description = r.Description,
                    VideoLink = r.VideoLink
                }).ToList();

                UserDTO userToAdd = new UserDTO
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RouteId = routes
                };
                listToReturn.Add(userToAdd);
            }
            return listToReturn;
        }


       public async Task<UserDTO> GetUserByIdAsync(int id)
{
    User user;
    using (var db = _dbContext)
    {
        user = await db.Users.Include(u => u.RouteId).FirstOrDefaultAsync(u => u.UserId == id);
    }
    if (user == null)
    {
        return null;
    }
    List<Route> routes = user.RouteId.Select(r => new Route
    {
        RouteId = r.RouteId,
        UserId = r.UserId,
        Name = r.Name,
        Distance = r.Distance,
        Link = r.Link,
        Region = r.Region,
        Description = r.Description,
        VideoLink = r.VideoLink
    }).ToList();

    UserDTO userToReturn = new UserDTO
    {
        UserId = user.UserId,
        FirstName = user.FirstName,
        LastName = user.LastName,
        RouteId = routes
    };
    return userToReturn;
}


        public async Task AddUserAsync(User user)
        {
            using (var db = _dbContext)
            {
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task<User> UpdateUserAsync(int id, User user)
        {
            using (var db = _dbContext)
            {
                User us = await db.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (us == null)
                {
                    return null;
                }
                us.FirstName = user.FirstName;
                us.LastName = user.LastName;
                await db.SaveChangesAsync();
                return us;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            List<Event> events;
            using (var db = _dbContext)
            {
                events = await db.Events.ToListAsync();
            }
            List<Event> listToReturn = new List<Event>();
            foreach (Event ev in events)
            {
                Event eventToAdd = new Event
                {
                    EventId = ev.EventId,
                    Name = ev.Name,
                    Date = ev.Date,
                    Text = ev.Text,
                    Link = ev.Link
                };
                listToReturn.Add(eventToAdd);
            }
            return listToReturn;
        }

        public async Task AddEventAsync(Event ev)
        {
            using (var db = _dbContext)
            {
                await db.Events.AddAsync(ev);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Event> UpdateEventAsync(int id, Event ev)
        {
            using (var db = _dbContext)
            {
                Event eve = await db.Events.FirstOrDefaultAsync(e => e.EventId == id);
                if (eve == null)
                {
                    return null;
                }
                eve.Name = ev.Name;
                eve.Date = ev.Date;
                eve.Text = ev.Text;
                eve.Link = ev.Link;
                await db.SaveChangesAsync();
                return eve;
            }
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var ev = await _dbContext.Events.FindAsync(id);
            if (ev == null)
            {
                return false;
            }
            _dbContext.Events.Remove(ev);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            Event ev;
            using (var db = _dbContext)
            {
                ev = await db.Events.FirstOrDefaultAsync(e => e.EventId == id);
            }
            if (ev == null)
            {
                return null;
            }
            Event eventToReturn = new Event
            {
                EventId = ev.EventId,
                Name = ev.Name,
                Date = ev.Date,
                Text = ev.Text,
                Link = ev.Link
            };
            return eventToReturn;
      

    }

}
}

