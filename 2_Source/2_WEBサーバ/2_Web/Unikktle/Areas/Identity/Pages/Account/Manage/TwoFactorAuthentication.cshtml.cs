//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text.Encodings.Web;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Unikktle.Common;
//using Unikktle.Data;
//using Unikktle.Models;

//namespace Unikktle.Areas.Identity.Pages.Account.Manage
//{
//    public class TwoFactorAuthenticationModel : PageModel
//    {
//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        public bool HasAuthenticator { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        public int RecoveryCodesLeft { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        [BindProperty]
//        public bool Is2faEnabled { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        public bool IsMachineRemembered { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        [TempData]
//        public string StatusMessage { get; set; }

//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;

//        public TwoFactorAuthenticationModel(
//            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }

//        public async Task<IActionResult> OnGetAsync()
//        {

//            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
//            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
//            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
//            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {

//            await _signInManager.ForgetTwoFactorClientAsync();
//            StatusMessage = "The current browser has been forgotten. When you login again from this browser you will be prompted for your 2fa code.";
//            return RedirectToPage();
//        }
//    }
//}
