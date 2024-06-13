using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class ClinicalRecord
    {
        public int ClinicalRecordID { get; set; }
        public string? FileName { get; set; }
        public string? Disorder { get; set; }
        public DateTime? ClinicalContactCommenced { get; set; }
        public DateTime? ClinicalContactTerminated { get; set; }
        public DateTime? Date { get; set; }
        public string? RelevantInformation { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? TutorEmailAddress { get; set; }
        public string? Clinician { get; set; }
        public string? AssessmentFindings { get; set; }

        public string? FilePath { get; set; }
        public string? Referral { get; set; }
        public string? History { get; set; }
        public int ClinicID { get; set; }
        public int PatientID { get; set; }

        public int ? DoctorID { get; set; } 

        // Relationships
        public Clinic Clinic { get; set; }
        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; } 

        public string ? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public string? ClinicName { get; set; }

    }
}
