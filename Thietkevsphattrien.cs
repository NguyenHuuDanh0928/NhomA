using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;

namespace YourNamespace.Models;
    public class ServiceModel
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; } 
        public decimal Price { get; set; } 
       
    }
public class ServicesController : Controller
{
    public IActionResult Index()
    {
        return View(); 
    }

    public IActionResult Details(int id)
    {
        return View(); 
    }

    [HttpPost]
    public IActionResult Book(ServiceModel model)
    {
        if (ModelState.IsValid)
        {
            return RedirectToAction("Index"); 
        }
        return View(model); 
    }
}