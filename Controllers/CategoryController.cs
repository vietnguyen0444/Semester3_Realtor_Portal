using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETAPI_SEM3.Models;
using NETAPI_SEM3.Security;
using NETAPI_SEM3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Controllers
{
    [Route("api/category")]
	public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

		[Produces("application/json")]
		[HttpGet("GetAllCategory")]
		public IActionResult GetAllCategory()
		{
			try
			{
				return Ok(_categoryService.GetAllCategory());
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPost("createCategory")]
		public IActionResult createCategory([FromBody] Category category)
		{
			try
			{
				return Ok(_categoryService.createCategory(category));
			}
			catch
			{
				return StatusCode(500, "Can not get category");
			}
		}

		[Produces("application/json")]
		[HttpGet("findCategory/{categoryId}")]
		public IActionResult findCategory(int categoryId)
		{

			try
			{
				return Ok(_categoryService.findCategory(categoryId));
			}
			catch
			{
				return StatusCode(500, "Can not get news");
			}
		}

		[HttpDelete("deleteCategory/{categoryId}")]
		public IActionResult deleteCategory(int categoryId)
		{
			try
			{
				if (_categoryService.deleteCategory(categoryId))
				{
					return Ok();
				}
				return StatusCode(500, "Can not delete news - Func");
			}
			catch
			{
				return StatusCode(500, "Can not delete news");
			}
		}

		[Produces("application/json")]
		[HttpPut("updateCategory")]
		[Consumes("application/json")]
		public IActionResult updateCategory([FromBody] Category category)
		{
			try
			{
				if (_categoryService.updateCategory(category))
				{
					return Ok();
				}
				return StatusCode(500, "Can not update news - Func");
			}
			catch
			{
				return StatusCode(500, "Can not update news");
			}
		}


		[Produces("application/json")]
		[Consumes("application/json")]
		[HttpGet("propertybycategory/{categoryId}")]
		public IActionResult PropertyByCategory(int categoryId)
		{
			try
			{
				var properties = _categoryService.PropertyByCategory(categoryId);
				return Ok(properties);
			}
			catch (Exception e2)
			{
				return BadRequest(e2.Message);
			}
		}

	}
}
