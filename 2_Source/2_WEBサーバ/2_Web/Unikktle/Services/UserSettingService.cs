using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using Unikktle.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity.UI.Services;
using UnikktleCommon;

namespace Unikktle.Services
{
    public class UserSettingService //: Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;

        public UserSettingService(
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMemoryCache cache,
            IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _Cache = cache;
            _emailSender = emailSender;
        }

        public BackgroundColor BackgroundColor(ClaimsPrincipal user, ISession session)
        {
            try
            {
                if (!_signInManager.IsSignedIn(user))
                {
                    return Common.BackgroundColor.White;
                }

                return SessionCache.GetBackgroundColor(session,
                    _dbContext, _Cache, _emailSender, _userManager, user);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "user: " + user.ToString() + "<br>session: " + session, ex);
            }

            return Common.BackgroundColor.White;
        }
    }
}
