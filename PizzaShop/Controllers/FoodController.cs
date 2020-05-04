using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Model;
using PizzaShop.Repository;

namespace PizzaShop.Controllers
{
    [Route("api/food")]
    public class FoodController : Controller
    {
        public IRepository<Food,int> foodRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public FoodController(IRepository<Food,int> foodRepository, IHostingEnvironment hostingEnvironment)
        {
            this.foodRepository = foodRepository;
            this.hostingEnvironment = hostingEnvironment;
        }


        private string ProcesUploadFile(Food model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFoler = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFoler, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                return uniqueFileName;
            }
            else
                return "Nije uredu";
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Food> foods = foodRepository.GetAll();
            return Ok(foods);
        }
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetById(int id)
        {
            Food food = foodRepository.GetById(id);

            if (food != null)
                return Ok(food);
            return BadRequest(new { message = "Article is not found" });
        }

        [HttpPost]
        public IActionResult Create(Food food)
        {
            if (ModelState.IsValid)
            {
                if (food.Photo != null)
                {
                    if (food.PhotoPath != null)
                    {
                        string filePathCopy = Path.Combine(hostingEnvironment.WebRootPath, "images", food.PhotoPath);
                        System.IO.File.Delete(filePathCopy);
                    }
                    string filePath = ProcesUploadFile(food);
                    if (filePath != "")
                        food.PhotoPath = filePath;

                }
                foodRepository.Create(food);
                return Ok(food);
            }
            else
            {
                ModelState.AddModelError("", "Error. Check the form data.");
            }
            return BadRequest(new { message = "error", ModelState });
        }


        [Route("delete/{id}")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                foodRepository.Delete(id);
                return Ok(id);

            }
            catch 
            {
                return BadRequest(new { message = "Nije pronađen korisnik" });
            }


        }


        [HttpPut]
        public IActionResult Update(Food food)
        {

            if (ModelState.IsValid)
            {
                if (food.Photo != null)
                {
                    if (food.PhotoPath != null)
                    {
                        string filePathCopy = Path.Combine(hostingEnvironment.WebRootPath, "images", food.PhotoPath);
                        System.IO.File.Delete(filePathCopy);
                    }
                    string filePath = ProcesUploadFile(food);
                    if (filePath != "")
                        food.PhotoPath = filePath;

                }



                Food employeeUpdated = foodRepository.Update(food);

                //string filePathCopy2 = Path.Combine(hostingEnvironment.WebRootPath, "images", food.PhotoPath);
                //var base64String = Convert.ToBase64String(System.IO.File.ReadAllBytes(filePathCopy2));


                if (employeeUpdated != null)
                {
                    return Ok(employeeUpdated);
                }
            }

            return BadRequest(new { message = "Nije pronađen korisnik", ModelState });
        }


    }
}