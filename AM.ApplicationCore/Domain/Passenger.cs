using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Passenger
    {
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        [Display(Name = "Date of Birth")]
        public DateTime BirthDate { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailAddress { get; set; }
       public fullname fullname { get; set; }
        [Key]
        [StringLength(7)]
        public string PassportNumber { get; set; }
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone Number must be exactly 8 digits.")]
        public int TelNumber { get; set; }
        public virtual IList<Flight> Flights { get; set; }
        public virtual IList<ReservationTicket> ReservationTickets { get; set; }

        //public bool CheckProfile(string nom, string prenom)
        //{
        //    return LastName == nom && FirstName.Equals(prenom);
        //}
        //public bool CheckProfile(string nom, string prenom,string email)
        //{
        //    return LastName == nom && FirstName.Equals(prenom) && EmailAddress == email;
        //}

        public bool CheckProfile(string nom, string prenom, string email=null)
        {
           if ( email == null )
                return fullname.LastName == nom && fullname.FirstName.Equals(prenom);
            return fullname.LastName == nom && fullname.FirstName.Equals(prenom) && EmailAddress == email;
        }

        public virtual void PassengerType()
            { Console.WriteLine("I am a passenger"); }

    }
}