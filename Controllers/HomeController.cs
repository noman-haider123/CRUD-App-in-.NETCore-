using Microsoft.AspNetCore.Mvc;
using Noman.Models;
using Noman.Respository;
using System.Diagnostics;
using System.Linq;

namespace Noman.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStudent _student;
        public HomeController(ILogger<HomeController> logger, IStudent student)
        {
            _logger = logger;
            _student = student;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _student.Index();
            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                var errros = "<ul>" + string.Join("",
                    ModelState.Values.SelectMany(e => e.Errors)
                    .Select(m => $"<li class = 'list-unstyled'>{m.ErrorMessage}</li>")) + "</ul>";
                ViewData["errors"] = errros;
                return View(student);
            }
            if(student.UploadImage == null)
            {
                ViewData["errors"] = "The Image Field is Required";
                return View(student);
            }
            var extension = Path.GetExtension(student.UploadImage.FileName).ToLower();
            var file = Path.GetFileName(student.UploadImage.FileName).ToLower(); // Optional
            var allowed_extension = new[] { ".jpg", ".png", ".jpeg" };
            if (!allowed_extension.Contains(extension))
            {
                ViewData["errors"] = "Only jpg,jpeg,png Format are Allowed";
                return View(student);
            }
            else if (student.UploadImage.Length > 2 * 1024 * 1024)
            {
                ViewData["errors"] = "Max 2MB Size File is Allowed";
                return View(student);
            }
            var filename = file;
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", filename);
            using var stream = new FileStream(filepath, FileMode.Create);
            await student.UploadImage.CopyToAsync(stream);
            student.Image = filename;
            await _student.Store(student);
            TempData["message"] = "Data Submitted Successfully";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           var student = await _student.Get(id);
           if (student == null)
           {
                return NotFound();
           }
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (!ModelState.IsValid)
            {
                var errors = "<ul>" +
                    string.Join("",
                    ModelState.Values.SelectMany(e => e.Errors)
                   .Select(m => $"<li class = 'list-unstyled'>{m.ErrorMessage}</li>")
                   ) + "</ul>";
                ViewData["errors"] = errors;
                return View(student);
            }
            var exsistingstudent = await _student.Get(id);
            if (exsistingstudent == null)
            {
                return NotFound();
            }
            if (student.UploadImage == null)
            {
                student.Image = exsistingstudent.Image;
            }
            else
            {
                var extension = Path.GetExtension(student.UploadImage.FileName).ToLower();
                var allowed_extension = new[] { ".jpg", ".png", ".jpeg" };

                if (!allowed_extension.Contains(extension))
                {
                    ViewData["errors"] = "Only jpg, jpeg, png formats are allowed";
                    return View(student);
                }

                if (student.UploadImage.Length > 2 * 1024 * 1024)
                {
                    ViewData["errors"] = "Max 2MB size allowed";
                    return View(student);
                }
                var filename = Path.GetFileName(student.UploadImage.FileName).ToLower();
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", filename);
                using var stream = new FileStream(filepath, FileMode.Create);
                await student.UploadImage.CopyToAsync(stream);
                student.Image = filename;
                if (!string.IsNullOrEmpty(exsistingstudent.Image))
                {
                    var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", exsistingstudent.Image);
                    if (System.IO.File.Exists(oldpath))
                    {
                        System.IO.File.Delete(oldpath);
                    }
                }
            }
             await _student.Update(id, student);
            TempData["message"] = "Data Updated Successfully";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var exsistingstudent = await _student.Get(id);
            if(exsistingstudent == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(exsistingstudent.Image))
            {
              var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", exsistingstudent.Image);
                if (System.IO.File.Exists(oldpath))
                {
                    System.IO.File.Delete(oldpath);
                }
            }
            await _student.Delete(id);
            TempData["message"] = "Data Deleted Successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
