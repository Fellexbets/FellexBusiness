using System.Collections.Generic;
using System.Threading.Tasks;
using Igor_AIS_Proj.Auxiliary;
using Igor_AIS_Proj.Business;
using Igor_AIS_Proj.Controllers;
using Igor_AIS_Proj.Models;
using Igor_AIS_Proj.Models.Responses;
//using
//namespace QuizzalT_API.Controllers
//{
//    public class IdentityController : Controller
//    {
//        private readonly IIdentityService _identityService;

//        public IdentityController(IIdentityService identityService)
//        {
//            _identityService = identityService;
//        }

//        [HttpPost, AllowAnonymous]
//        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
//        {
//            var authResponse = await _identityService.RegisterAsync(request.Email, request.Userpassword);

//            if (!authResponse.Success)
//            {
//                return BadRequest(new AuthFailResponse
//                {
//                    Errors = authResponse.Errors
//                });
//            }

//            return Ok(new AuthSuccessResponse()
//            {
//                Token = authResponse.Token
//            });
//        }
//    }

    
////}
// Igor_AIS_Proj.Persistence;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

