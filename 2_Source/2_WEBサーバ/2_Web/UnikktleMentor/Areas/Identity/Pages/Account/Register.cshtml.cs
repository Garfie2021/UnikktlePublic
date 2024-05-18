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
using UnikktleMentor.Cache;
using UnikktleMentor.Common;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using UnikktleCommon;

namespace UnikktleMentor.Areas.Identity.Pages.Account
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

        [BindProperty]
        public InputModel_Register Input { get; set; }

        public string ReturnUrl { get; set; }

        public List<CareerCategory> CareerList { get; set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            try
            {
                //Console.WriteLine("Test RegisterModel OnGet() 1");

                CareerList = InMemoryCache.GetCareerAndCategoryList(_dbContext, _Cache);

                ReturnUrl = returnUrl;

                //Console.WriteLine("Test RegisterModel OnGet() 2");

//#if DEBUG
//                Input = new InputModel_Register();
//                Input.LoginID = "xxx";
//                Input.Password = "xxx@";
//                Input.ConfirmPassword = "xxx@";
//                Input.Email = "xxx@xxx.com";
//                Input.BirthDate = DateTime.Parse("1979/12/20");
//                Input.Nickname = "�K�[�t�B�[";
//                Input.Career = 363;
//#endif

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

                //Console.WriteLine("Test RegisterModel OnPostAsync() 1");

                if (ModelState.IsValid)
                {
                    //Console.WriteLine("Test RegisterModel OnPostAsync() 2");

                    var user = new IdentityUser { UserName = Input.LoginID, Email = Input.Email };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        //Console.WriteLine("Test RegisterModel OnPostAsync() 3");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        //Console.WriteLine("Test RegisterModel OnPostAsync() 4");
                        //Console.WriteLine("Test RegisterModel OnPostAsync() callbackUrl: " + callbackUrl);

                        // ���{��� => ���[�U�[�A�J�E���g�����o�^���܂����B�������N���b�N���ăA�J�E���g���m�F���Ă��������N���b�N����ƃA�J�E���g�̓o�^���������܂��B�N���b�N����ƃA�J�E���g�̓o�^���������܂��B�B
                        await _emailSender.SendEmailAsync(Input.Email,
                            // "Mentor.xxx Confirm your email"
                            _sharedLocalizer["40"],
                            //"We have temporarily registered a user account.<br>" +
                            //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.<br>" +
                            //"Click to complete account registration.");
                            _sharedLocalizer["41", HtmlEncoder.Default.Encode(callbackUrl)]);

                        //Console.WriteLine("Test RegisterModel OnPostAsync() 5");

                        var userNo = SP_AspNetUsers.GetNo(_dbContext, user.Id);

                        //Console.WriteLine("Test RegisterModel OnPostAsync() 6");

                        SP_UserSetting.Insert(_dbContext, userNo, Input.Email,
                            Input.Nickname, Input.Gender, (DateTime)Input.BirthDate, Input.Career,
                            HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());

                        //Console.WriteLine("Test RegisterModel OnPostAsync() 7");

                        HttpContext.Session.Clear();  // �˂�̂���

                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("/Identity/Account/ConfirmEmailNotification");                    
                    }
                    foreach (var error in result.Errors)
                    {
                        //Console.WriteLine("Test RegisterModel OnPostAsync() 8");

                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                //Console.WriteLine("Test RegisterModel OnPostAsync() 9");

                // �ureturn Page();�v���邽�߂ɕK�v�B
                CareerList = InMemoryCache.GetCareerAndCategoryList(_dbContext, _Cache);

                //Console.WriteLine("Test RegisterModel OnPostAsync() 10");

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
