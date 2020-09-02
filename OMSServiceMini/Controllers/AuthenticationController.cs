using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using OMSServiceMini.Services.Authenticatinon;
using OMSServiceMini.Models;
using OMSServiceMini.Data;
using Microsoft.EntityFrameworkCore;

namespace OMSServiceMini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        // POST api/authentication
        [AllowAnonymous]
        public ActionResult<string> Post(
                AuthenticationRequest authRequest,
                [FromServices] IJwtSigningEncodingKey signingEncodingKey)
        {
            // 1. Проверяем данные пользователя из запроса.
            // ...

            // 2. Создаем утверждения для токена.
            var claims = new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, authRequest.Name)
            };

            // 3. Генерируем JWT.
            var token = new JwtSecurityToken(
                issuer: "OMSServiceMini",
                audience: "OMSServiceMiniClient",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSomething()
        {
            var nameIdentifier = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return new string[] { nameIdentifier?.Value, "value1", "value2" };
        }
    }
}