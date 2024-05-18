using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnikktleMentor.Common;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace UnikktleMentor.Cache
{
    public static class SessionCache
    {

        // 未ログインを許容しない。
        public static long? GetUserNo(ApplicationDbContext dbContext,
           ISession session, IEmailSender emailSender,
           string userID)
        {
            // キャッシュ有無をチェック
            
            var userNo = session.GetString(SessionKey.UserNo);
            if (string.IsNullOrEmpty(userNo) == false)
            {
                // キャッシュが存在する場合はキャッシュから返す
                return long.Parse(userNo);
            }

            try
            {
                var no = SP_AspNetUsers.GetNo(dbContext, userID);
                session.SetString(SessionKey.UserNo, no.ToString());

                return no;
            }
            catch(Exception ex)
            {
                MailNotify.ExceptionMail(emailSender, "userID: " + userID, ex);
            }

            return null;
        }

        public static UserSetting GetUserSettingProfile(ISession session,
            ApplicationDbContext dbContext, IMemoryCache cache,
            UserManager<IdentityUser> userManager, IEmailSender emailSender, ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);

            var no = SessionCache.GetUserNo(dbContext, session, emailSender, userId);
            if (no == null)
            {
                return null;
            }
            var userNo = (long)no;

            return SP_UserSetting.SelectProfile(dbContext, userNo)[0];
        }

    }
}
