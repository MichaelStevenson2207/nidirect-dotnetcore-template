using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace nidirect_app_frontend.ViewModels.Document
{
    public class BufferedSingleFileUploadPhysical
    {
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}