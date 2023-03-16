using Microsoft.AspNetCore.SignalR;

namespace testAPI.Models
{
    public class MapPoint
    {
        

        public int Id { get; set; }
        public string Rut { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Branch { get; set; }
        public string PhoneNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        
          

    }
}

//La sintraxis get; set; es para abreviar una propiedad. get para lectura y set para definir.
