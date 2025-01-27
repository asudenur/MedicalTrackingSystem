using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using Hospital.Models;
using Hospital.Models.ViewModels;

namespace Hospital.Controllers
{
    public class HomeController : Controller
    {
        private readonly HospitalContext _context;

        public HomeController(HospitalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserType") == "Doctor")
            {
                return RedirectToAction(nameof(DoctorDashboard));
            }
            else if (HttpContext.Session.GetString("UserType") == "Patient")
            {
                return RedirectToAction(nameof(PatientDashboard));
            }
            else if (HttpContext.Session.GetString("UserType") == "Admin")
            {
                return RedirectToAction(nameof(AdminDashboard));
            }

            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> DoctorDashboard()
        {
            if (HttpContext.Session.GetString("UserType") != "Doctor")
            {
                return RedirectToAction("Login", "Account");
            }

            var doctorId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var doctor = await _context.Doctors
                .Include(d => d.Branch)
                .FirstOrDefaultAsync(d => d.DoctorId == doctorId);

            if (doctor == null)
            {
                return NotFound();
            }

            var viewModel = new DoctorDashboardViewModel
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                TC = doctor.TC,
                BranchName = doctor.Branch?.BranchName ?? "Unknown",
                UserName = doctor.UserName
            };

            return View(viewModel);
        }

        public async Task<IActionResult> PatientDashboard()
        {
            if (HttpContext.Session.GetString("UserType") != "Patient")
            {
                return RedirectToAction("Login", "Account");
            }

            var patientId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == patientId);

            if (patient == null)
            {
                return NotFound();
            }

            var viewModel = new PatientDashboardViewModel
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                TC = patient.TC,
                Age = patient.Age,
                Disease = patient.Disease,
                MedicalRecord = patient.MedicalRecord,
                Medicine = patient.Medicine,
            };

            return View(viewModel);
        }

        public async Task<IActionResult> FindDoctor()
        {
            if (HttpContext.Session.GetString("UserType") != "Patient")
            {
                return RedirectToAction("Login", "Account");
            }

            var doctors = await _context.Doctors
                .Include(d => d.Branch)
                .ToListAsync();

            ViewBag.Branches = await _context.Branches.ToListAsync();
            return View(doctors);
        }

        public async Task<IActionResult> MyPatients()
        {
            if (HttpContext.Session.GetString("UserType") != "Doctor")
            {
                return RedirectToAction("Login", "Account");
            }

            var doctorId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var patients = await _context.Patients
                .Where(p => p.DoctorId == doctorId)
                .ToListAsync();

            return View(patients);
        }

        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public async Task<IActionResult> ManageDoctors()
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            var doctors = await _context.Doctors
                .Include(d => d.Branch)
                .ToListAsync();

            ViewBag.Branches = await _context.Branches.ToListAsync();
            return View(doctors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDoctor(string FirstName, string LastName, string TC, string Password, int BranchId)
        {
            // Burada doktoru veritabanýna ekleyin
            var doctor = new Doctor
            {
                FirstName = FirstName,
                LastName = LastName,
                TC = TC,
                Password = Password,
                BranchId = BranchId
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            return RedirectToAction("ManageDoctors");  // Geriye bir sayfa yönlendirebilirsiniz
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDoctor([FromBody] DeleteModel model)
        {
            try
            {
                if (HttpContext.Session.GetString("UserType") != "Admin")
                {
                    return Json(new { success = false, message = "Unauthorized access" });
                }

                var doctor = await _context.Doctors.FindAsync(model.Id);
                if (doctor != null)
                {
                    // Remove doctor's association with patients
                    var patients = await _context.Patients.Where(p => p.DoctorId == model.Id).ToListAsync();
                    foreach (var patient in patients)
                    {
                        patient.DoctorId = null;
                    }

                    _context.Doctors.Remove(doctor);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Doctor deleted successfully" });
                }
                return Json(new { success = false, message = "Doctor not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the doctor", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePatient([FromBody] DeleteModel model)
        {
            try
            {
                if (HttpContext.Session.GetString("UserType") != "Admin")
                {
                    return Json(new { success = false, message = "Unauthorized access" });
                }

                var patient = await _context.Patients.FindAsync(model.Id);
                if (patient != null)
                {
                    _context.Patients.Remove(patient);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Patient deleted successfully" });
                }
                return Json(new { success = false, message = "Patient not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the patient", error = ex.Message });
            }
        }

        public async Task<IActionResult> ManagePatients()
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            var patients = await _context.Patients.ToListAsync();
            return View(patients);
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(int doctorId)
        {
            if (HttpContext.Session.GetString("UserType") != "Patient")
            {
                return Json(new { success = false });
            }

            var patientId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
            var patient = await _context.Patients.FindAsync(patientId);

            if (patient == null)
            {
                return Json(new { success = false });
            }

            patient.DoctorId = doctorId;
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientModel model)
        {
            if (HttpContext.Session.GetString("UserType") != "Doctor")
            {
                return Json(new { success = false, message = "Unauthorized access" });
            }

            try
            {
                var doctorId = int.Parse(HttpContext.Session.GetString("UserId") ?? "0");
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.PatientId == model.PatientId && p.DoctorId == doctorId);

                if (patient == null)
                {
                    return Json(new { success = false, message = "Patient not found or not assigned to you" });
                }

                patient.Disease = model.Disease;
                patient.MedicalRecord = model.MedicalRecord;
                patient.Medicine = model.Medicine;

                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}