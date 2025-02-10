using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Day4API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public IActionResult Login(string _userName, string _password)
        {
            if (_userName == "admin" && _password == "123")
            {
                #region Generate Token
                // Header => Sepcify Hasihng Algorithm  2
                // Payload => Data (Claims), expire date  1
                // Signature => Hash of Header + Payload + Secret Key   3

                #region Claims

                // Claim => Key Value Pair
                List<Claim> values = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Email, "")
                };

                #endregion

                #region Secret Key

                // Generate Secret Key
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MYSECRETKEYISSANDsoil123@#!@and here am creating my scret key to test it hahahah"));

                #endregion

                // Generate Signature
                var signingCred = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                #region Token
                var token = new JwtSecurityToken(
                    claims: values,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: signingCred
                    );
                #endregion


                // Encode Token to String
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                #endregion
                return Ok(tokenString);
            }
            return Unauthorized();
        }
    }
}
