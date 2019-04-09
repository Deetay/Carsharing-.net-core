using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    [Table("Ride")]
    public class Ride
    {
        public Ride()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RideId { get; set; }

        public int OwnerId { get; set; }

        public string Description{ get; set;}

        public Place From { get; set; }

        public Place To { get; set; }

        public int NumOfSeats { get; set; }

        public int BookedSeats { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }

        //public virtual ICollection<int> Passengers { get; set; }

    }

   
}
