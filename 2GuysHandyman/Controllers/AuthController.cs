using _2GuysHandyman.ApiModels;
using _2GuysHandyman.ApiModels.AuthApiModels;
using _2GuysHandyman.models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace _2GuysHandyman.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly dbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthController(dbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<UsersApiModel>> GetMe()
        {   
            return mapper.Map<UsersApiModel>(
                 dbContext.Users.FirstOrDefault(u => u.Email == User.Identity.Name));
        }

        [HttpPost("register")]
        public async Task<ActionResult<Users>> Register(RegistrationApiModel user)
        {
            if (dbContext.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest("An user with this email is already exist!");
            }

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = mapper.Map<Users>(user);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            await dbContext.Users.AddAsync(newUser);
            await dbContext.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginApiModel user)
        {
            var dbUser = dbContext.Users.Where(u => u.Email == user.Email).First();
            if (dbUser == null)
            {
                return BadRequest("User not found.");
            }
            if (!VeryfyPasswordHash(user.Password, dbUser.PasswordHash, dbUser.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }

            string token = CreateToken(dbUser);

            return Ok(token);

        }

        //[HttpPost("register")] регистрация нового клиента с генерированным паролем, который сделал заказ, не заходя в систему


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VeryfyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim> 
            {
                new Claim(ClaimTypes.Name, user.Email),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));
           
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
           
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
