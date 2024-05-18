using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Unikktle.Common;
using Unikktle.Data;
using Unikktle.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;
using Unikktle.Cache;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading;
using UnikktleCommon;

namespace Unikktle.Controllers
{
    public class PayPalController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;

        public PayPalController(
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

        // 規定メソッド
        // 
        // URL
        // http://localhost:64829/PayPal/Buy?amt=1000&cc=JPY&item_name=Credit&item_number=xxx&st=Completed&tx=xxx
        // https://xxx/PayPal/Buy?amt=1000&cc=JPY&item_name=Credit&item_number=xxx&st=Completed&tx=xxx
        // 
        // amt = 1000 & 
        // cc = JPY & 
        // item_name = Credit & 
        // item_number = xxx & 
        // st = Completed & 
        // tx = 9GS413983Y164064V
        // 
        // https://localhost:44376/PayPal/Buy
        public async Task<IActionResult> Buy(int amt, string cc, string item_name, string item_number, string st, string tx)
        {
            try
            {
                var userNo = await ControlCommon.GetUserNo(_dbContext, _userManager, User,
                    HttpContext.Session, _emailSender);

                //Console.WriteLine("PayPal 完了　1");

                if (userNo == -1)
                {
                    //Console.WriteLine("PayPal 完了　2");
                    return BadRequest();
                }

                //Console.WriteLine("PayPal 完了　3");

                // GET パラメータチェック
                if (item_name != "Credit")
                {
                    //Console.WriteLine("PayPal 完了　4");
                    return BadRequest();
                }

                //Console.WriteLine("PayPal 完了　5");
                if (item_number != "xxx")
                {
                    //Console.WriteLine("PayPal 完了　6");
                    return BadRequest();
                }

                //Console.WriteLine("PayPal 完了　7");
                if (cc != "JPY")
                {
                    //Console.WriteLine("PayPal 完了　8");
                    return BadRequest();
                }

                //Console.WriteLine("PayPal 完了　9");
                var model = new BuyViewModel()
                {
                    Credit = amt
                };

                //Console.WriteLine("PayPal 完了　10");
                // 状態チェック
                if (st != "Completed")
                {
                    model.PayPalBuyStatus = PayPalBuyStatus.NotCompleted;

                    //Console.WriteLine("PayPal 完了　11");
                    return View(model);
                }

                // IPN処理状態チェック

                var cnt = 0;
                for (int retryCnt = 0; retryCnt < 300 ; retryCnt++) 
                {
                    cnt = SP_CreditHistory.GetCnt(_dbContext, userNo, tx);

                    if (cnt > 0)
                    {
                        // PayPalListen Webサーバが、IPN受信処理を終え、それを確認したら抜ける。
                        break;
                    }

                    // 最大5分間リトライする。
                    Thread.Sleep(1000);
                }

                //Console.WriteLine("PayPal 完了　12");
                if (cnt < 1)
                {
                    // model.PayPalBuyStatus = PayPalBuyStatus.IPN_Fail;

                    // 異常なHttpリクエストのはず。
                    //Console.WriteLine("PayPal 完了　13");
                    return BadRequest();
                }

                model.PayPalBuyStatus = PayPalBuyStatus.Completed;

                //Console.WriteLine("PayPal 完了　14");
                return View(model);
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
