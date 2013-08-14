using ClassesData;
using ClassesModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient.Models;

namespace WebClient.Repositories
{
    public class PlacesRepository : BaseRepository
    {
        private const string ValidPlaceNameChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM_1234567890 -";
        private const int MinPlaceNameChars = 4;
        private const int MaxPlaceNameChars = 30;

        public static IEnumerable<PlaceModel> GetAll()
        {
            var context = new FoursquareContext();
            List<PlaceModel> models = new List<PlaceModel>();

            foreach (var place in context.Places)
            {
                PlaceModel currentModel = new PlaceModel();
                currentModel.Name = place.Name;
                currentModel.Latitude = place.Latitude;
                currentModel.Longitude = place.Longitude;
                models.Add(currentModel);
            }

            return models;
        }

        public static IEnumerable<PlaceModel> GetNearby(long userId)
        {
            var context = new FoursquareContext();
            var user = context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ServerErrorException("User does not exist", "ERR_GEN_SVR");
            }

            List<PlaceModel> models = new List<PlaceModel>();
            foreach (var place in context.Places)
            {
                if (IsInProximity(place.Latitude, place.Longitude, user.Latitude, user.Longitude))
                {

                    PlaceModel currentModel = new PlaceModel();
                    currentModel.Name = place.Name;
                    currentModel.Latitude = place.Latitude;
                    currentModel.Longitude = place.Longitude;
                    models.Add(currentModel);
                }
            }

            return models;
        }

        public static void AddPlace(PlaceModel place)
        {
            ValidatePlaceModel(place);

            var context = new FoursquareContext();
            Place newPlace = new Place();
            newPlace.Name = place.Name;
            newPlace.Latitude = place.Latitude;
            newPlace.Longitude = place.Longitude;
            context.Places.Add(newPlace);
            context.SaveChanges();
        }

        public static void CheckIn(int userId, int placeId)
        {
            var context = new FoursquareContext();
            var user = context.Users.FirstOrDefault(u => u.Id == userId);
            var place = context.Places.FirstOrDefault(p => p.Id == placeId);
            if (user == null || place == null)
            {
                throw new ServerErrorException("User or place does not exist.", "INV_NICK_LEN");
            }

            user.Latitude = place.Latitude;
            user.Longitude = place.Longitude;
            context.SaveChanges();

            PubnubAPI pubnub = new PubnubAPI(
            "pub-c-9fc21b3b-c747-43f0-b756-766ebc32a9b1",               // PUBLISH_KEY
            "sub-c-6d28e41e-04b5-11e3-a3d6-02ee2ddab7fe",               // SUBSCRIBE_KEY
            "sec-c-YTVkZjU1NTMtMGE1MC00N2ZlLWE2MWEtNGExNjMwZTliODc5",   // SECRET_KEY
            true                                                        // SSL_ON?
        );
            string channel = place.Id + "" + place.Name;
            List<object> publishResult = pubnub.Publish(channel, "Hello Pubnub!");
        }

        private static void ValidatePlaceModel(PlaceModel place)
        {
            if (place.Name == null || place.Name.Length < MinPlaceNameChars || place.Name.Length > MaxPlaceNameChars)
            {
                throw new ServerErrorException("Invalid Place Name", "INV_NICK_LEN");
            }
        }

        private static bool IsInProximity(int placeLatitude, int placeLongitude, int userLatitude, int userLongitude)
        {
            var distance = Math.Sqrt((placeLatitude - userLatitude) * (placeLatitude - userLatitude) +
                (placeLongitude - userLongitude) * (placeLongitude - userLongitude));

            if (distance <= proximityDistance)
            {
                return true;
            }

            return false;
        }
    }
}