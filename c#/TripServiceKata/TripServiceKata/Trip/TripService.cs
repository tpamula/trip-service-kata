using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private TripDAO _tripDAO;

        public TripService(TripDAO tripDAO)
        {
            _tripDAO = tripDAO;
        }

        public List<Trip> GetTripsByUser(User.User loggedUser, User.User user)
        {
            if (loggedUser == null) throw new UserNotLoggedInException();

            return loggedUser.IsFriend(user)
                ? _tripDAO.FindTripsBy(user)
                : NoTrips();
        }

        private static List<Trip> NoTrips()
        {
            return new List<Trip>();
        }
    }
}