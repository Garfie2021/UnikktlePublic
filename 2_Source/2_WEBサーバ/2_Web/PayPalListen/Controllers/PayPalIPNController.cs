using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Identity.UI.Services;
using Unikktle.Common;
using PayPalListen.Common;
using PayPalListen.Data;
using PayPalListen.Models;
using Microsoft.Extensions.Logging;
using UnikktleCommon;

namespace PayPalListen.Controllers
{
    public class PayPalIPNController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public PayPalIPNController(
            ApplicationDbContext dbContext,
            IMemoryCache cache,
            IEmailSender emailSender,
            ILogger<ValuesController> logger)
        {
            _dbContext = dbContext;
            _emailSender = emailSender;
            _logger = logger;
        }


        // GET api/values
        // 
        // �e�X�g�ڑ��p�B�u���E�U�ɒl���\�������B
        // http://xxx/PayPalIPN/Get
        //[HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //Console.WriteLine("v1 v2");

            return new string[] { "v1", "v2" };
        }


        // PayPal���炭��IPN�������Ŏ󂯎��B
        // 
        // �J��
        // http://xxx/PayPalIPN/Receive
        // 
        // �{��
        // http://xxx:60003/PayPalIPN/Receive
        [HttpPost]
        public IActionResult Receive()
        {
            try
            {
                Console.WriteLine("PayPal IPN �����ʐM������");


                var ipnContext = new PayPalIPNContext()
                {
                    IPNRequest = Request
                };

                using(var reader = new StreamReader(ipnContext.IPNRequest.Body, Encoding.ASCII))
	            {  
		            ipnContext.RequestBody = reader.ReadToEnd();
	            }

                // �v���[���ȃf�[�^�����O�ɏo�́B
                //Store the IPN received from PayPal
                // Persist the request values into a database or temporary data store
                _logger.LogInformation(ipnContext.RequestBody);

                //Fire and forget verification task
                PayPalCommon.VerifyTask(_dbContext, _emailSender, ipnContext);

                //Reply back a 200 code
                return Ok();
            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
                //Capture exception for manual investigation
                MailNotify.ExceptionMail(_emailSender, null, ex);
            }

            return BadRequest();
        }

    }
}