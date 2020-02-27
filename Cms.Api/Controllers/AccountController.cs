using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Cms.Core.Commands.Register;
using Cms.Core.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Cms.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public AccountController(IConfiguration configuration,IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterInputVm model)
        {
            var result = await _mediator.Send(new RegisterInputCommand
            {
                Email=model.Email,
                Password=model.Password
            });
            return CustomOk(result);

        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginInputVm model)
        {

            var result = await _mediator.Send(new LoginInputQuery
            {
                Email = model.Email,
                Password = model.Password
            });
            return CustomOk(result);

           

        }

        [HttpGet("test")]
        [Authorize]
        public  async Task<IActionResult> Test()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            return Ok("success "+ userId);

        }

   
      
    }
}