using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Data;
using Company.DAL.Model;
using Company.PL.MappingProfilesss;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepository _employeeRepo;
        //private readonly IDepartmentRepository _departmentRepo;
        public EmployeeController(IMapper mapper,IUnitOfWork unitOfWork/*,IEmployeeRepository employeeRepo,IDepartmentRepository depratmentRepository*/)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
           // _employeeRepo = employeeRepo;
            //_departmentRepo = depratmentRepository;
        }
        //[HttpGet]
        public IActionResult Index(string searchinp)
        { 
            //ViewData["Message"] = "Hello From View Data";
            //ViewBag.Message = "Hello";
            var emp = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchinp))
            {
                 emp = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            { 
                 emp = _unitOfWork.EmployeeRepository.SearchByName(searchinp.ToLower());  
            }
            var mapped = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emp);
            return View(mapped);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel EmployeeVM)
        {
            if (ModelState.IsValid)
            {
                EmployeeVM.ImageName = DocumentSettings.UploadFile(EmployeeVM.Image, "images");
                //var MappedEmp = new Employee()
                //{
                //    Name = EmployeeVM.Name,
                //    Age = EmployeeVM.Age,
                //    Adderss = EmployeeVM.Adderss,
                //    Salary = EmployeeVM.Salary,
                //    PhoneNumber = EmployeeVM.PhoneNumber,
                //    Email = EmployeeVM.Email,
                //    IsActive = EmployeeVM.IsActive,
                //    HireDate = EmployeeVM.HireDate,
                //};
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM); 
                 _unitOfWork.EmployeeRepository.Add(MappedEmp);
                var count = _unitOfWork.Complete();
                if (count > 0)
                return RedirectToAction(nameof(Index));
                
            }
            return View(EmployeeVM);
        }
        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var empss = _unitOfWork.EmployeeRepository.GetById(id.Value);
            
            if (empss is null)
                return NotFound();
            
            var mappedemp = _mapper.Map<Employee, EmployeeViewModel>(empss);
            

            return View(viewname, mappedemp);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //ViewData["Departments"] = _departmentRepo.GetAll();

            return Details(id, nameof(Edit));

            //if (id is null)
            //    return BadRequest();

            //var dept = _employeeRepo.GetById(id.Value);
            //if(dept is null)
            //    return NotFound();

            //return View(dept);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {

            if (id != EmployeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                    _unitOfWork.EmployeeRepository.Update(MappedEmp);
                    var empedit = _unitOfWork.Complete();
                    if (empedit > 0)
                        return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    //1. Log Exception
                     //2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(EmployeeVM);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
            //if(id is null)
            //    return BadRequest();
            //if(ModelState.IsValid)
            //{
            //    _employeeRepo.GetById(id.Value);
            //}
            //return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
                return BadRequest();
            try
            {
                var MappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                _unitOfWork.EmployeeRepository.Delete(MappedEmp);
                var count = _unitOfWork.Complete();
                if (count > 0)
                    DocumentSettings.DeleteFile(EmployeeVM.ImageName, "images");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(EmployeeVM);
        }

    }
}
