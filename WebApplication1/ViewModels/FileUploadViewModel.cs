using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class FileUploadViewModel : ClinicalRecord
    {
        public byte[] FileData { get; set; }
    }
}
