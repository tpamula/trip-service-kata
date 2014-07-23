using System;
using TripServiceKata.Exception;
using TripServiceKata.Trip;
using Xunit;
using KataTrip = TripServiceKata.Trip.Trip;
using KataUser = TripServiceKata.User.User;

namespace TripServiceKata.Tests
{
    public class TripServiceTest
    {
        [Fact]
        public void returns_no_trips_when_user_has_no_friends()
        {
            var user = new KataUser();

            var userWithoutFriends = new KataUser();
            userWithoutFriends.AddTrip(new KataTrip());
            userWithoutFriends.AddTrip(new KataTrip());

            var someFriend = new KataUser();
            someFriend.AddTrip(new KataTrip());
            someFriend.AddTrip(new KataTrip());
            user.AddFriend(someFriend);

            var tripService = new TestableTripService(user);

            Assert.Equal(0, tripService.GetTripsByUser(userWithoutFriends).Count);
        }

        [Fact]
        public void should_throw_exception_when_user_not_logged_in()
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