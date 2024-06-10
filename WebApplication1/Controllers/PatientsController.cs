using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patient.ToListAsync());
        }
        [HttpPost]
        public IActionResult Search(string searchCriteria, string searchTerm)
        {
            IQueryable<Patient> patients = _context.Patient; 

            switch (searchCriteria)
            {
                case "patientName":
                    patients = patients.Where(p => p.PatientName.Contains(searchTerm));
                    break;
                case "patientLastName":
                    patients = patients.Where(p => p.PatientLastName.Contains(searchTerm));
                    break;
                case "disorder":
                    patients = patients.Where(p => p.PatientEmailAddress.Contains(searchTerm));
                    break;
                case "clinic":
                    patients = patients.Where(p => p.PatientContactNumber.Contains(searchTerm));
                    break;
                case "idNumber":
                    patients = patients.Where(p => p.IDNumber.Contains(searchTerm));
                    break;
            }

            return View("Index", patients.ToList());
        }
        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.PatientID == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpGet]
        public async Task<IActionResult> Create([Bind("PatientID,PatientName,PatientLastName,PatientDateOfBirth,IDNumber,PatientGender,PatientAddress,PostalCode,PatientContactNumber,PatientEmailAddress,PatientEmployerCellNo,PatientOccupation,CreatedBy,UpdatedBy,CreatedDate,SpouseName,SpouseContactNo,UpdatedDate,isActive,isPatientStudent,School,Grade,Teacher,SchoolContactNo,FatherDetails,FathersName,FatherOccupation,FatherAddress,FatherWorkPhone,FatherCellPhone,FatherEmail,MotherDetails,MotherName,MotherOccupation,MotherAddress,MotherCellphone,MotherWorkPhone,MotherEmail,OtherDoctorDetails,FamilyDoctorName,FamilyDoctorInstitution,FamilyDoctorContactNo,PsychologistName,PsychologistInstitution,PsychologistContactNo,SocialWorkerName,SocialWorkerInstitution,SocialWorkerContactNo,OccupationalTherapist,OccupationalTherapistName,OccupationalTherapistInstitution,OccupationalTherapistContactNo")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientID,PatientName,PatientLastName,PatientDateOfBirth,IDNumber,PatientGender,PatientAddress,PostalCode,PatientContactNumber,PatientEmailAddress,PatientEmployerCellNo,PatientOccupation,CreatedBy,UpdatedBy,CreatedDate,SpouseName,SpouseContactNo,UpdatedDate,isActive,isPatientStudent,School,Grade,Teacher,SchoolContactNo,FatherDetails,FathersName,FatherOccupation,FatherAddress,FatherWorkPhone,FatherCellPhone,FatherEmail,MotherDetails,MotherName,MotherOccupation,MotherAddress,MotherCellphone,MotherWorkPhone,MotherEmail,OtherDoctorDetails,FamilyDoctorName,FamilyDoctorInstitution,FamilyDoctorContactNo,PsychologistName,PsychologistInstitution,PsychologistContactNo,SocialWorkerName,SocialWorkerInstitution,SocialWorkerContactNo,OccupationalTherapist,OccupationalTherapistName,OccupationalTherapistInstitution,OccupationalTherapistContactNo")] Patient patient)
        {
            if (id != patient.PatientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .FirstOrDefaultAsync(m => m.PatientID == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient != null)
            {
                _context.Patient.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.PatientID == id);
        }
 
    }
}
