using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class PassengerRide
    {
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int RideId { get; set; }

        public Ride Ride { get; set; }
    }
}
