using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{ //deuxieme methode
    //public enum PlaneType
    //{
    //    Boing,
    //    Airbus
    //}
    public class Plane
    {
        #region exemple
        //exemple
        //private int capacite;

        //public int GetCapacite() {
        //    return capacite;
        //}
        //public void SetCapacite(int capacite)
        //{
        //    this.capacite = capacite;
        //}
        #endregion

        //prop + deux Tab
        // nullable capacite 
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive integer.")]
        public int? Capacity { get; set; }
        public string AirLineLogo { get; set; }
        public DateTime ManufactureDate { get; set; }
        public int PlaneId { get; set; }
        public PlaneType PlaneType { get; set; }
        ////secure version
        //public int MyProperty { get; private set; }
        ////full version
        //private int myVar;

        //public int MyProperty2
        //{
        //    get { return myVar; }
        //    set { myVar = value; }
        //}


        //proprite de navigation

        public virtual ICollection<Flight> Flights { get; set; }

       //public Plane()
       // {

       // }
       // public Plane(int Capacite, DateTime ManufactureDate, int PlaneId)
       // {
       //     this.Capacite = Capacite;
       //     this.ManufactureDate = ManufactureDate;
       //     this.PlaneId = PlaneId;
       // }


        public override String ToString()
        {
            return "Capacite: "+Capacity + " \nManufactureDate:" + ManufactureDate+ " \nPlaneId:" + PlaneId;
        }

    }

}
