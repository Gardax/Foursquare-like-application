using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebClient.Models
{
    [DataContract]
    public class PlaceModel
    {
        [DataMember(Name = "id")]
        public long Id { get; set; }

        [DataMember(Name = "latitude")]
        public int Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public int Longitude { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}