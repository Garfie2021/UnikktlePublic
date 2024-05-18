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
using UnikktleMentor.Cache;
using UnikktleMentor.Data;
using UnikktleMentor.Models;
using UnikktleMentor.Common;
using UnikktleMentorEngine;
using UnikktleCommon;


namespace UnikktleMentor.Pages.Mind
{
    public class Diagnosis : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMemoryCache _Cache;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public Diagnosis(
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


        public C01�e�_�v�Z_���� C01����;
        public C02�n���l�v�Z_���� C02����;
        public C03���q���__���� C03���q����;
        public C03�n������_���� C03�n������;
        public C04��b���q����_���� C04����;
        public C05��b���q���Z����_���� C05����;
        public C06�֘A���q����_���� C06����;
        public C07�W�����q����_���� C07����;
        public C08�ތ^�ʏW�����q����_���� C08����;
        public C09�m�C���[�[���q����_���� C09����;
        public C10���[�_�[��������_���� C10����;
        public C11�E��ʓK��������_���� C11����;
        public string �f�f����;
        public string �G���[;

        public UserSetting _UserSettingProfile;
        public List<string> _RaderJS_Data { get; set; }

        // id :  AnswerId�B�f�f����ID�B
        // id2 : user.Id�B�f�f���󂯂����[�U��ID�BTeamView.cshtml���珊�������o�̐f�f������ۂɎg����B
        public async Task<IActionResult> OnGet(int? id, long? id2)
        {
            try
            {
                //Console.WriteLine("Diagnosis OnGet 1");

                if (id == null)
                {
                    //Console.WriteLine("Diagnosis OnGet 2");
                    // �����O�C��

                    C01���� = HttpContext.Session.Get<C01�e�_�v�Z_����>(SessionKey.C01����);
                    C02���� = HttpContext.Session.Get<C02�n���l�v�Z_����>(SessionKey.C02����);
                    C04���� = HttpContext.Session.Get<C04��b���q����_����>(SessionKey.C04����);
                    C07���� = HttpContext.Session.Get<C07�W�����q����_����>(SessionKey.C07����);
                    C09���� = HttpContext.Session.Get<C09�m�C���[�[���q����_����>(SessionKey.C09����);
                    C10���� = HttpContext.Session.Get<C10���[�_�[��������_����>(SessionKey.C10����);

                    //Console.WriteLine("Diagnosis OnGet 3");
                    if (C01���� == null || C02���� == null || C04���� == null || C07���� == null ||
                        C09���� == null || C10���� == null)
                    {
                        //Console.WriteLine("Diagnosis OnGet 4");
                        // URL���ڎw�肩��̑J�ڎ��B�񓚂���Ă��Ȃ��̂Ńg�b�v��ʂɑJ�ڂ���B
                        return LocalRedirect("/");
                    }

                    //Console.WriteLine("Diagnosis OnGet 5");
                    _RaderJS_Data = new List<string>();
                    _RaderJS_Data.Add("{ " + $"'label': '', 'data': [{C02����.�W���_D_�}����}, {C02����.�W���_C_�C���̕ω�}, {C02����.�W���_I_�򓙊�}, {C02����.�W���_N_�_�o��}, {C02����.�W���_O_��ϐ�}, {C02����.�W���_Co_������}, {C02����.�W���_Ag_�U����}, {C02����.�W���_G_������}, {C02����.�W���_R_�̂�C}, {C02����.�W���_T_�v�l��}, {C02����.�W���_A_�x�z��}, {C02����.�W���_S_�Љ}], 'fill': true, 'backgroundColor': 'rgba(" + WebColor.RGB_JS[0] + ", 0.2)', 'borderColor': 'rgb(" + WebColor.RGB_JS[0] + ")', 'pointBackgroundColor': 'rgb(" + WebColor.RGB_JS[0] + ")', 'pointBorderColor': '#fff', 'pointHoverBackgroundColor': '#fff', 'pointHoverBorderColor': 'rgb(" + WebColor.RGB_JS[0] + ")'" + " },");
                }
                else
                {
                    // ���O�C���ς�

                    //Console.WriteLine("Diagnosis OnGet 6");
                    (bool bad, IdentityUser user) = await ControlCommon.AuthenticatBadAsync(_userManager, User);
                    if (bad)
                    {
                        //Console.WriteLine("Diagnosis OnGet 7");
                        return LocalRedirect("/Identity/Account/Login");
                    }

                    var no = SessionCache.GetUserNo(_dbContext, HttpContext.Session, _emailSender, user.Id);
                    if (no == null)
                    {
                        //Console.WriteLine("Diagnosis OnGet 8");
                        throw new Exception("���肦�Ȃ�");
                    }

                    //Console.WriteLine("Diagnosis OnGet 9");
                    var fromTeamView = (Request.Headers["Referer"].ToString().IndexOf("/TeamViewUser?") > -1);
                    List<AnswerDetail> answerSelect;
                    long userNo;

                    if (fromTeamView && id2 == null)
                    {
                        //Console.WriteLine("Diagnosis OnGet 10");
                        // TeamView��ʂ���J�ڂ��ė��āA�f�f�Ώێ�ID�iid2�j��null�̃P�[�X�͂��肦�Ȃ��B
                        throw new Exception("���肦�Ȃ�");
                    }
                    else if (fromTeamView)
                    {
                        //Console.WriteLine("Diagnosis OnGet 11");
                        // TeamView��ʂ���J�ڂ��ė����ꍇ�AuserNo�𓮓I�ɕς���B
                        userNo = (long)id2;
                    }
                    else
                    {
                        //Console.WriteLine("Diagnosis OnGet 12");
                        // �f�t�H���g�l�́A���O�C�����[�U�[��userNo�B
                        userNo = (long)no;
                    }

                    //Console.WriteLine("Diagnosis OnGet 13");
                    answerSelect = SP_AnswerDetail.Select(_dbContext, userNo, (int)id);

                    //Console.WriteLine("Diagnosis OnGet 13-1");
                    CharacterDiagnosis.Diagnosis(Gender.Male, answerSelect,
                        out C01����,
                        out C02����,
                        out C03���q����,
                        out C03�n������,
                        out C04����,
                        out C05����,
                        out C06����,
                        out C07����,
                        out C08����,
                        out C09����,
                        out C10����,
                        out C11����,
                        out �f�f����,
                        out �G���[);

                    //Console.WriteLine("Diagnosis OnGet 14");
                    _RaderJS_Data = new List<string>();
                    _RaderJS_Data.Add("{ " + $"'label': '', 'data': [{C02����.�W���_D_�}����}, {C02����.�W���_C_�C���̕ω�}, {C02����.�W���_I_�򓙊�}, {C02����.�W���_N_�_�o��}, {C02����.�W���_O_��ϐ�}, {C02����.�W���_Co_������}, {C02����.�W���_Ag_�U����}, {C02����.�W���_G_������}, {C02����.�W���_R_�̂�C}, {C02����.�W���_T_�v�l��}, {C02����.�W���_A_�x�z��}, {C02����.�W���_S_�Љ}], 'fill': true, 'backgroundColor': 'rgba(" + WebColor.RGB_JS[0] + ", 0.2)', 'borderColor': 'rgb(" + WebColor.RGB_JS[0] + ")', 'pointBackgroundColor': 'rgb(" + WebColor.RGB_JS[0] + ")', 'pointBorderColor': '#fff', 'pointHoverBackgroundColor': '#fff', 'pointHoverBorderColor': 'rgb(" + WebColor.RGB_JS[0] + ")'" + " },");

                    //Console.WriteLine("Diagnosis OnGet 15");
                    _UserSettingProfile = SessionCache.GetUserSettingProfile(
                        HttpContext.Session, _dbContext, _Cache, _userManager, _emailSender, User);
                }

                //Console.WriteLine("Diagnosis OnGet 16");
                return Page();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Diagnosis OnGet 17");
                ExceptionSt.ExceptionCommon(ex);
            }

            //Console.WriteLine("Diagnosis OnGet 18");
            return BadRequest();
        }
    }
}
