﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETAPI_SEM3.Models;
using NETAPI_SEM3.Services.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Controllers.User
{
	[Route("api/user/")]
	[AllowAnonymous]
	public class IndexController : Controller
	{

		private IndexService indexService;
		public IndexController(IndexService _indexService)
		{
			indexService = _indexService;
		}


		[Produces("application/json")]
		[HttpGet("loadtopproperty")]
		public IActionResult LoadTopProperty()
		{
			try
			{
				var properties = indexService.LoadTopProperty();

				return Ok(properties);
			}
			catch(Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[Produces("application/json")]
		[HttpGet("loadpopularlocation")]
		public IActionResult LoadCategoriesNumber()
		{
			try
			{
				var categories = indexService.LoadPopularLocations();

				return Ok(categories);
			}
			catch
			{
				return BadRequest();
			}
		}
		[Produces("application/json")]
		[HttpGet("loadcountries")]
		public IActionResult LoadCountries()
		{
			try
			{
				var countries = indexService.LoadCountries();
				return Ok(countries);
			}
			catch (Exception e2)
			{
				return BadRequest(e2.Message);
			}
		}
		[Produces("application/json")]
		[HttpGet("loadcategories")]
		public IActionResult LoadCategories()
		{
			try
			{
				var countries = indexService.LoadCategories();
				return Ok(countries);
			}
			catch (Exception e2)
			{
				return BadRequest(e2.Message);
			}
		}
	}
}