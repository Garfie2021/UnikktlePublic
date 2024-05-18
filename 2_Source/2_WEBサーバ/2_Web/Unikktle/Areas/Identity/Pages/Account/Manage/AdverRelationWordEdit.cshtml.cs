using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Unikktle.Cache;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using UnikktleCommon;

namespace Unikktle.Areas.Identity.Pages.Account.Manage
{
    public partial class AdverRelationWordEditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AdverRelationWordEditModel(
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _sharedLocalizer = sharedLocalizer;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel_AdverWordEdit Input { get; set; }

        // id：編集対象の行Id
        // rid：関連ワード選択画面で選択したワードのId
        // c：1ならAdverCompetingUnitPricesSelect.cshtmlから戻って来た。
        public async Task<IActionResult> OnGetAsync(int? id, long? r)
        {
            try
            {
                (bool bad, IdentityUser user) auth = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (auth.bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                Input = new InputModel_AdverWordEdit();

                var screenTransitionMode_Tmp = HttpContext.Session.GetIntAndRemove(SessionKey.ScreenTransitionMode_AdverCompetingUnitPricesSelect);
                if (screenTransitionMode_Tmp != null)
                {
                    if ((ScreenTransitionMode_AdverCompetingUnitPricesSelect)screenTransitionMode_Tmp == 
                        ScreenTransitionMode_AdverCompetingUnitPricesSelect.競合単価画面遷移)
                    {
                        // 競合単価画面から戻って来た

                        var adverWord = HttpContext.Session.Get<AdverWordRelation>(SessionKey.AdverRelationWordEdit);
                        Input.Word = adverWord.Word;
                        Input.ClickCost = adverWord.ClickCost;

                        return Page();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                if (id == null && r == null)
                {
                    // 新規登録
                    // 広告設定 Top(AdverEdit.cshtml)から遷移して来た場合

                    HttpContext.Session.Set(SessionKey.AdverRelationWordEdit, new AdverWordRelation());
                }
                else if (id != null)
                {
                    // 編集開始と見なす
                    // 広告設定 Top画面（AdverEdit.cshtml）から遷移して来た場合。

                    var adverWord = HttpContext.Session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit)
                        .RelationWordList.First(x => x.Id == id);

                    HttpContext.Session.Set(SessionKey.AdverRelationWordEdit, adverWord);

                    Input.Word = adverWord.Word;
                    Input.ClickCost = adverWord.ClickCost;
                }
                else if (r != null)
                {
                    // 編集中 if(rid != null)
                    // 関連ワード選択画面から遷移して来た場合

                    var adverWord = HttpContext.Session.Get<AdverWordRelation>(SessionKey.AdverRelationWordEdit);
                    adverWord.WordId = r;
                    adverWord.Word = SP_CollectKeyword.GetWord(_dbContext, (long)adverWord.WordId);
                    HttpContext.Session.Set(SessionKey.AdverRelationWordEdit, adverWord);

                    Input.Word = adverWord.Word;
                    Input.ClickCost = adverWord.ClickCost;
                }

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id + "<br>rid: " + r, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostRelationWordSelectAsync()
        {
            try
            {
                (bool bad, IdentityUser user) auth = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (auth.bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var adverWord = HttpContext.Session.Get<AdverWordRelation>(SessionKey.AdverRelationWordEdit);
                adverWord.ClickCost = Input.ClickCost;
                HttpContext.Session.Set(SessionKey.AdverRelationWordEdit, adverWord);

                return LocalRedirect("/Identity/Account/Manage/AdverRelationWordSelect");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostCompetingUnitPricesSelectAsync()
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

                var adverWord = HttpContext.Session.Get<AdverWordRelation>(SessionKey.AdverRelationWordEdit);
                adverWord.ClickCost = Input.ClickCost;
                HttpContext.Session.Set(SessionKey.AdverRelationWordEdit, adverWord);

                HttpContext.Session.SetInt32(SessionKey.AdverPageType, (int)AdverPageType.Relation);

                return LocalRedirect("/Identity/Account/Manage/AdverCompetingUnitPricesSelect");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        // id：編集対象の行Id
        public async Task<IActionResult> OnPostAsync(int? id)
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

                var viewModel = HttpContext.Session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);
                var adverWord = HttpContext.Session.Get<AdverWordRelation>(SessionKey.AdverRelationWordEdit);

                if (adverWord.Id == null)
                {
                    // 追加

                    if (viewModel.RelationWordList.Any(x => x.WordId == adverWord.WordId))
                    {
                        // "同じワードが既に登録されています。"
                        ModelState.AddModelError(string.Empty, _sharedLocalizer["32"]);
                        return Page();
                    }

                    //adverWord.WordId = long.Parse(HttpContext.Session.GetString(SessionKey.AdverRelationWordSelect_rid));
                    //adverWord.Word = Input.Word;
                    adverWord.ClickCost = Input.ClickCost;  // クリック単価だけ直接入力できるのでInputをコピーする。

                    viewModel.RelationWordList.Add(adverWord);
                }
                else
                {
                    // 更新

                    if (viewModel.RelationWordList.Any(x => x.Id != adverWord.Id && x.WordId == adverWord.WordId))
                    {
                        // "同じワードが既に登録されています。"
                        ModelState.AddModelError(string.Empty, _sharedLocalizer["32"]);
                        return Page();
                    }

                    var row = viewModel.RelationWordList.First(x => x.Id == adverWord.Id);
                    
                    //var rid = HttpContext.Session.GetString(SessionKey.AdverRelationWordSelect_rid);
                    //if (!string.IsNullOrEmpty(adverWord.WordId))
                    //{
                    //    row.WordId = adverWord.WordId;
                    //}
                    row.WordId = adverWord.WordId;
                    row.Word = adverWord.Word;
                    row.ClickCost = Input.ClickCost;  // クリック単価だけ直接入力できるのでInputをコピーする。
                }

                HttpContext.Session.Set(SessionKey.ViewModel_AdverEdit, viewModel);

                HttpContext.Session.SetInt32(SessionKey.ScreenTransitionMode, (int)ScreenTransitionMode.編集中);

                HttpContext.Session.Remove(SessionKey.AdverRelationWordEdit);

                return LocalRedirect("/Identity/Account/Manage/AdverRelationEdit?id=" + viewModel.Adver.Id);
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public IActionResult OnPostBack()
        {
            try
            {
                HttpContext.Session.SetInt32(SessionKey.ScreenTransitionMode, (int)ScreenTransitionMode.編集中);

                HttpContext.Session.Remove(SessionKey.AdverRelationWordEdit);

                return LocalRedirect("/Identity/Account/Manage/AdverRelationEdit");
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
