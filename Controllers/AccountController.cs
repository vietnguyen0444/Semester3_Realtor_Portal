﻿using Microsoft.AspNetCore.Mvc;
using NETAPI_SEM3.Services;
using NETAPI_SEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETAPI_SEM3.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http;
using DemoSession16.Helpers;
using NETAPI_SEM3.Mails;

namespace NETAPI_SEM3.Controllers
{
	[Route("api/account")]
    [AllowAnonymous]
    public class AccountController : Controller
	{
		private readonly AccountService _accountService;
		private readonly MemberService _memberService;
		private readonly IConfiguration _configuration;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly EmailService _emailService;

		public AccountController(
			AccountService accountService,
			MemberService memberService,
			IConfiguration configuration,
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			RoleManager<IdentityRole> roleManager,
			EmailService emailService
			)
		{



			this._accountService = accountService;
			this._memberService = memberService;
			this._configuration = configuration;
			this._userManager = userManager;
			this._signInManager = signInManager;
			this._roleManager = roleManager;
			this._emailService = emailService;
		}

        [HttpPost("sendEmail")]
        public IActionResult Send([FromBody] MailRequest mailRequest)
        {
            try
            {
                var mailHelper = new MailHelper(_configuration);
                mailHelper.Send(_configuration["Gmail:Username"], mailRequest.email, mailRequest.subject, mailRequest.content);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return BadRequest("Username or password is incorrect.");
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return BadRequest("Your email was not confirmed.");
            }

            if (await _userManager.GetLockoutEnabledAsync(user) == false)
            {
                return BadRequest("Your account was locked.");
            }

            //check password
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return BadRequest("Username or password is incorrect.");
            }

            var resultToken = await GenerateToken(model);
            var role = await _userManager.GetRolesAsync(user);

            // Return token to user
            return Ok(new
            {
                token = resultToken,
                username = user.UserName,
                userId = user.Id,
                role = role
            });
        }

        private async Task<string> GenerateToken(AccountViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);

            IdentityOptions _options = new IdentityOptions();

            var claims = new List<Claim>
            {
                new Claim("Username", model.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                  _configuration["Tokens:Issuer"],
                  claims,
                  expires: DateTime.Now.AddDays(30),
                  signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //check username
            var hasUser = await _userManager.FindByNameAsync(model.Username);
            if (hasUser != null)
            {
                return BadRequest("Username was exist, choose another.");
            }

            //check email
            var hasEmail = await _userManager.FindByEmailAsync(model.Email);
            if (hasEmail != null)
            {
                return BadRequest("Email was exist, choose another.");
            }

            var user = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };



            // create account aspnetuser
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                string userRole;
                if (model.Position.Equals("Agent") || model.Position.Equals("Private Seller"))
                {
                    userRole = "Admin";
                }
                else
                {
                    userRole = "User";
                }
                // set role
                await _userManager.AddToRoleAsync(user, userRole);

                // create Member
                var member = new Member
                {
                    AccountId = user.Id,
                    Username = user.UserName,
                    FullName = model.FullName,
                    Status = true,
                    Email = user.Email,
                    RoleId = userRole,
                    Position = model.Position,
                    Photo = "avatar.png",
                    CreateDate = DateTime.Now,
                    IsShowMail = true
                };
                _memberService.CreateMember(member);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedEmailToken = Encoding.UTF8.GetBytes(token);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                var param = new Dictionary<string, string>
                {
                    {"token", validEmailToken },
                    {"email", user.Email }
                };

                var callback = QueryHelpers.AddQueryString(model.ClientURI, param);

                var message = new SendMailMessage(new string[] { model.Email }, "Email Confirmation", callback, null);
                await _emailService.SendEmailAsync(message);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation([FromQuery] string token, [FromQuery] string email)
        {
            if (email == null || token == null)
            {
                return BadRequest("Something wrong, please try again.");
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Account not found.");
            }

            var confirmResult = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (!confirmResult.Succeeded)
            {
                return BadRequest("Something wrong, please try again.");
            }

            return Ok();
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please enter right email.");
            }

            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
            if (user == null)
            {
                return BadRequest("Account not found.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedEmailToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            var param = new Dictionary<string, string>
            {
                {"token", validEmailToken },
                {"email", forgotPassword.Email }
            };

            var callback = QueryHelpers.AddQueryString(forgotPassword.ClientURI, param);

            var message = new SendMailMessage(new string[] { forgotPassword.Email }, "Reset password token", callback, null);
            await _emailService.SendEmailAsync(message);

            return Ok();
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user == null)
            {
                return BadRequest("Accountn not found.");
            }

            var decodedToken = WebEncoders.Base64UrlDecode(resetPassword.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var resetPassResult = await _userManager.ResetPasswordAsync(user, normalToken, resetPassword.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);

                return BadRequest("Invalid token, please resend email to resset password.");
            }

            return Ok();
        }

        [HttpPost("resendemailconfirm")]
        public async Task<IActionResult> ResendEmailConfirm([FromBody] AccountViewModel model)
        {
            var user = new IdentityUser
            {
                Email = model.Email
            };
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedEmailToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
            var param = new Dictionary<string, string>
                {
                    {"token", validEmailToken },
                    {"email", model.Email }
                };

            var callback = QueryHelpers.AddQueryString(model.ClientURI, param);

            var message = new SendMailMessage(new string[] { model.Email }, "Email Confirmation", callback, null);
            await _emailService.SendEmailAsync(message);
            return Ok();
        }

        // Profile
        [HttpPost("CheckPasswordDB")]
		public async Task<IActionResult> CheckPasswordDB([FromBody] AccountViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await _userManager.FindByNameAsync(model.Username);

			//check password
			if (!await _userManager.CheckPasswordAsync(user, model.Password))
			{
				return BadRequest("Invalid Password");
			}
			return Ok();
		}

		[HttpPost("updatePassword")]
		public async Task<IActionResult> UpdatePassword([FromBody] ResetPassword resetPassword)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var user = await _userManager.FindByEmailAsync(resetPassword.Email);
			if (user == null)
			{
				return BadRequest("Invalid Request");
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			//var decodedToken = WebEncoders.Base64UrlDecode(resetPassword.Token);
			var resetPassResult = await _userManager.ResetPasswordAsync(user, token, resetPassword.Password);
			if (!resetPassResult.Succeeded)
			{
				var errors = resetPassResult.Errors.Select(e => e.Description);

				return BadRequest(new { Errors = errors });
			}



			return Ok();
		}
	}
}
