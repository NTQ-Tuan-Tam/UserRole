using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RoleUser.DTO;
using RoleUser.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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

            var Users = from u in _context.UserNames
                        join g in _context.Groups
                            on  u.GroupId equals g.Id
                            select new UserDto
                            { 
                                Id= u.Id,
                                Name = u.Name,
                                Email = u.Email,
                                Password= u.Password,
                               GroupName = g.GroupName,
                                
                            };
            var result = from g in _context.Groups
                         select g;

            ViewBag.Group = ToSelectList(result.ToList());
            return View(Users);

        }
       

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public IActionResult Create(UserName user) //chuwa toi uu
        {
            if (ModelState.IsValid) //tim hiue
            {
                var users = new UserName()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Password = user.Password,
                    Email = user.Email,
                    
                    
                };
                _context.UserNames.Add(user);
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
           UserName user = _context.UserNames.Where(p => p.Id == id).FirstOrDefault();

            
            
            return PartialView("_EditUserPatialView", user);

        }
        [NonAction]
        public SelectList ToSelectList(List<Group> group)
        {
            
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var i in group )
            {
                list.Add(new SelectListItem()
                {
                    Text = i.GroupName.ToString(),
                    Value = i.GroupName.ToString()
                });
               
            }

            return new SelectList(list, "Value", "Text") ;
        }

        [HttpPost]
        public IActionResult Edit(UserName user)
        {
            _context.UserNames.Update(user);
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

            var users = _context.UserNames.Single(x => x.Id == Id);
            if (users != null)
            {
                var userView = new UserName()
                {
                    Id = users.Id,
                    Name = users.Name,
                    Password = users.Password,
                    Email = users.Email,
                  
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

            var users = _context.UserNames.SingleOrDefault(x => x.Id == Id);
            if (users != null)
            {
                var userView = new UserName()
                {
                    Id = users.Id,
                    Name = users.Name,
                    Password = users.Password,
                    Email = users.Email,
                    
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
        public IActionResult Delete(UserName model)
        {

            var users = _context.UserNames.SingleOrDefault(x => x.Id == model.Id);
            if (users != null)
            {
                _context.UserNames.Remove(users);
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
