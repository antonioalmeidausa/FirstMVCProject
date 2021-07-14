using System;
using FirstMVCProject.Models.FrontModels;

namespace FirstMVCProject.Models.ApplicationModels
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int CompanyId { get; set; }

        public Company CompanyName { get; set; }
    }
}
