using Microsoft.AspNetCore.Mvc;

namespace nidirect_app_frontend.ViewModels.Document;

public sealed class DocumentViewModel : BaseViewModel
{
    [BindProperty]
    public BufferedSingleFileUploadPhysical FileUpload { get; set; }
}