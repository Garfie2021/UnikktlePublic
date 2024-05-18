using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Unikktle.Common;
using UnikktleCommon;

namespace Unikktle.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ConfirmEmailModel(
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _sharedLocalizer = sharedLocalizer;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            try
            {
                if (userId == null || code == null)
                {
                    return BadRequest();
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    //return NotFound($"Unable to load user with ID '{userId}'.");
                    return BadRequest();
                }

                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (!result.Succeeded)
                {
                    //throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
                    return BadRequest();
                }

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "userId: " + userId + "<br>code: " + code, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "�G���[���������܂����B"
            }

            return BadRequest();
        }
    }
}
