using AM.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Interfaces
{
    public  interface IFlightService:IService<Flight>
    {
        public IEnumerable<DateTime> GetFlightdates(string destination);
        public IEnumerable<Flight> SortFlights()

        {

            return GetAll().OrderByDescending(f => f.FlightDate);

        }
        public void ShowFlightDetails(Plane plane);
        public int ProgrammedFlightNumber(DateTime startDate);
        public double DurationAverage(string destination);
        public IEnumerable<Flight> OrderedDurationFlights();
        public IEnumerable<Traveller> SeniorTravellers(Flight flight);
        IEnumerable<Staff> GetStaffByFlightId(int volId);
        IEnumerable<Traveller> GetPassengersByPlaneAndDate(Plane plane, DateTime DateVol);


        public IEnumerable< IGrouping<string, Flight>> DestinationGroupedFlights();
        public void DestinationGroupedF();
        void GetFlights(string filterType, string filterValue);


    }
}
