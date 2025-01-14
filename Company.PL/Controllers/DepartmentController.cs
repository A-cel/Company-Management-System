using Microsoft.AspNetCore.Mvc;
using Company.BLL.Repositories;
using Company.BLL.Interfaces;
using Company.DAL.Model;
using System;
namespace Company.PL.Controllers
{
    //Department Controller is in 2 relationships 
    // 1) inheritance : DeptController is a Controller
    // 2) Association : DeptController Has a DeptRepo
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IUnitOfWork unitOfWork/*,IDepartmentRepository depratmentRepo*/)
        {
            _unitOfWork = unitOfWork;
            //_departmentRepo = depratmentRepo;
        }
        public IActionResult Index()
        {
            var dept = _unitOfWork.DepartmentRepository.GetAll();
            return View(dept);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.DepartmentRepository.Add(department);
                var count = _unitOfWork.Complete();
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Details(int? id ,string viewname = "Details")
        {
            if (!id.HasValue)
                return BadRequest(); // 400

            var depts = _unitOfWork.DepartmentRepository.GetById(id.Value);
            if (depts is null) 

                return NotFound();

            return View(viewname, depts);
        }
      
        public IActionResult Edit(int? id)
        {
            return Details(id,"Edit");
            //if (id is null)
            //    return BadRequest();

            //var dept = _departmentRepo.GetById(id.Value);
            //if(dept is null)
            //    return NotFound();

            //return View(dept);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,Department department)
        {
            if(id != department.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Complete();
                }
                catch(Exception ex)
                {
                    //1. Log Exception
                    //2. Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }
        
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
            //if(id is null)
            //    return BadRequest();
            //if(ModelState.IsValid)
            //{
            //    _departmentRepo.GetById(id.Value);
            //}
            //return View(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int id,Department department)
        {
            if (id != department.Id)
                return BadRequest();
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(department);
        }

    }
}
