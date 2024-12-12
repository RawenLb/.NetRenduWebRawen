using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class ReservationTicket
    {
        public DateTime ReservationDate { get; set; }
        public double Price { get; set; }
        public virtual Passenger Passenger { get; set; }
        [ForeignKey("Passenger")]
        public string PassengerFK { get; set; }
        public virtual Ticket Ticket { get; set; }
        [ForeignKey("Ticket")]
        public int TicketFK { get; set; }
    }
}
