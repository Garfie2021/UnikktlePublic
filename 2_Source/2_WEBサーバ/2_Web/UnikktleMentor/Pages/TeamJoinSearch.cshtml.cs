using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using UnikktleMentor.Cache;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using UnikktleMentor.Common;
using UnikktleMentorEngine;
using UnikktleCommon;


namespace UnikktleMentor.Pages
{

    public class TeamJoinSearchModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TeamJoinSearchModel(
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

        public string TeamName { get; set; }


        [BindProperty]
        public InputModel_TeamEdit Input { get; set; }

        //public void OnGet()
        //{

        //}

        //public async Task<IActionResult> OnPostJoinIDAsync()
        //{
        //    try
        //    {
        //        (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
        //        if (bad)
        //        {
        //            return LocalRedirect("/Identity/Account/Login");
        //        }

        //        var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
        //        if (no == null)
        //        {
        //            return BadRequest();
        //        }
        //        var userNo = (long)no;


        //        return Page();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return BadRequest();
        //}

        //public async Task<IActionResult> OnPostSearchAsync(int Id)
        //{
        //    try
        //    {
        //        (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
        //        if (bad)
        //        {
        //            return LocalRedirect("/Identity/Account/Login");
        //        }

        //        var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
        //        if (no == null)
        //        {
        //            return BadRequest();
        //        }
        //        var userNo = (long)no;


        //        return Page();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return BadRequest();
        //}

        //public async Task<IActionResult> OnPostJoinAsync(int Id)
        //{
        //    try
        //    {
        //        (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
        //        if (bad)
        //        {
        //            return LocalRedirect("/Identity/Account/Login");
        //        }

        //        var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
        //        if (no == null)
        //        {
        //            return BadRequest();
        //        }
        //        var userNo = (long)no;


        //        return LocalRedirect("/Home");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return BadRequest();
        //}

        public IActionResult OnPostBack()
        {
            try
            {
                return LocalRedirect("/Home");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }

            return Page();
        }
    }
}
