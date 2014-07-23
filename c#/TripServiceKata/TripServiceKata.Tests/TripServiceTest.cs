using TripServiceKata.Exception;
using TripServiceKata.Trip;
using Xunit;
using KataUser = TripServiceKata.User.User;

namespace TripServiceKata.Tests
{
    public class TripServiceTest
    {
        [Fact]
        public void should_throw_error_when_user_not_logged_in()
        {
            KataUser guest = null;
            var tripService = new TestableTripService(guest);

            Assert.Throws<UserNotLoggedInException>(() =>
            {
                tripService.GetTripsByUser(guest);
            });
        }

        public class TestableTripService : TripService
        {
            private KataUser _loggedUser;

            public TestableTripService(KataUser loggedUser)
            {
                _loggedUser = loggedUser;
            }

            protected override KataUser GetLoggedUser()
            {
                return _loggedUser;
            }
        }
    }
}