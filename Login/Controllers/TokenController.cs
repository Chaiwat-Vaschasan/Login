
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Login.Database;
using Login.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Login.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class TokenController : ControllerBase
    {

        private ILogger<TokenController> _logger;
        private readonly DatabaseContext context;
        private IConfiguration Configuration { get; }

        public TokenController(ILogger<TokenController> logger, DatabaseContext context, IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.context = context;
            _logger = logger;
        }

        [HttpGet("VisitorToken")]
        public IActionResult GetVisitorToken()
        {
            try
            {
                //key
                string securityKey = Configuration.GetConnectionString("securityKey");
                //symmetric key
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                //signingCredentials 
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
                //setting Claims
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "Visitor"));
                // create token
                var token = new JwtSecurityToken(
                   issuer: Configuration.GetConnectionString("Issuer"),
                   audience: Configuration.GetConnectionString("Audience"),
                   expires: DateTime.Now.AddHours(1),
                   signingCredentials: signingCredentials,
                   claims: claims
               );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("UserToken")]
        public IActionResult GetUserToken([FromBody] GetToken User)
        {
            try
            {
                Account result = context.Account.SingleOrDefault(a =>
                      a.UserName == User.UserName && a.Password == User.Password);
                if (result == null)
                {
                    return Forbid();
                }
                //key
                string securityKey = Configuration.GetConnectionString("securityKey");
                //symmetric key
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
                //signingCredentials 
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
                //setting Claims
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, result.Role));
                // create token
                var token = new JwtSecurityToken(
                   issuer: Configuration.GetConnectionString("Issuer"),
                   audience: Configuration.GetConnectionString("Audience"),
                   expires: DateTime.Now.AddHours(1),
                   signingCredentials: signingCredentials,
                   claims: claims
               );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

    }
}