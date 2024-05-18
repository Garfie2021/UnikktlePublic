//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Drawing;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Text;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Unikktle.Common;
//using Unikktle.Data;
//using Unikktle.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Localization;
//using Microsoft.Extensions.Caching.Memory;
//using Unikktle.Cache;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using System.IO;
//using System.Net;

//namespace Unikktle.Controllers
//{
//    public class PayPalIPNContext
//    {
//        public HttpRequest IPNRequest { get; set; }

//        public string RequestBody { get; set; }

//        public string Verification { get; set; } = String.Empty;
//    }

//    public class PayPalIPNController : Controller
//    {
//        private readonly ApplicationDbContext _dbContext;
//        private readonly IMemoryCache _Cache;
//        private readonly IEmailSender _emailSender;

//        public PayPalIPNController(
//            ApplicationDbContext dbContext,
//            IMemoryCache cache,
//            IEmailSender emailSender)
//        {
//            _dbContext = dbContext;
//            _Cache = cache;
//            _emailSender = emailSender;
//        }


//        //public IActionResult Index()
//        //{
//        //    try
//        //    {
//        //        return View();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        MailNotify.ExceptionMail(_emailSender, null, ex);
//        //    }
//        //    return BadRequest();
//        //}


//        // PayPal���炭��IPN�������Ŏ󂯎��B
//        // 
//        // �J��
//        // https://localhost:44376/PayPalIPN/Receive
//        // 
//        // �{��
//        // https://xxx/PayPalIPN/Receive
//        [HttpPost]
//        public IActionResult Receive()
//        {
//            try
//            {
//                //_emailSender.SendEmailAsync("xxx@xxx.com",�@"[Unikktle] PayPalIPN/Receive ����",�@"");

//                var ipnContext = new PayPalIPNContext()
//                {
//                    IPNRequest = Request
//                };

//                using(var reader = new StreamReader(ipnContext.IPNRequest.Body, Encoding.ASCII))
//	            {  
//		            ipnContext.RequestBody = reader.ReadToEnd();
//	            }

//                //Store the IPN received from PayPal
//                // Persist the request values into a database or temporary data store
//                SP_PayPalIPNlog.Insert(_dbContext, ipnContext.RequestBody);

//                //Fire and forget verification task
//                Task.Run(() =>  VerifyTask(ipnContext));

//                //Reply back a 200 code
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                //Capture exception for manual investigation
//                MailNotify.ExceptionMail(_emailSender, null, ex);
//            }

//            return BadRequest();
//        }

//        private void VerifyTask(PayPalIPNContext ipnContext)
//        {
//            try
//            {
//                var verificationRequest = WebRequest.Create("https://www.sandbox.paypal.com/cgi-bin/webscr");

//                //Set values for the verification request
//                verificationRequest.Method = "POST";
//                verificationRequest.ContentType = "application/x-www-form-urlencoded";

//                //Add cmd=_notify-validate to the payload
//                var strRequest = "cmd=_notify-validate&" + ipnContext.RequestBody;
//                verificationRequest.ContentLength = strRequest.Length;

//                //Attach payload to the verification request
//                using(var writer = new StreamWriter(verificationRequest.GetRequestStream(), Encoding.ASCII))
//                {
//                    writer.Write(strRequest);
//                }

//                //Send the request to PayPal and get the response
//                using(var reader = new StreamReader(verificationRequest.GetResponse().GetResponseStream()))
//                {
//                    ipnContext.Verification = reader.ReadToEnd();
//                }

//                ProcessVerificationResponse(ipnContext);
//            }
//            catch (Exception ex)
//            {
//                //Capture exception for manual investigation
//                MailNotify.ExceptionMail(_emailSender, "ipnContext: " + ipnContext.ToString(), ex);
//            }
//        }

//        private void ProcessVerificationResponse(PayPalIPNContext ipnContext)
//        {
//            if (ipnContext.Verification.Equals("VERIFIED"))
//            {
//                // check that Payment_status=Completed
//                // check that Txn_id has not been previously processed
//                // check that Receiver_email is your Primary PayPal email
//                // check that Payment_amount/Payment_currency are correct
//                // process payment
//            }
//            else if (ipnContext.Verification.Equals("INVALID"))
//            {
//                //Log for manual investigation
//            }
//            else
//            {
//                //Log error
//            }

//            SP_PayPalCreditPurchaseHistory.Insert(_dbContext, 0, 1000);
//            SP_UserSetting.UpdateOwnedCredit(_dbContext, 0, 1000);
//        }
//    }
//}