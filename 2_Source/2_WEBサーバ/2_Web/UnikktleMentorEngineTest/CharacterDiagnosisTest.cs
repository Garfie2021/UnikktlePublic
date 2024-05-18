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
                    AnswerList.Add(new AnswerDetail() { Id = cnt, �� = �񓚑I����.�͂� });
                }

                CharacterDiagnosis.Diagnosis(Gender.Male, AnswerList,
                    out C01�e�_�v�Z_���� c01����,
                    out C02�n���l�v�Z_���� c02����,
                    out C03���q���__���� c03���q����,
                    out C03�n������_���� c03�n������,
                    out C04��b���q����_���� c04����,
                    out C05��b���q���Z����_���� c05����,
                    out C06�֘A���q����_���� c06����,
                    out C07�W�����q����_���� c07����,
                    out C08�ތ^�ʏW�����q����_���� c08����,
                    out C09�m�C���[�[���q����_���� c09����,
                    out C10���[�_�[��������_���� c10����,
                    out C11�E��ʓK��������_���� c11����,
                    out string str�f�f����,
                    out string str�G���[);


                if (string.IsNullOrEmpty(str�f�f����))
                {

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
        }

        [TestMethod]
        public void Test�ݖ���J�e�S�����Ƀ��X�g�A�b�v����()
        {
            try
            {
                var �ݖ�List = SetsumonList.GetSetsumonList_JA();

                var �J�e�S���Ɛݖ� = "";

                �J�e�S���Ɛݖ� += "1.�y�Љ�z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qS_�Љ)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "2.�y�x�z���z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qA_�x�z��)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "3.�y�v�l���z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qT_�v�l��)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "4.�y�̂�C�z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qR_�̂�C)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "5.�y�������z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qG_������)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "6.�y�U�����z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qAg_�U����)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "7.�y�������z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qCo_������)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "8.�y��ϐ��z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qO_��ϐ�)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "9.�y�_�o���z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qN_�_�o��)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "10.�y�򓙊��z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qI_�򓙊�)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "11.�y�C���̕ω��z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qC_�C���̕ω�)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                �J�e�S���Ɛݖ� += "12.�y�}�����z\r\n";
                foreach (var item in C01�e�_�v�Z.mia��b���qD_�}����)
                {
                    �J�e�S���Ɛݖ� += �ݖ�List[item].Setsumon + "\r\n";
                }
                �J�e�S���Ɛݖ� += "\r\n";

                Console.WriteLine(�J�e�S���Ɛݖ�);

            }
            catch (Exception ex)
            {
                ExceptionSt.ExceptionCommon(ex);
            }
        }
    }
}
