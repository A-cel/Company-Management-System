using AutoMapper;
using Company.DAL.Model;
using Company.PL.MappingProfilesss;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var users = await _userManager.Users.Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    FName = u.FName,
                    LName = u.LName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).ToListAsync(); ;
                return View(users);
            }
            else
            {
                var user = await _userManager.FindByEmailAsync(email);
                var mappeduser = new UserViewModel()
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(user).Result
                };
                return View(new List<UserViewModel>() { mappeduser });
            }
        }
        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest(); // 400

            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            var mappeduser = _mapper.Map<ApplicationUser , UserViewModel>(user);
            //{
            //    Id = user.Id,
            //    FName = user.FName,
            //    LName = user.LName,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    Roles = _userManager.GetRolesAsync(user).Result
            //};


            return View(viewname, mappeduser);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();

            return await Details(id, nameof(Edit));

            //if (id is null)
            //    return BadRequest();

            //var dept = _employeeRepo.GetById(id.Value);
            //if(dept is null)
            //    return NotFound();

            //return View(dept);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel updateduser)
        {

            if (id != updateduser.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);   
                    user.FName = updateduser.FName;
                    user.LName = updateduser.LName;
                    user.PhoneNumber = updateduser.PhoneNumber;
                    //user.Email = updateduser.Email;
                    //user.SecurityStamp = Guid.NewGuid().ToString();

                    await _userManager.UpdateAsync(user);

                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //1. Log Exception
                    //2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(updateduser);
        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
            //if(id is null)
            //    return BadRequest();
            //if(ModelState.IsValid)
            //{
            //    _employeeRepo.GetById(id.Value);
            //}
            //return View(id);
        }
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                //user.Email = updateduser.Email;
                //user.SecurityStamp = Guid.NewGuid().ToString();

                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error","Home");
            }

        }

    }
}
