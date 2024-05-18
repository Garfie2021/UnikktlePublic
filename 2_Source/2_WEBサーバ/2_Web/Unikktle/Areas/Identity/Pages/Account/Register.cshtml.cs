using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Unikktle.Cache;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using UnikktleCommon;

namespace Unikktle.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public RegisterModel(
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMemoryCache cache,
            IEmailSender emailSender,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _Cache = cache;
            _emailSender = emailSender;
            _sharedLocalizer = sharedLocalizer;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel_Register Input { get; set; }

        public string ReturnUrl { get; set; }

        public List<CareerCategory> CareerList { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            try
            {
                CareerList = InMemoryCache.CareerList(_dbContext, _Cache);

                ReturnUrl = returnUrl;

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "returnUrl: " + returnUrl, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "�G���[���������܂����B"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                //returnUrl = returnUrl ?? Url.Content("/");

                if (ModelState.IsValid)
                {
                    var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        // ���{��� => ���[�U�[�A�J�E���g�����o�^���܂����B�������N���b�N���ăA�J�E���g���m�F���Ă��������N���b�N����ƃA�J�E���g�̓o�^���������܂��B�N���b�N����ƃA�J�E���g�̓o�^���������܂��B�B
                        await _emailSender.SendEmailAsync(Input.Email,
                            // "xxx Confirm your email"
                            _sharedLocalizer["40"],
                            //"We have temporarily registered a user account.<br>" +
                            //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.<br>" +
                            //"Click to complete account registration.");
                            _sharedLocalizer["41", HtmlEncoder.Default.Encode(callbackUrl)]);

                        var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                        if (no == null)
                        {
                            return BadRequest();
                        }
                        var userNo = (long)no;

                        SP_UserSetting.Insert(_dbContext, userNo,
                            Input.Gender, Input.BirthDate, Input.Career,
                            HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("/Identity/Account/ConfirmEmailNotification");                    
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                // If we got this far, something failed, redisplay form
                
                CareerList = InMemoryCache.CareerList(_dbContext, _Cache);

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "returnUrl: " + returnUrl, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "�G���[���������܂����B"
            }

            return BadRequest();
        }

    }
}
