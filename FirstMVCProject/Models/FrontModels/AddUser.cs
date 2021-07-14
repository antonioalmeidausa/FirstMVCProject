using System;
using System.ComponentModel.DataAnnotations;
using FirstMVCProject.Models.ApplicationModels;

namespace FirstMVCProject.Models.FrontModels
{
    public class AddUser
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum Caracthers is 2.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum Caracthers is 2")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Company ID is required.")]
        public int? CompanyId { get; set; }

        public Company CompanyName { get; set; }
    }
}
