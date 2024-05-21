namespace WebApplication1.Models
{
    public class Clinic
    {
        public int ClinicID { get; set; }
        public string ClinicName { get; set; }
        public string ClinicLocation { get; set; }
        public bool IsActive { get; set; }

        // Navigation property
        public List<ClinicalRecord>? ClinicalRecords { get; set; }
    }
}
