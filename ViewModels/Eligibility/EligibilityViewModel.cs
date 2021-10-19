using System.ComponentModel.DataAnnotations;

namespace nidirect_app_frontend.ViewModels.NewFolder
{
    public class EligibilityViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Enter if you currently reside in Northern Ireland")]
        public bool? IsResidentNi { get; set; }
    }
}