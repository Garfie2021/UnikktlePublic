using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikktleMentorEngine;
using System.Collections.Generic;
using System;
using UnikktleCommon;

namespace UnikktleMentorEngineTest
{
    [TestClass]
    public class CharacterDiagnosisTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                var AnswerList = new List<AnswerDetail>();
                for (byte cnt = 0; cnt < 120; cnt++)
                {
                    AnswerList.Add(new AnswerDetail() { Id = cnt, ‰ñ“š = ‰ñ“š‘I‘ðŽˆ.‚Í‚¢ });
                }

                CharacterDiagnosis.Diagnosis(Gender.Male, AnswerList,
                    out C01‘e“_ŒvŽZ_Œ‹‰Ê c01Œ‹‰Ê,
                    out C02Œn“’lŒvŽZ_Œ‹‰Ê c02Œ‹‰Ê,
                    out C03ˆöŽq“¾“__Œ‹‰Ê c03ˆöŽqŒ‹‰Ê,
                    out C03Œn“”»’è_Œ‹‰Ê c03Œn“Œ‹‰Ê,
                    out C04Šî‘bˆöŽq”»’è_Œ‹‰Ê c04Œ‹‰Ê,
                    out C05Šî‘bˆöŽq’·’Z”»’è_Œ‹‰Ê c05Œ‹‰Ê,
                    out C06ŠÖ˜AˆöŽq”»’è_Œ‹‰Ê c06Œ‹‰Ê,
                    out C07W‡ˆöŽq”»’è_Œ‹‰Ê c07Œ‹‰Ê,
                    out C08—ÞŒ^•ÊW‡ˆöŽq”»’è_Œ‹‰Ê c08Œ‹‰Ê,
                    out C09ƒmƒCƒ[ƒ[ˆöŽq”»’è_Œ‹‰Ê c09Œ‹‰Ê,
                    out C10ƒŠ[ƒ_[Ž‘Ž¿”»’è_Œ‹‰Ê c10Œ‹‰Ê,
                    out C11EŽí•Ê“K‰ž«”»’è_Œ‹‰Ê c11Œ‹‰Ê,
                    out string strf’fŒ‹‰Ê,
                    out string strƒGƒ‰[);


                if (string.IsNullOrEmpty(strf’fŒ‹‰Ê))
                {

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        [TestMethod]
        public void TestÝ–â‚ðƒJƒeƒSƒŠ–ˆ‚ÉƒŠƒXƒgƒAƒbƒv‚·‚é()
        {
            try
            {
                var Ý–âList = SetsumonList.GetSetsumonList_JA();

                var ƒJƒeƒSƒŠ‚ÆÝ–â = "";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "1.yŽÐ‰ï«z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqS_ŽÐ‰ï«)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "2.yŽx”z«z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqA_Žx”z«)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "3.yŽvl«z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqT_Žvl«)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "4.y‚Ì‚ñ‹Cz\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqR_‚Ì‚ñ‹C)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "5.yŠˆ“®«z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqG_Šˆ“®«)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "6.yUŒ‚«z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqAg_UŒ‚«)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "7.y‹¦’²«z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqCo_‹¦’²«)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "8.yŽåŠÏ«z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqO_ŽåŠÏ«)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "9.y_ŒoŽ¿z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqN__ŒoŽ¿)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "10.y—ò“™Š´z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqI_—ò“™Š´)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "11.y‹C•ª‚Ì•Ï‰»z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqC_‹C•ª‚Ì•Ï‰»)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                ƒJƒeƒSƒŠ‚ÆÝ–â += "12.y—}‚¤‚Â«z\r\n";
                foreach (var item in C01‘e“_ŒvŽZ.miaŠî‘bˆöŽqD_—}‚¤‚Â«)
                {
                    ƒJƒeƒSƒŠ‚ÆÝ–â += Ý–âList[item].Setsumon + "\r\n";
                }
                ƒJƒeƒSƒŠ‚ÆÝ–â += "\r\n";

                Console.WriteLine(ƒJƒeƒSƒŠ‚ÆÝ–â);

            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }
    }
}
