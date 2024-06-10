namespace WebApplication1.Models
{
    public class Doctor
    {public int DoctorID { get; set; }
        public string? DoctorType { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorInstitution { get; set; }
        public string? DoctorContactNo { get; set; }
        public string? DoctorEmailAddress { get; set; }
        public List<ClinicalRecord>? ClinicalRecords { get; set; }
    }
}
