using Microsoft.AspNetCore.Mvc;
using RealEstateApi.Data;
using RealEstateApi.Models;

namespace RealEstateApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ApiDbContext _dbContext = new ApiDbContext();
        // GET: api/<CategoriesController>

        [HttpGet]
        public IEnumerable<Category> Get() --> Get Request
        {
            return _dbContext.Categories;
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id) --> Get with Id Request
        {
            var category = _dbContext.Categories.FirstOrDefault(x=>x.Id == id);
            //return StatusCode(StatusCodes.Status200OK); --> To show any status code
            return Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] Category category) --> Post Request
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Category categoryObj) --> Put Request
        {
            var category = _dbContext.Categories.Find(id);
            if(category == null)
            {
                return NotFound("No records found for Id " + id);
            }
            else
            {
                category.Name = categoryObj.Name;
                category.ImageUrl = categoryObj.ImageUrl;
                _dbContext.SaveChanges();
                return Ok();
            }
            
        }
        [HttpGet("[action]")]
        public IActionResult GetSortCategory()
        {
            return Ok(_dbContext.Categories.OrderByDescending(x => x.Name));
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) --> Delete Request
        {
            var category = _dbContext.Categories.Find(id);
            if(category == null)
            {
                return NotFound("The Id " + id + " does not exist");
            }
            else
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return Ok("Deleted successfully");

            }
            
        }
    }
}
