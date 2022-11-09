using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
using Task1_T.Routes;
using Task1_T.Services.Users;

namespace Task1_T.Controllers
{

    public class IdentityController : BaseController
    {
        private readonly IUserService _userService;
        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register (UserRegistrationRequest request)
        {
            return Ok(await _userService.RegisterAsync(request.Email, request.Password));
        }


        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login(UserRegistrationRequest request)
        {
            return Ok(await _userService.LoginAsync(request.Email, request.Password));
        }

    }
}
