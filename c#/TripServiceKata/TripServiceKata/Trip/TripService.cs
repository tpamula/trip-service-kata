using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            List<Trip> tripList = new List<Trip>();
            User.User loggedUser = GetLoggedUser();

            if (loggedUser == null) throw new UserNotLoggedInException();

            return loggedUser.IsFriend(user)
                ? TripDAO.FindTripsByUser(user)
                : new List<Trip>();
        }

        protected virtual User.User GetLoggedUser()
        {
            User.User loggedUser = UserSession.GetInstance().GetLoggedUser();
            return loggedUser;
        }
    }
}