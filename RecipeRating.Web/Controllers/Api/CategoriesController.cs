using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeRating.DAL;
using RecipeRating.Model;
using RecipeRating.Model.Interfaces;
using RecipeRating.Web.Models.DTO;
using RecipeRating.Web.Models.InputModel;

namespace RecipeRating.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CategoriesController(IRepositoryWrapper repository, ILogger<CategoriesController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _repository.Category.GetAll();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(categoriesDto);
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _repository.Category.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDTO>(category));
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<IActionResult> PostCategory(CategoryInputModel category)
        {
            var newCategory = new Category
            {
                Name = category.Name,
            };
            await _repository.Category.AddAsync(newCategory);
            await _repository.Category.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, [FromBody] CategoryInputModel categoryInput)
        {
            var category = await _repository.Category.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(categoryInput.Name))
            {
                category.Name = categoryInput.Name;
            }

            try
            {
                await _repository.Category.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Internal server error");
            }

            return Ok();
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _repository.Category.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _repository.Category.DeleteAsync(category);
            await _repository.Category.SaveChangesAsync();

            return Ok();
        }
    }
}
