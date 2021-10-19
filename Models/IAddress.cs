using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace nidirect_app_frontend.Models
{
    public interface IAddress
    {
        public string SearchAddress { get; set; }
        public string SearchPostCode { get; set; }

        [DisplayName("Address line 1")]
        [Required(ErrorMessage = "Enter address line 1")]
        [StringLength(35, ErrorMessage = "{0} must be a string with a maximum length of {1}")]
        public string Address1 { get; set; }

        [DisplayName("Address line 2")]
        [StringLength(35, ErrorMessage = "{0} must be a string with a maximum length of {1}")]
        public string Address2 { get; set; }

        [DisplayName("Address line 3")]
        [StringLength(35, ErrorMessage = "{0} must be a string with a maximum length of {1}")]
        public string Address3 { get; set; }

        [DisplayName("Town or city")]
        [StringLength(35, ErrorMessage = "{0} must be a string with a maximum length of {1}")]
        [Required(ErrorMessage = "Enter town or city")]
        public string TownCity { get; set; }

        [DisplayName("Post code")]
        [Required(ErrorMessage = "Enter post code")]
        [StringLength(8, ErrorMessage = "{0} must be a string with a maximum length of {1}")]
        public string PostCode { get; set; }

        [DisplayName("Country")]
        [StringLength(35, ErrorMessage = "{0} must be a string with a maximum length of {1}")]
        public string Country { get; set; }
    }
}