using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebClient.Models;
using WebClient.Repositories;

namespace WebClient.Controllers
{
    public class PlacesController : BaseApiController
    {
        [HttpGet]
        [ActionName("all")]
        public HttpResponseMessage GetAllPlaces()
        {
            var response = this.PerformOperation(() =>
            {
                //var userId = UsersRepository.LoginUser(sessionKey);
                var placesModels = PlacesRepository.GetAll();
                return placesModels;
            });
            return response;
        }

        [HttpGet]
        [ActionName("nearby")]
        public HttpResponseMessage GetNearbyPlaces(string sessionKey)
        {
            var response = this.PerformOperation(() =>
            {
                var userId = UsersRepository.LoginUser(sessionKey);
                var placesModels = PlacesRepository.GetNearby(userId);
                return placesModels;
            });
            return response;
        }

        [HttpPost]
        [ActionName("add")]
        public HttpResponseMessage AddPlace(string sessionKey, [FromBody]PlaceModel place)
        {
            var response = this.PerformOperation(() =>
            {
                var userId = UsersRepository.LoginUser(sessionKey);
                PlacesRepository.AddPlace(place);
            });
            return response;
        }
    }
}