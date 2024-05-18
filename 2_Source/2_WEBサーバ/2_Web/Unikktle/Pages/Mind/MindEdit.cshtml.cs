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
using Unikktle.Cache;
using Unikktle.Data;
using Unikktle.Models;
using Unikktle.Common;
using UnikktleCommon;


namespace Unikktle.Pages.Home
{
    public class MindEditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public MindEditModel(
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
        public InputModel_MindEdit Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //public MindViewModel MindViewModel { get; set; }

        public string PageTitle;

        public bool View_AllowOtherEdit { get; set; } = false;

        public async Task<IActionResult> OnGet()
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

                var previewMode = HttpContext.Session.Get<bool>(SessionKey.MindEditPreview);
                HttpContext.Session.Remove(SessionKey.MindEditPreview);

                if (previewMode)
                {
                    // プレビュー画面の「戻る」ボタンで戻って来た。

                    Input = HttpContext.Session.Get<InputModel_MindEdit>(SessionKey.MindEditInput);

                    return Page();
                }

                var mindEditMode = (MindEditMode)HttpContext.Session.GetInt32(SessionKey.MindEditMode);

                if (mindEditMode == MindEditMode.New)
                {
                    Input = new InputModel_MindEdit();


//#if DEBUG
//                    Input.Title = "Asp.Net Core 2.2 Webシステム";
//                    Input.Explanation = "Asp.Net Core 2.2 ベース WebシステムのVPSモデル。\r\n" +
//                                        "※WebサーバのSSH/SQLServerポートは、イントラのIPからのみ接続可能にし、更に、イントラ側のIPを動的にすることでセキュリティ強度を上げられる。";
//                    Input.Item = "1|サーバーホスティングサービス（VPS）\n" +
//                                 "2|Webサーバー\n" +
//                                 "3|CentOS 7.7\n" +
//                                 "4|.NET Core 2.2.7\n" +
//                                 "7|ASP.NET Core 2.2.7\n" +
//                                 "8|Webアプリ\n" +
//                                 "9|Html\n" +
//                                 "10|CSS\n" +
//                                 "11|JavaScript\n" +
//                                 "12|C#\n" +
//                                 "13|Java runtime (java-1.8.0-openjdk)\n" +
//                                 "14|WAF (SiteGuard Server Edition)\n" +
//                                 "15|SQLServer 2017\n" +
//                                 "16|ストアドプロシージャ (T-SQL)\n" +
//                                 "5|Nginx\n" +
//                                 "17|SSL証明書\n" +
//                                 "6|SSHサーバー\n" +
//                                 "100|ユーザー\n" +
//                                 "101|Webブラウザ\n" +
//                                 "102|メーラー\n" +
//                                 "200|イントラ\n" +
//                                 "201|Windows\n" +
//                                 "202|PuTTY\n" +
//                                 "203|WinSCP\n" +
//                                 "204|バッチ(C#)\n" +
//                                 "300|SMTPサーバー\n" +
//                                 "301|SendGrid\n" +
//                                 "400|SSL認証局\n" +
//                                 "401|Let's Encrypt";
//                    Input.ItemRelation = "2|>|1|\n" +
//                                         "3|>|2|\n" +
//                                         "4|>|3|\n" +
//                                         "5|>|3|\n" +
//                                         "6|>|3|\n" +
//                                         "7|>|4|\n" +
//                                         "8|>|7|\n" +
//                                         "9|>|8|\n" +
//                                         "10|>|8|\n" +
//                                         "11|>|8|\n" +
//                                         "12|>|8|\n" +
//                                         "13|>|3|\n" +
//                                         "14|>|13|\n" +
//                                         "15|>|3|\n" +
//                                         "16|>|15|\n" +
//                                         "17|>|5|\n" +
//                                         "101|>|100|\n" +
//                                         "102|>|100|\n" +
//                                         "201|>|200|\n" +
//                                         "202|>|201|\n" +
//                                         "203|>|201|\n" +
//                                         "204|>|201|\n" +
//                                         "301|>|300|\n" +
//                                         "401|>|400|\n" +
//                                         "5|-|8|リバースプロキシ\n" +
//                                         "5|-|14|リバースプロキシ\n" +
//                                         "101|-|5|https\n" +
//                                         "15|-|204|独自ポート\n" +
//                                         "6|-|202|独自ポート\n" +
//                                         "6|-|203|独自ポート\n" +
//                                         "12|-|16|Local接続\n" +
//                                         "301|-|12|SMTP\n" +
//                                         "301|-|102|POP\n" +
//                                         "401|-|17|http/https"
//                                         ;
//#endif

                    //PageTitle = "Create new Mind";
                    PageTitle = _sharedLocalizer["93"];

                    View_AllowOtherEdit = true;
                }
                else if (mindEditMode == MindEditMode.Edit || mindEditMode == MindEditMode.Duplication)
                {
                    var mind = ControlCommonMind.Mind(_dbContext, HttpContext.Session.Get<long>(SessionKey.MindNo));

                    Input = new InputModel_MindEdit
                    {
                        Title = mind.Title,
                        Explanation = mind.Explanation,
                        ItemRelation = mind.ItemRelation,
                        PublishOnlyToMe = mind.PublishOnlyToMe,
                        AllowOtherEdit = mind.AllowOtherEdit
                    };

                    if (mindEditMode == MindEditMode.Edit)
                    {
                        //PageTitle = "Edit Mind";
                        PageTitle = _sharedLocalizer["94"];

                        if (mind.UserNo == userNo)
                        {
                            View_AllowOtherEdit = true;
                        }
                    }
                    else 
                    {
                        // if (mindEditMode == MindEditMode.Duplication)

                        //PageTitle = "Duplication Mind";
                        PageTitle = _sharedLocalizer["95"];

                        View_AllowOtherEdit = true;
                    }
                }
                else
                {
                    return BadRequest();
                }


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

//                // 仮実装。Mind登録時に実行する。
//                MindViewModel.MindRowList = ControlCommonMind.UnikktileConvertToClass(unikktile);
//#endif

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
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

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                // WEBブラウザの「戻る」に備えて、このタイミングで入力値はセッションに記録しておく
                HttpContext.Session.Set(SessionKey.MindEditInput, Input);

                // 改行を変換しておく
                Input.Title = Input.Title.Replace("\r\n", "\n");
                Input.Explanation = Input.Explanation.Replace("\r\n", "\n");
                Input.ItemRelation = Input.ItemRelation.Replace("\r\n", "\n");

                ClassConvertMind.ClassConvert(
                    _dbContext, 
                    _sharedLocalizer,
                    Input.ItemRelation,
                    out List<MindRow_Text> textList,
                    out List<MindRow_Link> linkList,
                    out List<MindRow_Rect> rectList,
                    out List<MindRow_Line> lineList,
                    out string sentences,
                    out string error);

                if (!string.IsNullOrEmpty(error))
                {
                    StatusMessage = "Error " + error;

                    return Page();
                }

                //ControlCommonMind.CulcWidthHeight(textList, out int width, out int height);
                var width = textList.Max(x => x.X2) + 200;
                var height = textList.Max(x => x.Y2) + 200;

                var mindViewModel = new MindViewModel
                {
                    Mind = new Models.Mind()
                    {
                        // \r\n を \n に置換しないと、本番環境で \r も改行扱いになり、2行改行になる。
                        UserNo = userNo,
                        Title = Input.Title.Replace("\r\n", "\n"),
                        Explanation = Input.Explanation.Replace("\r\n", "\n"),
                        Item_SpaceSeparator = sentences,
                        ItemRelation = Input.ItemRelation.Replace("\r\n", "\n"),
                        PublishOnlyToMe = Input.PublishOnlyToMe,
                        AllowOtherEdit = Input.AllowOtherEdit,
                    },
                    SVG_Width = width,
                    SVG_Height = height,
                    TextList = textList,
                    LinkList = linkList,
                    RectList = rectList,
                    LineList = lineList,
                };

                HttpContext.Session.Set(SessionKey.MindEditPreview, true);
                HttpContext.Session.Set(SessionKey.MindViewModel, mindViewModel);

                return LocalRedirect("/Mind/Mind");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

        public IActionResult OnPostBack()
        {
            try
            {
                return LocalRedirect("/Mind/Mind?i=" + HttpContext.Session.Get<long>(SessionKey.MindNo));
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }
    }
}