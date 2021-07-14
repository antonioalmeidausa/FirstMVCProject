using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FirstMVCProject.DatabaseConnections;
using FirstMVCProject.Models.FrontModels;
using FirstMVCProject.Models.ApplicationModels;
using System.Linq;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstMVCProject.Controllers
{
     
    public class UserController : Controller
    {

        private readonly AppContext _appContext;

        public UserController(AppContext appContext)
        {
            _appContext = appContext;
        }

        DatabaseConnection DatabaseAction = new DatabaseConnection();

        // GET: /<controller>/
        public IActionResult Index()
        {

            List<User> UsersFromDatabase = _appContext.Users.ToList();

            List<UserViewModel> AllUsers = new List<UserViewModel>();

            foreach (var user in UsersFromDatabase)
            {
                UserViewModel userView = new UserViewModel();

                userView.Id = user.Id;
                userView.FirstName = user.FirstName;
                userView.LastName = user.LastName;
                userView.DateOfBirth = user.DateOfBirth;

                AllUsers.Add(userView);
            }

            UsersList usersList = new UsersList();

            usersList.AllUsers = AllUsers;

            return View(usersList);

        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(AddUser user)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User(); ;

                newUser.FirstName = user.FirstName;
                newUser.LastName = user.LastName;
                newUser.DateOfBirth = (System.DateTime)user.DateOfBirth;
                newUser.CompanyId = (int)user.CompanyId;

                _appContext.Users.Add(newUser);
                _appContext.SaveChanges();
                ViewBag.Message = "User Added Succesfully!";
            }

            else
            {
                ViewBag.Message = "Error to add a new user. Please try again!";
            }

            return View();
        }

        [HttpGet]
        public IActionResult UpdateUser(int Id)
        {
            User firstUser = DatabaseAction.GetAllUsers().FirstOrDefault(x => x.Id == Id);

            UpdateUserFrontModel userToUpdate = new UpdateUserFrontModel();

            userToUpdate.FirstName = firstUser.FirstName;
            userToUpdate.LastName = firstUser.LastName;
            userToUpdate.DateOfBirth = firstUser.DateOfBirth;


            return View(userToUpdate);
        }

        [HttpPost]
        public IActionResult UpdateUser(UpdateUserFrontModel user)
        {
            DatabaseAction.GetUserById(user.Id);
            User userUpdated = new User();

            userUpdated.Id = user.Id;

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                userUpdated.FirstName = user.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(user.LastName))
            {
                userUpdated.LastName = user.LastName;
            }

            if (user.DateOfBirth >= SqlDateTime.MinValue.Value && user.DateOfBirth <= SqlDateTime.MaxValue.Value)
            {
                userUpdated.DateOfBirth = user.DateOfBirth;
            }



                _appContext.Entry(userUpdated).State = EntityState.Modified;

                _appContext.SaveChanges();
                ViewBag.Message = "User Updated Succesfully!";

            return View(user);
        }

        public IActionResult DeleteUser(int Id)
        {
                var user = _appContext.Users.FirstOrDefault(u => u.Id == Id);
                _appContext.Entry(user).State = EntityState.Deleted;
                _appContext.SaveChanges();
                TempData["Message"] = "User Deleted Succesfully!";

            return RedirectToAction("Index");
        }

        public IActionResult ListUser()
        {
            return Json(_appContext.Users.Include(a => a.CompanyName).ToList());
        }

    }
}
