//using BlazorOpenBank.Data.UserServices;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Web;
//using BlazorOpenBank.Data.Services.Base;

//namespace BlazorOpenBank.Pages
//{
//    public class ragaga : PageModel
//    {
//        private HttpClient _httpClient;
//        public ragaga(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//        }

//        [BindProperty]
//        public LoginUserRequest LoginRequest { get; set; }

//        public string ReturnUrl { get; set; }

//        public string Errors { get; set; }

//        public bool ShowSignInErrors { get; set; }

//        public bool IsProcessing { get; set; } = false;
//        [Inject]
//        private UserService _userServices { get; set; }

//        [Inject]
//        public NavigationManager _navigationManager { get; set; }

//        public void OnGet()
//        {
//        }

//        public async Task<IActionResult> OnPost()
//        {
//            var api = new Data.Services.Base.Client(_httpClient);
//            return (IActionResult)await api.AuthenticateAsync(LoginRequest);
            
            
//            //if (result.IsCompletedSuccessfully)
//            //{
//            //    return RedirectToPage("/");
//            //}
//            //return Page();
//        }

    

//    }
//}

