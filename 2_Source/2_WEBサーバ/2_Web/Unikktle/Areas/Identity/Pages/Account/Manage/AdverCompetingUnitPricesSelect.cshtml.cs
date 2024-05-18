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
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using UnikktleCommon;

namespace Unikktle.Areas.Identity.Pages.Account.Manage
{
    public partial class AdverCompetingUnitPricesSelectModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public AdverCompetingUnitPricesSelectModel(
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

        //public AdverCompetingUnitPrice AdverCompetingUnitPrice;

        public string Word;

        public List<UnitPrice> UnitPriceList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                if (bad)
                {
                    return LocalRedirect("/Identity/Account/Login");
                }

                //AdverCompetingUnitPrice = HttpContext.Session.Get<AdverCompetingUnitPrice>(SessionKey.AdverCompetingUnitPrice);
                //if (AdverCompetingUnitPrice == null)
                //{
                //    return BadRequest();
                //}

                //word = HttpContext.Session.GetStringAndRemove(SessionKey.AdverCompetingUnitPricesSelect_Word);

                var adverPageType = (AdverPageType)HttpContext.Session.GetInt32(SessionKey.AdverPageType);
                if (adverPageType == AdverPageType.Relation)
                {
                    // 関連ワード広告画面から遷移してきた。
                    var model = HttpContext.Session.Get<AdverWordRelation>(SessionKey.AdverRelationWordEdit);
                    Word = model.Word;
                    UnitPriceList = SP_AdverRelationWord.SelectCompetingUnitPrices(_dbContext, (long)model.WordId);
                }
                else
                {
                    // 検索ワード広告画面から遷移してきた。
                    Word = HttpContext.Session.Get<AdverWordRelation>(SessionKey.AdverSearchWordEdit).Word;
                    UnitPriceList = SP_AdverSearchWord.SelectCompetingUnitPrices(_dbContext, Word);
                }


                HttpContext.Session.SetInt32(SessionKey.ScreenTransitionMode_AdverCompetingUnitPricesSelect, 
                    (int)ScreenTransitionMode_AdverCompetingUnitPricesSelect.競合単価画面遷移);

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

        public IActionResult OnPostBack()
        {
            try
            {
                //AdverCompetingUnitPrice = HttpContext.Session.GetAndRemove<AdverCompetingUnitPrice>(SessionKey.AdverCompetingUnitPrice);
                //if (AdverCompetingUnitPrice == null)
                //{
                //    return BadRequest();
                //}

                var adverPageType = (AdverPageType)HttpContext.Session.GetInt32(SessionKey.AdverPageType);
                if (adverPageType == AdverPageType.Relation)
                {
                    return LocalRedirect("/Identity/Account/Manage/AdverRelationWordEdit");
                }
                else
                {
                    return LocalRedirect("/Identity/Account/Manage/AdverSearchWordEdit");
                }
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
