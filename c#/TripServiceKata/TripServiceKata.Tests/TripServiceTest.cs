using FakeItEasy;
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
        private KataUser _loggedInUser = new KataUser();
        private TripService _tripService;

        public TripServiceTest()
        {
            _tripService = new TripService(FakedTripDAO);
        }

        private TripDAO FakedTripDAO
        {
            get
            {
                var tripDAO = A.Fake<TripDAO>();
                A.CallTo(() => tripDAO.FindTripsBy(A<KataUser>.Ignored))
                    .ReturnsLazily((KataUser friend) => friend.Trips());

                return tripDAO;
            }
        }

        [Fact]
        public void returns_no_trips_when_user_has_no_friends()
        {
            var userWithoutFriends = new KataUser();
            userWithoutFriends.AddTrip(new KataTrip());
            userWithoutFriends.AddTrip(new KataTrip());

            var someFriend = new KataUser();
            someFriend.AddTrip(new KataTrip());
            someFriend.AddTrip(new KataTrip());
            _loggedInUser.AddFriend(someFriend);

            Assert.Equal(0, _tripService.GetTripsByUser(_loggedInUser, userWithoutFriends).Count);
        }

        [Fact]
        public void should_return_correct_amount_of_friends_trips()
        {
            var friend = new KataUser();
            friend.AddTrip(new KataTrip());
            friend.AddTrip(new KataTrip());
            friend.AddTrip(new KataTrip());

            _loggedInUser.AddFriend(friend);

            Assert.Equal(3, _tripService.GetTripsByUser(_loggedInUser, friend).Count);
        }

        [Fact]
        public void should_throw_exception_when_user_not_logged_in()
        {
            KataUser guest = null;
            var friend = new KataUser();

            Assert.Throws<UserNotLoggedInException>(() =>
            {
                _tripService.GetTripsByUser(guest, friend);
            });
        }
    }
}