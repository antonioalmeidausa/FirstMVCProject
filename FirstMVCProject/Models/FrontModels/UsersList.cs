using System;
using System.Collections.Generic;

namespace FirstMVCProject.Models.FrontModels
{
    public class UsersList
    {
        public List<UserViewModel> AllUsers { get; set; }
    }

    public class UserViewModel
    {
        public int  Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }


}
