using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
 

namespace WebApplication1.Controllers
{

    [Authorize]
    public class ClinicalRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _azureBlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=clinicalrecords;AccountKey=4SlfjGJb9Ass6rDhXpYCAadWAr9sl3GrdoRP4kc1cvcZBrIHyDddFgDGo4VHBTERiSReBMpA6RDa+AStBPuw+g==;EndpointSuffix=core.windows.net";
        private readonly string _blobContainerName = "medrecords";
        private readonly UserManager<IdentityUser> _userManager;
        public ClinicalRecordsController(IConfiguration configuration, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
        }

        // GET: ClinicalRecords
        //public ActionResult Index(HttpPostedFileBase postedFile)
        //{ 
        //if (postedFile != null)
        //            {
        //        string path = Server.MapPath("~/Uploads/");
            
        //    }
        //return View() randiom comment 
        
        //}



        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClinicalRecord.Include(c => c.Clinic).Include(c => c.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClinicalRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinicalRecord = await _context.ClinicalRecord
                .Include(c => c.Clinic)
                .Include(c => c.Patient)
                .FirstOrDefaultAsync(m => m.ClinicalRecordID == id);
            if (clinicalRecord == null)
            {
                return NotFound();
            }

            return View(clinicalRecord);
        }
        [HttpGet]
        public IActionResult AutoSearchDoctor(string searchTerm)
        {
            // Perform search based on the searchTerm and retrieve filtered data
            var filteredData = GetFilteredDoctorData(searchTerm);

            // Transform filtered data into SelectListItem format
            var selectListItems = filteredData.Select(item => new SelectListItem
            {
                Value = item.DoctorID.ToString(),
                Text = item.DoctorName
            });

            return Json(selectListItems);
        }
        [HttpGet]
        public IActionResult AutoSearchPatient(string searchTerm)
        {
            // Perform search based on the searchTerm and retrieve filtered data
            var filteredData = GetFilteredPatientData(searchTerm);

            // Transform filtered data into SelectListItem format
            var selectListItems = filteredData.Select(item => new SelectListItem
            {
                Value = item.PatientID.ToString(),
                Text = item.PatientName
            });

            return Json(selectListItems);
        }
        [HttpGet]
        public IActionResult AutoSearchClinic(string searchTerm)
        {
            // Perform search based on the searchTerm and retrieve filtered data
            var filteredData = GetFilteredClinicData(searchTerm);

            // Transform filtered data into SelectListItem format
            var selectListItems = filteredData.Select(item => new SelectListItem
            {
                Value = item.ClinicID.ToString(),
                Text = item.ClinicName
            });

            return Json(selectListItems);
        }
        public List<Doctor> GetFilteredDoctorData(string searchTerm)
        {
            List<Doctor> doctors = _context.Doctor.ToList();


            // Filter the list based on the searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower(); // Convert searchTerm to lowercase for case-insensitive comparison
                doctors = doctors.Where(d => d.DoctorName.ToLower().Contains(searchTerm)).ToList();
            }

            return doctors;
        }
        public List<Patient> GetFilteredPatientData(string searchTerm)
        {
            List<Patient> patients = _context.Patient.ToList();


            // Filter the list based on the searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower(); // Convert searchTerm to lowercase for case-insensitive comparison
                patients = patients.Where(d => d.PatientName.ToLower().Contains(searchTerm)).ToList();
            }

            return patients;
        }
        public List<Clinic> GetFilteredClinicData(string searchTerm)
        {
            List<Clinic> clinics = _context.Clinic.ToList();


            // Filter the list based on the searchTerm
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower(); // Convert searchTerm to lowercase for case-insensitive comparison
                clinics = clinics.Where(d => d.ClinicName.ToLower().Contains(searchTerm)).ToList();
            }

            return clinics;
        }
        // GET: ClinicalRecords/Create /// need to make the role names work
        [Authorize]
        public async Task<IActionResult>  Create()
        {
            ViewData["ClinicName"] = new SelectList(_context.Set<Clinic>(), "ClinicName", "ClinicName");
            ViewData["PatientName"] = new SelectList(_context.Patient, "PatientName", "PatientName");

            ViewData["DoctorName"] = new SelectList(_context.Doctor, "DoctorName", "DoctorName");
            var user = await _userManager.GetUserAsync(User);
            ViewData["LoggedInUser"] = user.UserName;
            return View();
        }

        // POST: ClinicalRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Create( IFormCollection form)
        {   
            var clinicalRecord = ExtractFormValuesIntoModel(form);
            
            if (ModelState.IsValid)
            {
                BlobClient blobClient = await UploadFileToBlobStorage(form);
                clinicalRecord.FilePath = blobClient.Uri.ToString();
                _context.Add(clinicalRecord);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            List<Doctor> doctors = _context.Doctor.ToList();
            var doctorId = doctors.Find(d => d.DoctorName.ToLowerInvariant().Equals(clinicalRecord.DoctorName.ToLowerInvariant)).DoctorID;
            ViewData["ClinicName"] = new SelectList(_context.Set<Clinic>(), "ClinicName", "ClinicName", clinicalRecord.ClinicID);
            ViewData["PatientName"] = new SelectList(_context.Patient, "PatientName", "PatientName", clinicalRecord.PatientName);
            ViewData["DoctorName"] = new SelectList(_context.Doctor, "DoctorName", "DoctorName", clinicalRecord.Doctor.DoctorName);
            var user = await _userManager.GetUserAsync(User);
            ViewData["LoggedInUser"] = user.UserName;
            return View(clinicalRecord);
        }

        private async Task<BlobClient> UploadFileToBlobStorage(IFormCollection form)
        {
            var file = form.Files["Upload"];
            
            BlobServiceClient blobServiceClient = new BlobServiceClient(_azureBlobStorageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_blobContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
            await blobClient.UploadAsync(file.OpenReadStream(), true);
            return blobClient;
        }

        private ClinicalRecord ExtractFormValuesIntoModel(IFormCollection form)
        {
            var clinicalRecord = new ClinicalRecord()
            {
                FileName = form["FileName"],
                Disorder = form["Disorder"],
                ClinicalContactCommenced = DateTime.Parse(form["ClinicalContactCommenced"]),
                ClinicalContactTerminated = DateTime.Parse(form["ClinicalContactTerminated"]),
                Date = DateTime.Today,
                RelevantInformation = form["RelevantInformation"],
                CreatedBy = form["CreatedBy"],
                UpdatedBy = form["UpdatedBy"],
                UpdatedDate = DateTime.Today,
                TutorEmailAddress = form["TutorEmailAddress"],
                Clinician = form["Clinician"],
                AssessmentFindings = form["AssessmentFindings"],
                Referral = form["Referral"],
                History = form["History"],
                ClinicID = int.Parse(form["ClinicID"]),
                PatientID = int.Parse(form["PatientID"]),
                DoctorName = form["DoctorName"],
                FilePath= form["FilePath"]
            };

            return clinicalRecord;
        }

        private async Task UploadToBlobStorageAsync(IFormFile file)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_azureBlobStorageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_blobContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);
            await blobClient.UploadAsync(file.OpenReadStream(), true);
        }

        // GET: ClinicalRecords/Edit/5
      
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinicalRecord = await _context.ClinicalRecord.FindAsync(id);
            if (clinicalRecord == null)
            {
                return NotFound();
            }
            ViewData["ClinicID"] = new SelectList(_context.Set<Clinic>(), "ClinicID", "ClinicID", clinicalRecord.ClinicID);
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID", clinicalRecord.PatientID);
            ViewData["DoctorID"] = new SelectList(_context.Doctor, "DoctorID", "DoctorID", clinicalRecord.DoctorID);
            return View(clinicalRecord);
        }

        // POST: ClinicalRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClinicalRecordID,FileName,Disorder,ClinicalContactCommenced,ClinicalContactTerminated,Date,RelevantInformation,CreatedBy,UpdatedBy,UpdatedDate,TutorEmailAddress,Clinician,AssessmentFindings,Referral,History,ClinicID,PatientID,DoctorID")] ClinicalRecord clinicalRecord)
        {
            if (id != clinicalRecord.ClinicalRecordID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinicalRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicalRecordExists(clinicalRecord.ClinicalRecordID))
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
            ViewData["ClinicID"] = new SelectList(_context.Set<Clinic>(), "ClinicID", "ClinicID", clinicalRecord.ClinicID);
            ViewData["PatientID"] = new SelectList(_context.Patient, "PatientID", "PatientID", clinicalRecord.PatientID);
            ViewData["DoctorID"] = new SelectList(_context.Doctor, "DoctorID", "DoctorID", clinicalRecord.DoctorID);
            return View(clinicalRecord);
        }

        // GET: ClinicalRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clinicalRecord = await _context.ClinicalRecord
                .Include(c => c.Clinic)
                .Include(c => c.Patient)
                .FirstOrDefaultAsync(m => m.ClinicalRecordID == id);
            if (clinicalRecord == null)
            {
                return NotFound();
            }

            return View(clinicalRecord);
        }

        // POST: ClinicalRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clinicalRecord = await _context.ClinicalRecord.FindAsync(id);
            if (clinicalRecord != null)
            {
                _context.ClinicalRecord.Remove(clinicalRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinicalRecordExists(int id)
        {
            return _context.ClinicalRecord.Any(e => e.ClinicalRecordID == id);
        }

        //upload files need to append this code to the create functionality
        public async Task<IActionResult> UploadFile(ClinicalRecord cr, IFormFile file)
        {
            var filename =  DateTime.Now.ToString("yyyymmddhhmmss");
            filename =filename +"_"+ file.FileName ;
            var path = $"{_configuration.GetSection("FileManagement:SystemUploads").Value}";
            var filepath = Path.Combine(path, filename);
            //to save file to folder 
            var stream = new FileStream(filepath, FileMode.Create);
            await file.CopyToAsync(stream);
            var clinRec = new ClinicalRecord
            {
                UpdatedBy = cr.UpdatedBy,
                CreatedBy = cr.CreatedBy,
                FileName = filename,
                FilePath = filepath,
            };
            await _context.AddAsync(clinRec);

           return RedirectToAction("Index");
          
        }

        [HttpPost]
        public IActionResult Search(string searchCriteria, string searchTerm)
        {
            IQueryable<ClinicalRecord> record = _context.ClinicalRecord;
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View("Index", record.ToList());
            }
            switch (searchCriteria)
            {
                case "patientName":
                    record = record.Where(p => p.Patient.PatientName.Contains(searchTerm));
                    break;
                case "patientLastName":
                    record = record.Where(p => p.Patient.PatientLastName.Contains(searchTerm));
                    break;
                case "doctor":
                    record = record.Where(p => p.DoctorID.ToString().Contains(searchTerm));
                    break;
                case "fileName":
                    record = record.Where(p => p.FileName.Contains(searchTerm));
                    break;
                case "idNumber":
                    record = record.Where(p => p.Patient.IDNumber.Contains(searchTerm));
                    break;
            }

            return View("Index", record.ToList());
        }
    }
}
