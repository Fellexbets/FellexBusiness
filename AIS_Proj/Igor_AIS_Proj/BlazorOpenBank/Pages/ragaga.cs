//// Licensed to the .NET Foundation under one or more agreements.
//// The .NET Foundation licenses this file to you under the MIT license.
//#nullable disable

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Text.Encodings.Web;
//using System.Threading;
//using System.Threading.Tasks;
//using BlazorOpenBank.Data.Services.Base;
//using BlazorOpenBank.Data.UserServices;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.WebUtilities;
//using Microsoft.Extensions.Logging;

//namespace BlazorOpenBank.Pages
//{
//    public class Register : PageModel
//    {

//        private readonly ILogger<Register> _logger;
//        private UserService _userService;


//        public Register
//        (
//            ILogger<Register> logger
//, UserService userService)
//        {
//            _logger = logger;
//            _userService = userService;
//        }

//        [BindProperty]
//        public CreateUserRequest CreateUser { get; set; }


//        public async Task OnGetAsync()
//        {
//        }

//        public async Task OnPostAsync()
//        {
//            var api = new Data.Services.Base.Client(new HttpClient());
//            await api.Create4Async(CreateUser);
//        }
//    }
//}
