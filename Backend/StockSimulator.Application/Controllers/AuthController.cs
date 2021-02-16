using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockSimulator.Application.ViewModels;
using StockSimulator.CrossCutting.Configuration;
using StockSimulator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockSimulator.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly GeneralConfig _generalConfig;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<GeneralConfig> generalConfig)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _generalConfig = generalConfig.Value;
        }

        [HttpPost("sign_up")]
        public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new User
            {
                UserName = registerUser.Name,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);
            return Ok(await GenerateJwt(registerUser.Email));
        }

        [HttpPost("sign_in")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {

        }

        private Task<object> GenerateJwt(string email)
        {
            throw new NotImplementedException();
        }
    }
}
