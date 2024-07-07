using Microsoft.AspNetCore.Mvc;

namespace nidirect_app_frontend.ViewModels.Document;

public class DocumentViewModel : BaseViewModel
{
    [BindProperty]
    public BufferedSingleFileUploadPhysical FileUpload { get; set; }
}