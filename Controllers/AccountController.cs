using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopSite.CW.WebApp.Models;
using ShopSite.CW.WebApp.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

//using ShopSite.CW.WebApp.Services;

namespace ShopSite.CW.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Declare private variables for UserManager, SignInManager, EmailService, and IConfiguration
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;

        // Constructor to initialize the above variables
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
         EmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        // Endpoint to handle user registration
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthModel model)
        {
            // Create IdentityUser with provided email and password
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Generate an email verification token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Create the verification link
                var verificationLink = Url.Action("VerifyEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);

                // Send the verification email
                var emailSubject = "Email Verification";
                var emailBody = $"Please verify your email by clicking the following link: {verificationLink}";
                _emailService.SendEmail(user.Email, emailSubject, emailBody);
               // Return success message
                return Ok("User registered successfully. An email verification link has been sent.");
            }
            // If user creation fails, return error messages
            return BadRequest(result.Errors);
        }


        // Add an action to handle email verification
        [HttpGet("verify-email")]// Endpoint to handle email verification
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            // Find user by userId
            var user = await _userManager.FindByIdAsync(userId);
            // If user not found, return  message
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Confirm email using token
            var result = await _userManager.ConfirmEmailAsync(user, token);

            // If email confirmation is successful, return success message
            if (result.Succeeded)
            {
                return Ok("Email verification successful.");
            }
            // If email confirmation fails, return error message
            return BadRequest("Email verification failed.");
        }


        // Endpoint to handle user login
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthModel model)
        {
            // Attempt user login using SignInManager
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email); // Find user by email
                var roles = await _userManager.GetRolesAsync(user); // Get user roles
                var token = GenerateJwtToken(user,roles); // Generate JWT token
                return Ok(new { Token = token });  // Return token
            }

            return Unauthorized("Invalid login attempt."); // If login fails, return unauthorized error message
        }

        // Endpoint to handle user logout
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Sign out the user
            return Ok("Logged out");
        }
        
        // Method to generate JWT token
        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            //creates a new claim for a JWT
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //jti is claim type
                //generates a new unique identifier using the Guid.NewGuid() method
            };

            // Add roles as claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }

}