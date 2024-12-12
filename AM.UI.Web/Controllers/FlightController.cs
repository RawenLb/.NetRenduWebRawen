using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AM.UI.Web.Controllers
{
    public class FlightController : Controller
    {

        IFlightService sf;
        IServicePlanes sp;

        public FlightController(IFlightService sf, IServicePlanes sp)
        {
            this.sf = sf;
            this.sp = sp;
        }

        // GET: FlightController
        public ActionResult Index(DateTime? dateDepart)
        {
            if (dateDepart == null)
                return View(sf.GetAll().ToList());
            else
                return View(sf.GetMany(f => f.FlightDate.Date.Equals(dateDepart.Value.Date)).ToList());
        }

        public ActionResult Sort()

        {

            return View("Index", sf.SortFlights());

        }



        // GET: FlightController/Details/5
        public ActionResult Details(int id)
        {
            // Retrieve the flight using the service method
            var flight = sf.GetById(id);

            // Check if the flight was found
            if (flight == null)
            {
                return NotFound(); // Return 404 if the flight is not found
            }

            // Return the View with the flight details
            return View(flight);
        }


        // GET: FlightController/Create
        public ActionResult Create()
        {
            ViewBag.planeList = new SelectList(sp.GetAll(), "PlaneId", "Capacity");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Flight f, IFormFile PilotImage)
        {
            try
            {
                if (PilotImage != null && PilotImage.ContentType.StartsWith("image"))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", PilotImage.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        PilotImage.CopyTo(stream);
                    }

                    f.Pilot = PilotImage.FileName;
                }

                sf.Add(f);
                sf.Commit();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: FlightController/Edit/5
        public ActionResult Edit(int id)
        {
            var flight = sf.GetById(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewBag.planeList = new SelectList(sp.GetAll(), "PlaneId", "Capacity");
            return View(flight);
        }

        // POST: FlightController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Flight flight, IFormFile PilotImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Handle Pilot Image if uploaded
                    if (PilotImage != null && PilotImage.ContentType.StartsWith("image"))
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", PilotImage.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            PilotImage.CopyTo(stream);
                        }

                        flight.Pilot = PilotImage.FileName;  // Update pilot image name
                    }

                    // If the flight is found and updated, commit the changes
                    sf.Update(flight);
                    sf.Commit();

                    return RedirectToAction(nameof(Index));
                }

                // If model state is invalid, reload plane list
                ViewBag.planeList = new SelectList(sp.GetAll(), "PlaneId", "Capacity");
                return View(flight);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error updating flight: " + ex.Message);
                ViewBag.planeList = new SelectList(sp.GetAll(), "PlaneId", "Capacity");
                return View(flight);
            }
        }

        // GET: FlightController/PlaneDetails/5
        public ActionResult PlaneDetails(int id)
        {
            // Récupérer le détail de l'avion basé sur l'ID
            var plane = sp.GetById(id);
            if (plane == null)
            {
                return NotFound(); // Retourner 404 si l'avion n'est pas trouvé
            }
            return View(plane); // Afficher la vue avec les détails de l'avion
        }



        // GET: FlightController/Delete/5
        public ActionResult Delete(int id)
        {
            var flight = sf.GetById(id);  // Retrieve the flight by id
            if (flight == null)
            {
                return NotFound();  // Return 404 if flight is not found
            }
            return View(flight);  // Display confirmation view
        }

        // POST: FlightController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var flight = sf.GetById(id);  // Retrieve the flight by id
                if (flight == null)
                {
                    return NotFound();  // Return 404 if flight not found
                }

                // Delete the flight
                sf.Delete(flight);
                sf.Commit();  // Apply changes to the database

                return RedirectToAction(nameof(Index));  // Redirect to index after deletion
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error deleting flight: " + ex.Message);
                return View();  // Return to Delete confirmation view if error occurs
            }
        }
    }


    }
