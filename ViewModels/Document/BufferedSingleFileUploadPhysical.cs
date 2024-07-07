using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace nidirect_app_frontend.ViewModels.Document;

public class BufferedSingleFileUploadPhysical
{
    [Display(Name = "File")]
    public IFormFile FormFile { get; set; }
}