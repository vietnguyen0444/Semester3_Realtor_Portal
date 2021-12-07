using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NETAPI_SEM3.Models;
using NETAPI_SEM3.Security;
using NETAPI_SEM3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Controllers
{
	[Route("api/member")]
	public class MemberController : Controller
	{
		private MemberService memberService;
		private readonly IMapper _mapper;
		private readonly IConfiguration _configuration;
		private readonly UserManager<IdentityUser> _userManager;

		public MemberController(IMapper mapper, IConfiguration _configuration, UserManager<IdentityUser> userManager, MemberService _memberService)
		{
			this._mapper = mapper;
			this._configuration = _configuration;
			this._userManager = userManager;
			this.memberService = _memberService;
		}

		[HttpGet]
		public IActionResult GetAllMember()
		{
			try
			{
				return Ok(memberService.GetAllMember());
			}
			catch
			{
				return BadRequest();
			}
		}

		[Produces("application/json")]
		[HttpPut("updateMember")]
		public IActionResult updateMember([FromBody] Member member)
		{
			try
			{
				if (memberService.updateMember(member))
				{
					return Ok();
				}
				return StatusCode(500, "Can not update member - Func");
			}
			catch
			{
				return StatusCode(500, "Can not update member");
			}
		}

		[Produces("application/json")]
		[HttpGet("findUser/{userId}")]
		public IActionResult findUser(string userId)
		{
			try
			{
				return Ok(memberService.findUser(userId));
			}
			catch
			{
				return StatusCode(500, "Can not update member");
			}
		}

		[HttpGet("{page}")]
		public IActionResult GetAllMemberPage(int page)
		{
			try
			{
				return Ok(memberService.GetAllMemberPage(page));
			}
			catch(Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[HttpGet("search/{fullName}/{position}/{status}")]
		public IActionResult SearchMember(string fullName, string position, string status)
		{
			try
			{
				return Ok(memberService.SearchMember(fullName, position, status));
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpGet("search/{fullName}/{position}/{status}/{page}")]
		public IActionResult SearchMemberPage(string fullName, string position, string status, int page)
		{
			try
			{
				return Ok(memberService.SearchMemberPage(fullName, position, status, page));
			}
			catch
			{
				return BadRequest();
			}
		}

		[Produces("application/json")]
		[Consumes("application/json")]
		[HttpPut("updateStatus/{memberId}/{userId}")]
		public IActionResult UpdateStatus(int memberId, string userId, [FromBody] bool status)
		{
			try
			{
				var userTask = _userManager.FindByIdAsync(userId);
				userTask.Wait();
				var user = userTask.Result;

				if (status == true)
				{
					_userManager.SetLockoutEnabledAsync(user, false);
				}
				else
				{
					_userManager.SetLockoutEnabledAsync(user, true);
				}
				memberService.UpdateStatus(memberId, status);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}
		}
	}
}
