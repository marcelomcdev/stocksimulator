using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockSimulator.Application.Security;
using StockSimulator.Application.ViewModels;
using StockSimulator.CrossCutting.Configuration;
using StockSimulator.Domain.Entities;
using StockSimulator.Domain.Interfaces.Services;
using StockSimulator.Service.QuoteSimulator;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
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
        private readonly IAccountService _accountService;
        private readonly IOperationService _operationService;
        private IListenerService _listener;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<GeneralConfig> generalConfig, IAccountService accountService, IOperationService operationService, IListenerService listener)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _generalConfig = generalConfig.Value;
            _accountService = accountService;
            _operationService = operationService;
            _listener = listener;
        }

        [HttpPost("sign_up")]
        public async Task<ActionResult> Register(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new User
            {
                Name = registerUser.Name,
                UserName = registerUser.Email.Split('@')[0],
                Email = registerUser.Email,
                EmailConfirmed = true,
                CPF = registerUser.CPF
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);
            else
            {
                _accountService.InsertIdentity(new Account() { 
                                                    UserId = user.Id, 
                                                    Bank = 352, 
                                                    Branch = 1, 
                                                    AccountNumber = (_accountService.GetLastAccountNumber() + 1), 
                                                    TotalBalance = 0 
                                                });
            }

            await _signInManager.SignInAsync(user, false);
            return Ok(await GenerateJwt(registerUser.Email));
        }

        [HttpPost("sign_in")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(er => er.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, false);

            if (result.Succeeded)
                return Ok(await GenerateJwt(loginUser.UserName));

            return BadRequest("Usuário ou senha inválidos.");
        }

        private async Task<SessionToken> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if(user == null)
                user = await _userManager.FindByNameAsync(email);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_generalConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _generalConfig.Issuer,
                Audience = _generalConfig.ValidIn.First(),
                Expires = DateTime.UtcNow.AddHours(_generalConfig.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            Response.Headers.Add("access-token", token);
            Response.Headers.Add("client", user.UserName);
            Response.Headers.Add("uid", user.Id);

            return new SessionToken()
            {
                AccessToken = token,
                Client = user.UserName,
                UID = user.Id
            };
        }
    }
}
