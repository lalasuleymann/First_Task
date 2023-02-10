using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Task1_T.Constants;
using Task1_T.Extensions;
using Task1_T.Models.Dtos.Users;
using Task1_T.Models.Entities;
using Task1_T.Routes;
using Task1_T.Services.Departments;
using Task1_T.Services.Users;
using static Task1_T.Extensions.ClaimRequirementFilter;
using AuthorizeAttribute = Task1_T.Extensions.ClaimRequirementFilter.AuthorizeAttribute;

namespace Task1_T.Controllers
{

    public class IdentityController : BaseController
    {
        private readonly IUserService _userService;
        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register (UserRegistrationRequest request)
        {
            return Ok(await _userService.RegisterAsync(request.Name,request.Surname,request.Email, request.Password, request.RePassword));
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            return Ok(await _userService.LoginAsync(request.Email, request.Password));
        }
        
        [HttpGet(ApiRoutes.Identity.GetAll)]
        [Authorize(AdminPermissions.Admin)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetSignedUsers());
        }
    }
}
