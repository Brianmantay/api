using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Dtos;
using api.Services;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class TokenIssuerController : Controller
    {
        ITokenService _tokenService;
        public TokenIssuerController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // POST api/TokenIssuer
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody]UserDto user)
        {
            var token = _tokenService.GenerateToken(user);
            return new OkObjectResult(token);
        }
    }
}
