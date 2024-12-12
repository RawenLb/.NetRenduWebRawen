using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Flight
    {
        public DateTime Departure { get; set; }
        public string Destination { get; set; }
        public string Pilot { get; set; }

        public DateTime EffectiveArrival { get; set; }
        public int EstimatedDuration { get; set; }
        public DateTime FlightDate { get; set; }
        public int FlightId { get; set; }
        public virtual Plane Plane { get; set; }

        [ForeignKey("Plane")]
        public int PlaneFK { get; set; }
        public virtual IList<Passenger> Passengers { get; set; }

        public DateTime DepartureDate { get; internal set; }

        public override string ToString()
        {
            return "Date: " + FlightDate + " Destination: " + Destination + " DURATION: " + EstimatedDuration;
        }
    }
}