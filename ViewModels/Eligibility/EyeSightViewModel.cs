using System.ComponentModel.DataAnnotations;

namespace nidirect_app_frontend.ViewModels.Eligibility;

public sealed class EyeSightViewModel : BaseViewModel
{
    [Required(ErrorMessage = "Enter if you meet the legal eyesight standard for driving")]
    public bool? IsLegalEyeSight { get; set; }
}