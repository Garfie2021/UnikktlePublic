using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
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

namespace Unikktle.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public IndexModel(
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

        public string Username { get; set; }
        //public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel_Index Input { get; set; }

        public List<CareerCategory> CareerList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var userName = await _userManager.GetUserNameAsync(user);
                //var email = await _userManager.GetEmailAsync(user);
                //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

                Username = userName;

                var userSettingProfile = SessionCache.GetUserSettingProfile(
                    HttpContext.Session, _dbContext, _Cache, _userManager, _emailSender, User);

                Input = new InputModel_Index
                {
                    //Email = email,
                    //PhoneNumber = phoneNumber,
                    Gender = userSettingProfile.Gender,
                    BirthDate = userSettingProfile.BirthDate,
                    Career = userSettingProfile.Career
                };

                CareerList = InMemoryCache.CareerList(_dbContext, _Cache);

                //IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                //var email = await _userManager.GetEmailAsync(user);
                //if (Input.Email != email)
                //{
                //    var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                //    if (!setEmailResult.Succeeded)
                //    {
                //        var userId = await _userManager.GetUserIdAsync(user);
                //        throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                //    }
                //}

                //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
                //if (Input.PhoneNumber != phoneNumber)
                //{
                //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                //    if (!setPhoneResult.Succeeded)
                //    {
                //        var userId = await _userManager.GetUserIdAsync(user);
                //        throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                //    }
                //}

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                SP_UserSetting.UpdateProfile(_dbContext, userNo,
                    Input.Gender, Input.BirthDate, Input.Career);

                await _signInManager.RefreshSignInAsync(user);

                StatusMessage = _sharedLocalizer["14"]; // "Your profile has been updated"

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        //public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }


        //    var userId = await _userManager.GetUserIdAsync(user);
        //    var email = await _userManager.GetEmailAsync(user);
        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    var callbackUrl = Url.Page(
        //        "/Account/ConfirmEmail",
        //        pageHandler: null,
        //        values: new { userId = userId, code = code },
        //        protocol: Request.Scheme);
        //    await _emailSender.SendEmailAsync(
        //        email,
        //        "Confirm your email",
        //        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //    StatusMessage = "Verification email sent. Please check your email.";
        //    return RedirectToPage();
        //}
    }
}
