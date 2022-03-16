using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleTracking.Business.Interfaces;
using VehicleTracking.Core.Models;

namespace VehicleTrackingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// The login service
        /// </summary>
        private readonly ILoginService _loginService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="loginService">The login service.</param>
        public LoginController(ILoginService loginService)
        {
            this._loginService = loginService;
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult Login([FromBody] UserAPIModel user)
        {
            var isExist = _loginService.Login(user.UserName, user.Password);
            if (!isExist) return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim("type", "Admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SXkSqsKyNUyvGbnHs7ke2NCq8zQzNLW7mPmHbnZZ"));

            var token = new JwtSecurityToken(
                "https://fbi-demo.com",
                "https://fbi-demo.com",
                claims,
                expires: DateTime.Now.AddDays(30.0),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new OkObjectResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
