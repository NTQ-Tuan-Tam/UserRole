﻿using Microsoft.AspNetCore.Mvc;
using RoleUser.Models;
using System.Collections.Generic;
using System.Linq;

namespace RoleUser.Controllers
{

    public class UsersController : Controller
    {

        private readonly EmployeeContext _context;

        public UsersController(EmployeeContext context)
        {
            _context = context;
        }
        [Route("ListUser")]
        public IActionResult ListUser()
        {
            List<User> users = _context.Users.ToList();


            return View(users);

        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Create(User user) //chuwa toi uu
        {
            if (ModelState.IsValid) //tim hiue
            {
                var users = new User()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password,
                    Email = user.Email,
                    GroupName = user.GroupName
                    
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "success Done";
                return RedirectToAction("ListUser");
            }
            else
            {
                TempData["errorMessage"] = "Model data not valid"; 
                return View();  
            }
        }
        

        public IActionResult Edit(int id)
        {
            User user = _context.Users.Where(p => p.Id == id).FirstOrDefault();
            return PartialView("_EditUserPatialView", user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("ListUser");
        }
        //[HttpGet]
        //public IActionResult Edit(int Id)
        //{
            
        //        var users = _context.Users.SingleOrDefault(x => x.Id == Id);
        //        if (users != null)
        //        {
        //            var userView = new User()
        //            {
        //                Id = users.Id,
        //                Name = users.Name,
        //                Password = users.Password,
        //                Email = users.Email,
        //                GroupName = users.GroupName
        //            };
        //            return View(userView);
        //        }
        //        else
        //        {
        //            TempData["ErrorMessage"] = $"User detail not validable : {Id}";
        //            return RedirectToAction("ListUser");
        //        }
            
           

        //}
        //[HttpPost]
        //public IActionResult Edit(User user)
        //{
            
        //        if (ModelState.IsValid)
        //        {
        //            var userView = new User()
        //            {
        //                Id = user.Id,
        //                Name = user.Name,
        //                Email = user.Email,
        //                Password = user.Password,
        //                GroupName = user.GroupName
        //            };
        //            _context.Users.Update(userView);
        //            _context.SaveChanges();
        //            TempData["SuccessMessage"] = "success Done";
        //            return RedirectToAction("ListUser");

        //        }
        //        else
        //        {
        //            TempData["errorMessage"] = "Model data not valid";
        //            return View();
        //        }
           
        //}
        [HttpGet]
        public IActionResult Details(int Id)
        {

            var users = _context.Users.Single(x => x.Id == Id);
            if (users != null)
            {
                var userView = new User()
                {
                    Id = users.Id,
                    Name = users.Name,
                    Password = users.Password,
                    Email = users.Email,
                    GroupName = users.GroupName
                };
                return View(userView);
            }
            else
            {
                TempData["ErrorMessage"] = $"User detail not validable : {Id}";
                return RedirectToAction("ListUser");
            }



        }
        //[HttpPost]
        //public IActionResult Details(int delete)
        //{

        //    var users = _context.Users.Single(x => x.Id == delete);
        //    if (users != null)
        //    {
        //        var userView = new User()
        //        {
        //            Id = users.Id,
        //            Name = users.Name,
        //            Password = users.Password,
        //            Email = users.Email
        //        };
        //        return View(userView);
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = $"User detail not validable : {delete}";
        //        return RedirectToAction("ListUser");
        //    }



        //}

        [HttpGet]
        public IActionResult Delete(int Id)
        {

            var users = _context.Users.SingleOrDefault(x => x.Id == Id);
            if (users != null)
            {
                var userView = new User()
                {
                    Id = users.Id,
                    Name = users.Name,
                    Password = users.Password,
                    Email = users.Email,
                    GroupName = users.GroupName
                };
                return View(userView);
            }
            else
            {
                TempData["ErrorMessage"] = $"User detail not validable : {Id}";
                return RedirectToAction("ListUser");
            }



        }
        [HttpPost]
        public IActionResult Delete(User model)
        {

            var users = _context.Users.SingleOrDefault(x => x.Id == model.Id);
            if (users != null)
            {
                _context.Users.Remove(users);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Delete done";
                return RedirectToAction("ListUser");
            }
            else
            {
                TempData["ErrorMessage"] = $"User detail not validable : {model}";
                return RedirectToAction("ListUser");
            }



        }
    }

}
