using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Data;
using Hospital.Models;
using Hospital.Models.ViewModels;

namespace Hospital.Controllers
{
    public class AccountController : Controller
    {
        private readonly HospitalContext _context;

        public AccountController(HospitalContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var admin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Username == model.TC);

                if (admin != null && admin.Password == model.Password)
                {
                    HttpContext.Session.SetString("UserType", "Admin");
                    HttpContext.Session.SetString("UserId", admin.AdminId.ToString());
                    return RedirectToAction("AdminDashboard", "Home");
                }

                var doctor = await _context.Doctors
                    .Include(d => d.Branch)
                    .FirstOrDefaultAsync(d => d.TC == model.TC);

                if (doctor != null && doctor.Password == model.Password)
                {
                    HttpContext.Session.SetString("UserType", "Doctor");
                    HttpContext.Session.SetString("UserId", doctor.DoctorId.ToString());
                    return RedirectToAction("DoctorDashboard", "Home");
                }

                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.TC == model.TC);

                if (patient != null && patient.Password == model.Password)
                {
                    HttpContext.Session.SetString("UserType", "Patient");
                    HttpContext.Session.SetString("UserId", patient.PatientId.ToString());
                    return RedirectToAction("PatientDashboard", "Home");
                }

                // Hatalý giriþ durumunda hata mesajý
                ModelState.AddModelError("", "Geçersiz TC veya þifre. Lütfen tekrar deneyin.");
            }

            return View(model);
        }


        public async Task<IActionResult> Register()
        {
            ViewBag.Branches = await _context.Branches.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TC kontrolü: Hasta ve Doktor tablosunda ayný TC varsa ekleme yapma
                var tcExists = await _context.Doctors.AnyAsync(d => d.TC == model.TC) ||
                               await _context.Patients.AnyAsync(p => p.TC == model.TC);

                if (tcExists)
                {
                    ModelState.AddModelError("", "Bu TC kimlik numarasý ile kayýtlý bir kullanýcý zaten mevcut.");
                    ViewBag.Branches = await _context.Branches.ToListAsync();
                    return View(model);
                }

                if (model.UserType == "Doctor")
                {
                    var doctor = new Doctor
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        TC = model.TC,
                        Password = model.Password,
                        BranchId = model.BranchId ?? 0,
                        UserName = model.TC,
                        UserType = "Doctor"
                    };

                    _context.Doctors.Add(doctor);
                }
                else
                {
                    var patient = new Patient
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        TC = model.TC,
                        Password = model.Password,
                        Age = model.Age ?? 0,
                        UserType = "Patient"
                    };

                    _context.Patients.Add(patient);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }

            ViewBag.Branches = await _context.Branches.ToListAsync();
            return View(model);
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
} 