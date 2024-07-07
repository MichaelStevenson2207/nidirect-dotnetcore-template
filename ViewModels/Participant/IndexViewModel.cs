using System.ComponentModel.DataAnnotations;

namespace nidirect_app_frontend.ViewModels.Participant;

public sealed class IndexViewModel : BaseViewModel
{
    [Required(ErrorMessage = "Enter your forename")]
    public string Forename { get; set; }

    [Required(ErrorMessage = "Enter your surname")]
    public string Surname { get; set; }
}