using nidirect_app_frontend.ViewModels;

namespace nidirect_app_frontend.Models;

public class ErrorViewModel : BaseViewModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}