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
    [Route("api/user")]
    [AllowAnonymous]
    public class AboutUsController : Controller
    {
        private AboutUsService aboutUsService;

        public AboutUsController(AboutUsService _aboutUsService)
        {
            aboutUsService = _aboutUsService;
        }

        [Produces("application/json")]
        [HttpGet("loadagentAU")]
        public IActionResult LoadAgentAU()
        {
            try
            {
                var agents = aboutUsService.loadAgentAU();

                return Ok(agents);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("loadsalecount")]
        public IActionResult LoadSaleCount()
        {
            try
            {
                var sale = aboutUsService.SaleCount();

                return Ok(sale);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("loadrentcount")]
        public IActionResult LoadRentCount()
        {
            try
            {
                var rent = aboutUsService.RentCount();

                return Ok(rent);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("loadsetting")]
        public IActionResult LoadSetting()
        {
            try
            {
                var settings = aboutUsService.loadSetting();

                return Ok(settings);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Produces("application/json")]
        [HttpGet("getAllFAQ")]
        public IActionResult getAllFAQ()
        {
            try
            {
                return Ok(aboutUsService.getAllFAQ());
            }
            catch
            {
                return StatusCode(500, "Can not get all faq!");
            }
        }

    }
}