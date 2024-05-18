using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public partial class AdverSearchEditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AdverSearchEditModel(
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
        public InputModel_AdverSearch Input { get; set; }
        //public InputModel_Adver Input { get; set; }

        public ViewModel_AdverEdit _ViewModel { get; set; }

        public string CategoryInitialStyle;

        // id: AdverId
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                Input = new InputModel_AdverSearch();
                //Input = new InputModel_Adver();

                var screenTransitionMode = (ScreenTransitionMode)HttpContext.Session.GetInt32(SessionKey.ScreenTransitionMode);

                if (screenTransitionMode == ScreenTransitionMode.新規登録)
                {
                    var businessNo = (short)HttpContext.Session.GetInt32(SessionKey.BusinessId);

                    _ViewModel = new ViewModel_AdverEdit()
                    {
                        BusinessId = businessNo,
                        Adver = new Adver(),
                        SearchWordList = new List<AdverWordSearch>()
                    };

                    HttpContext.Session.Set(SessionKey.ViewModel_AdverEdit, _ViewModel);
                }
                else if (screenTransitionMode == ScreenTransitionMode.編集開始)
                {
                    var businessNo = (short)HttpContext.Session.GetInt32(SessionKey.BusinessId);

                    var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                    if (no == null)
                    {
                        return BadRequest();
                    }
                    var userNo = (long)no;

                    _ViewModel = new ViewModel_AdverEdit()
                    {
                        BusinessId = businessNo,
                        Adver = SP_AdverSearch.SelectOne(_dbContext, userNo, businessNo, (int)id),
                        SearchWordList = SP_AdverSearchWord.Select(_dbContext, userNo, businessNo, (int)id)
                    };

                    HttpContext.Session.Set(SessionKey.ViewModel_AdverEdit, _ViewModel);
                }
                else if (screenTransitionMode == ScreenTransitionMode.編集中)
                {
                    _ViewModel = HttpContext.Session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);
                }
                else
                {
                    return BadRequest();
                }

                Input.Valid = _ViewModel.Adver.Valid;
                Input.Category = _ViewModel.Adver.Category;
                Input.AdverName = _ViewModel.Adver.AdverName;
                Input.AdverTitle1 = _ViewModel.Adver.AdverTitle1;
                Input.AdverTitle2 = _ViewModel.Adver.AdverTitle2;
                Input.AdverURL = _ViewModel.Adver.AdverURL;
                Input.AdvertisingBudget = _ViewModel.Adver.AdvertisingBudget;

                SetCategoryInitialStyle();

                return Page();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostSearchWordAddAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                if (IsValidWordCount(2) == false)
                {
                    return Page();
                }

                SessionCache.SetAdverSearchEdit_Input(HttpContext.Session, Input);
                //SessionCache.SetAdverEdit_Input(HttpContext.Session, Input);

                return LocalRedirect("/Identity/Account/Manage/AdverSearchWordEdit");
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
        public async Task<IActionResult> OnPostSearchWordEditAsync(long? id)
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                SessionCache.SetAdverSearchEdit_Input(HttpContext.Session, Input);
                //SessionCache.SetAdverEdit_Input(HttpContext.Session, Input);

                return LocalRedirect("/Identity/Account/Manage/AdverSearchWordEdit?id=" + id.ToString());
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        // id：編集対象の行Id
        public async Task<IActionResult> OnPostSearchWordDeleteAsync(int? id)
        {
            try
            {
                (bool bad, IdentityUser userId) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                var viewModel = SessionCache.SetAdverEdit_DeleteFlg_Search(HttpContext.Session, (int)id);

                HttpContext.Session.SetInt32(SessionKey.ScreenTransitionMode, (int)ScreenTransitionMode.編集中);

                return LocalRedirect("/Identity/Account/Manage/AdverSearchEdit?id=" + viewModel.Adver.Id.ToString());
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, "id: " + id, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                if (!ModelState.IsValid)
                {
                    _ViewModel = HttpContext.Session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);
                    return Page();
                }

                var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                if (no == null)
                {
                    return BadRequest();
                }
                var userNo = (long)no;

                _ViewModel = HttpContext.Session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);

                if (_ViewModel.Adver.Id == null)
                {
                    // 新規登録

                    if (Input.Category == AdverCategory.無料)
                    {
                        // 無料広告は最大数をチェック
                        // 無料広告は最大1つまで登録可。

                        if (SP_AdverSearch.Get無料Count(_dbContext, userNo) > 0)
                        {
                            // "無料広告は最大1つまで登録できます。既に1つ登録されています。"
                            ModelState.AddModelError(string.Empty, _sharedLocalizer["22"]);
                            return Page();
                        }
                    }
                    else
                    {
                        // 有料広告は保有Creditをチェック

                        if (SP_UserSetting.GetOwnedCredit(_dbContext, userNo) < 1 && Input.Valid == Valid.有効)
                        {
                            // "保有Creditが0以下です。有効な有料広告は登録できません。"
                            ModelState.AddModelError(string.Empty, _sharedLocalizer["21"]);
                            return Page();
                        }
                    }
                }
                else
                {
                    // 更新

                    if (Input.Category == AdverCategory.無料)
                    {
                        // 無料広告は最大数をチェック
                        // 無料広告は最大1つまで登録可。

                        if (SP_AdverSearch.Get無料Count_自分を除く(_dbContext, userNo, _ViewModel.BusinessId, (int)_ViewModel.Adver.Id) > 0)
                        {
                            // "無料広告は最大1つまで登録できます。既に1つ登録されています。"
                            ModelState.AddModelError(string.Empty, _sharedLocalizer["22"]);
                            return Page();
                        }
                    }
                    else
                    {
                        // 有料広告は保有Creditをチェック

                        if (SP_UserSetting.GetOwnedCredit(_dbContext, userNo) < 1 && Input.Valid == Valid.有効)
                        {
                            // "保有Creditが0以下です。有料広告を有効にできません。"
                            ModelState.AddModelError(string.Empty, _sharedLocalizer["23"]);
                            return Page();
                        }
                    }
                }

                if (MinWordCount() == false)
                {
                    return Page();
                }

                if (IsValidWordCount(3) == false)
                {
                    return Page();
                }

                int adverId;
                if (_ViewModel.Adver.Id == null)
                {
                    adverId = SP_AdverSearch.Insert(_dbContext,
                        userNo, _ViewModel.BusinessId, Input.Valid, Input.Category, 
                        Input.AdverName, Input.AdverTitle1, Input.AdverTitle2, Input.AdverURL, 
                        Input.AdvertisingBudget);

                    //StatusMessage = "登録が完了しました";
                    StatusMessage = _sharedLocalizer["10"];
                }
                else
                {
                    adverId = (int)_ViewModel.Adver.Id;
                    SP_AdverSearch.Update(_dbContext,
                        userNo, _ViewModel.BusinessId, adverId, Input.Valid, Input.Category, 
                        Input.AdverName, Input.AdverTitle1, Input.AdverTitle2, Input.AdverURL, 
                        Input.AdvertisingBudget);

                    // "編集が完了しました";
                    StatusMessage = _sharedLocalizer["11"];
                }

                foreach (var item in _ViewModel.SearchWordList)
                {
                    if (item.DelFlg == DelFlg.削除する)
                    {
                        SP_AdverSearchWord.Delete(_dbContext, userNo, _ViewModel.BusinessId, adverId, (long)item.WordId);
                    }
                    else
                    {
                        long WordId;
                        if (item.WordId == null)
                        {
                            WordId = SP_SearchWord.SearchWord_GetNoWithInsert(_dbContext, item.Word);
                        }
                        else
                        {
                            WordId = (long)item.WordId;
                        }

                        SP_AdverSearchWord.InsertUpdate(_dbContext, userNo, _ViewModel.BusinessId, adverId, WordId, item.ClickCost);
                    }
                }

                // 編集用のセッション変数を全て削除
                SessionCache.AdverAllRemove(HttpContext.Session);

                return LocalRedirect("/Identity/Account/Manage/Adver?idb="+ _ViewModel.BusinessId);
                //return LocalRedirect("/Identity/Account/Manage/Business");
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }
            finally
            {
                SetCategoryInitialStyle();
            }

            return BadRequest();
        }

        public IActionResult OnPostBack()
        {
            try
            {
                // 編集用のセッション変数を全て削除
                SessionCache.AdverAllRemove(HttpContext.Session);

                return LocalRedirect("/Identity/Account/Manage/Adver?idb=" +
                    HttpContext.Session.GetInt32(SessionKey.BusinessId));
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                MailNotify.ExceptionMail(_emailSender, null, ex);
                ModelState.AddModelError(string.Empty, _sharedLocalizer["1"]); // "エラーが発生しました。"
            }

            return BadRequest();
        }

        //private bool IsValid無料AdverCount(long userNo, int max)
        //{
        //    if (SP_AdverSearch.Get無料Count(_dbContext, userNo) > max)
        //    {
        // "無料広告は最大1つまで登録できます。既に1つ登録されています。"
        //  ModelState.AddModelError(string.Empty, _sharedLocalizer["22"]);

        //        return false;
        //    }

        //    return true;
        //}

        private void SetCategoryInitialStyle()
        {
            if (_ViewModel.Adver.Category == AdverCategory.無料)
            {
                CategoryInitialStyle = "style=display:none;";
            }
        }

        private bool MinWordCount()
        {
            _ViewModel = HttpContext.Session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);

            if (_ViewModel.SearchWordList.Count(x => x.DelFlg == DelFlg.削除しない) < 1)
            {
                // "検索ワードは1件以上登録する必要があります。"
                ModelState.AddModelError(string.Empty, _sharedLocalizer["33"]);
                return false;
            }

            return true;
        }

        private bool IsValidWordCount(int max)
        {
            _ViewModel = HttpContext.Session.Get<ViewModel_AdverEdit>(SessionKey.ViewModel_AdverEdit);

            if (Input.Category == AdverCategory.無料 && 
                _ViewModel.SearchWordList.Count(x => x.DelFlg == DelFlg.削除しない) > max)
            {
                // "無料広告に登録できるワードは3つまでです。"
                ModelState.AddModelError(string.Empty, _sharedLocalizer["31"]);
                return false;
            }

            return true;
        }

    }
}
