using AutoMapper;
using Company.DAL.Model;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Company.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> rolemanager , IMapper mapper)
        {
            _rolemanager = rolemanager;
            _mapper = mapper;
        }  
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var users = await _rolemanager.Roles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    RoleName = r.Name,
                }).ToListAsync(); ;
                return View(users);
            }
            else
            {
                var role = await _rolemanager.FindByNameAsync(name);
                if(role is not null)
                {
                    var mappedrole = new RoleViewModel()
                    {
                        Id = role.Id,
                        RoleName = role.Name,
                    };
                    return View(new List<RoleViewModel>() { mappedrole });

                }
                return View(Enumerable.Empty<RoleViewModel>());
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                var mappedrole = _mapper.Map<RoleViewModel, IdentityRole>(roleVM);
                await _rolemanager.CreateAsync(mappedrole);
                return RedirectToAction(nameof(Index));
            }
            return View(roleVM);
        }
        public async Task<IActionResult> Details(string id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest(); // 400

            var role = await _rolemanager.FindByIdAsync(id);

            if (role is null)
                return NotFound();

            var mappedrole = _mapper.Map<IdentityRole, RoleViewModel>(role);
            //{
            //    Id = user.Id,
            //    FName = user.FName,
            //    LName = user.LName,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    Roles = _userManager.GetRolesAsync(user).Result
            //};


            return View(viewname, mappedrole);
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
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel updatedrole)
        {

            if (id != updatedrole.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _rolemanager.FindByIdAsync(id);
                    role.Name = updatedrole.RoleName;
                    //user.Email = updateduser.Email;
                    //user.SecurityStamp = Guid.NewGuid().ToString();

                    await _rolemanager.UpdateAsync(role);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //1. Log Exception
                    //2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(updatedrole);
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
                var user = await _rolemanager.FindByIdAsync(id);

                //user.Email = updateduser.Email;
                //user.SecurityStamp = Guid.NewGuid().ToString();

                await _rolemanager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }

        }

    }
}
