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
                    AnswerList.Add(new AnswerDetail() { Id = cnt, 回答 = 回答選択肢.はい });
                }

                CharacterDiagnosis.Diagnosis(Gender.Male, AnswerList,
                    out C01粗点計算_結果 c01結果,
                    out C02系統値計算_結果 c02結果,
                    out C03因子得点_結果 c03因子結果,
                    out C03系統判定_結果 c03系統結果,
                    out C04基礎因子判定_結果 c04結果,
                    out C05基礎因子長短判定_結果 c05結果,
                    out C06関連因子判定_結果 c06結果,
                    out C07集合因子判定_結果 c07結果,
                    out C08類型別集合因子判定_結果 c08結果,
                    out C09ノイローゼ因子判定_結果 c09結果,
                    out C10リーダー資質判定_結果 c10結果,
                    out C11職種別適応性判定_結果 c11結果,
                    out string str診断結果,
                    out string strエラー);


                if (string.IsNullOrEmpty(str診断結果))
                {

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        [TestMethod]
        public void Test設問をカテゴリ毎にリストアップする()
        {
            try
            {
                var 設問List = SetsumonList.GetSetsumonList_JA();

                var カテゴリと設問 = "";

                カテゴリと設問 += "1.【社会性】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子S_社会性)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "2.【支配性】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子A_支配性)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "3.【思考性】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子T_思考性)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "4.【のん気】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子R_のん気)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "5.【活動性】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子G_活動性)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "6.【攻撃性】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子Ag_攻撃性)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "7.【協調性】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子Co_協調性)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "8.【主観性】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子O_主観性)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "9.【神経質】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子N_神経質)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "10.【劣等感】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子I_劣等感)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "11.【気分の変化】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子C_気分の変化)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                カテゴリと設問 += "12.【抑うつ性】\r\n";
                foreach (var item in C01粗点計算.mia基礎因子D_抑うつ性)
                {
                    カテゴリと設問 += 設問List[item].Setsumon + "\r\n";
                }
                カテゴリと設問 += "\r\n";

                Console.WriteLine(カテゴリと設問);

            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }
    }
}
