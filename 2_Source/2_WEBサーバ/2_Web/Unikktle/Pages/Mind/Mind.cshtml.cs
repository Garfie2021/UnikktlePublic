using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json;
using Unikktle.Cache;
using Unikktle.Data;
using Unikktle.Models;
using Unikktle.Common;
using Microsoft.Extensions.Localization;
using UnikktleCommon;

namespace Unikktle.Pages.Home
{

    public class MindModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public MindModel(
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



        public MindViewModel MindViewModel { get; set; }

        public bool Logined { get; set; } = false;
        public bool PreviewMode { get; set; }
        public bool Author { get; set; } = false;


        // http://localhost:60569/Home/Mind/1
        // 変数名は「No」禁止。「Id」じゃないとnullになる。
        // i：id
        // HTTP GET
        public async Task<IActionResult> OnGet(long? i)
        {
            try
            {
                if (i == null)
                {
                    PreviewMode = HttpContext.Session.Get<bool>(SessionKey.MindEditPreview);

                    if (PreviewMode)
                    {
                        // Mindのプレビュー。


                        (bool bad2, IdentityUser user2) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                        if (bad2)
                        {
                            return LocalRedirect("/Identity/Account/Login");
                        }

                        var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user2.Id);
                        if (no == null)
                        {
                            return BadRequest();
                        }
                        var userNo2 = (long)no;

                        MindViewModel = HttpContext.Session.Get<MindViewModel>(SessionKey.MindViewModel);

                        StatusMessage = _sharedLocalizer["115"];    // これはプレビュー画面です。

                        return Page();
                    }
                    else
                    {
                        // Mindの参照でも、Mindのプレビューでもない。
                        return BadRequest();
                    }
                }

                //// Mind 閲覧 ////

                // 遷移元URLを保持しとく
                HttpContext.Session.SetString(SessionKey.Referer, Request.GetTypedHeaders().Referer.PathAndQuery);

                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                Logined = !bad;

                long userNo = -1;
                if (Logined)
                {
                    var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                    if (no == null)
                    {
                        return BadRequest();
                    }
                    userNo = (long)no;
                }

                var mindNo = (long)i;

                MindViewModel = JsonConvert.DeserializeObject<MindViewModel>(ControlCommonMind.MindJson(_dbContext, mindNo));

                if (MindViewModel.Mind.PublishOnlyToMe && MindViewModel.Mind.UserNo != userNo)
                {
                    // 自分にだけ公開しているMindが、URL直指定で表示できてしまう問題に対処。
                    return BadRequest();
                }

                if (MindViewModel.Mind.UserNo == userNo)
                {
                    Author = true;
                }

                HttpContext.Session.Set(SessionKey.MindNo, mindNo);
                HttpContext.Session.SetString(SessionKey.HeaderTitile, _sharedLocalizer["113", MindViewModel.Mind.Title]);
                
                StatusMessage = HttpContext.Session.GetString(SessionKey.StatusMessage);
                HttpContext.Session.Remove(SessionKey.StatusMessage);

                //#if DEBUG
                //                // テストデータ

                //                var unikktile =
                //                    "Rect|1|10|20|100|50|#ffffa3|orange|5\n" +
                //                    "Line|2|20|50|50|100|orange|5\n" +
                //                    "Text|3|30|80|VL Gothic|10|white|テキスト\n" +
                //                    "Link|4|40|120|VL Gothic|10|white|リンク|http://e.com\n";

                //                MindViewModel.Mind = new Unikktle.Models.Mind()
                //                {
                //                    Unikktile = unikktile
                //                };
                //// 仮実装。Mind登録時に実行する。
                //MindViewModel.MindRowList = ControlCommonMind.UnikktileConvertToClass(MindViewModel.Mind.Unikktile);
                //#endif


                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                Console.WriteLine(ex.Message);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostNewAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                HttpContext.Session.SetInt32(SessionKey.MindEditMode, (int)MindEditMode.New);
                
                // 初期化
                HttpContext.Session.Remove(SessionKey.MindEditInput);

                return LocalRedirect("/Mind/MindEdit");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                HttpContext.Session.SetInt32(SessionKey.MindEditMode, (int)MindEditMode.Edit);

                return LocalRedirect("/Mind/MindEdit");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostDuplicatAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                HttpContext.Session.SetInt32(SessionKey.MindEditMode, (int)MindEditMode.Duplication);

                return LocalRedirect("/Mind/MindEdit");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                var mindNo = HttpContext.Session.Get<long>(SessionKey.MindNo);
                
                SP_Mind.Get_UserNo(_dbContext, mindNo, out long AuthorUserNo, out bool AllowOtherEdit);
                if (AllowOtherEdit)
                {
                    if (userNo != AuthorUserNo)
                    {
                        return BadRequest();
                    }
                }

                SP_Mind.Delete(_dbContext, mindNo);

                HttpContext.Session.Remove(SessionKey.MindNo);

                HttpContext.Session.SetString(SessionKey.StatusMessage, "削除が完了しました");

                return LocalRedirect("/Home/Status");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostRegistAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;
                
                HttpContext.Session.Remove(SessionKey.MindEditPreview);// 不要になった。

                MindViewModel = HttpContext.Session.Get<MindViewModel>(SessionKey.MindViewModel);

                var mindEditMode = (MindEditMode)HttpContext.Session.GetInt32(SessionKey.MindEditMode);

                if (mindEditMode == MindEditMode.New || mindEditMode == MindEditMode.Duplication)
                {
                    var mindNo = SP_Mind.Insert(_dbContext, userNo, 
                        MindViewModel.Mind.Title, 
                        MindViewModel.Mind.Explanation,
                        MindViewModel.Mind.ItemRelation.Replace('|', ' '),  // セパレータを全文検索用に置換。
                        MindViewModel.Mind.ItemRelation,
                        MindViewModel.Mind.PublishOnlyToMe,
                        MindViewModel.Mind.AllowOtherEdit,
                        JsonConvert.SerializeObject(MindViewModel));

                    HttpContext.Session.SetString(SessionKey.StatusMessage, "登録が完了しました");

                    return LocalRedirect("/Mind/Mind?i=" + mindNo);
                }
                else if (mindEditMode == MindEditMode.Edit)
                {
                    var mindNo = HttpContext.Session.Get<long>(SessionKey.MindNo);

                    SP_Mind.Get_UserNo(_dbContext, mindNo, out long AuthorUserNo, out bool AllowOtherEdit);

                    if (AllowOtherEdit == false)
                    {
                        if (userNo != AuthorUserNo)
                        {
                            return BadRequest();
                        }
                    }

                    SP_Mind.Update(_dbContext, mindNo, 
                        MindViewModel.Mind.Title,
                        MindViewModel.Mind.Explanation,
                        MindViewModel.Mind.Item_SpaceSeparator,
                        MindViewModel.Mind.ItemRelation,
                        MindViewModel.Mind.PublishOnlyToMe,
                        MindViewModel.Mind.AllowOtherEdit,
                        JsonConvert.SerializeObject(MindViewModel));

                    HttpContext.Session.SetString(SessionKey.StatusMessage, "更新が完了しました");

                    HttpContext.Session.Remove(SessionKey.MindViewModel);
                    HttpContext.Session.Remove(SessionKey.MindEditInput);
                    HttpContext.Session.Remove(SessionKey.MindEditPreview);

                    return LocalRedirect("/Mind/Mind?i=" + mindNo);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }
            finally
            {
                // 入力値のキャッシュをクリア
                HttpContext.Session.Remove(SessionKey.MindEditInput);
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostBackAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;


                //HttpContext.Session.Remove(SessionKey.MindEditPreview);
                //HttpContext.Session.Remove(SessionKey.MindEditInput);

                PreviewMode = HttpContext.Session.Get<bool>(SessionKey.MindEditPreview);

                if (PreviewMode)
                {
                    // Mindのプレビュー。
                    return LocalRedirect("/Mind/MindEdit");
                }
                else
                {
                    // 遷移元の画面に戻す
                    return LocalRedirect(HttpContext.Session.GetString(SessionKey.Referer));
                }

            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

    }
}