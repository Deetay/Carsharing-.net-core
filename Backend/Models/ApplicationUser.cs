using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Backend.Models
{
    [Table("User")]
    public class ApplicationUser
    {
        public ApplicationUser()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        

        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public double Rating { get; set; }

        public long RateAmount { get; set; } = 0;

        private string Gender { get; set; }

        //usun virtual jesli nie bedzie dzialac lazy loading
        public virtual byte[] Photo { get; set; }

        public string Description { get; set; }

        public string Car { get; set; }

    }
}
