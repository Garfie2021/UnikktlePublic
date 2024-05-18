using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using �萔;
using DB;
using CollectEmailMEagazine;
using AppDirectory;


namespace UnitTest_x64
{
    [TestClass]
    public class CollectEmailMEagazine_Test
    {
        [TestMethod]
        public void _1_InsertCollectTarget()
        {
            var target = new CollectTargetRow()
            {
                ���� = "a",
                From_MailAddress = "b",
            };

            using (var cn = new SqlConnection())
            {
                AppSetting.ConnectionString_UnikktleCollect = "Data Source=xxx;Initial Catalog=UnikktleCollect;User ID=xxx;Password=xxx";

                cn.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                cn.Open();

                // DB�o�^
                var No = DB.Collect.SP_CollectTarget.Insert(cn, target);
            }
        }

        [TestMethod]
        public void _1_GetCollectTargetNo()
        {
            var mail = new CollectMailRow()
            {
                SendDate = DateTime.Parse("2018/04/28 13:10:44"),
                FromMailAddress = "itmid-membership@noreply.itmedia.jp",
                FromDisplayName = "�A�C�e�B���f�B�AID������",
                CurrentMessageID = "20180428131044.60774E058@itmidsh02.itmedia.co.jp",
                //CurrentSubject = " �y�A�C�e�B���f�B�AID�z�{�o�^�����̂��m�点",
                CurrentBody = "\r\n�������������������������������������������������������������c�c�d�d�E�E�E\r\n�y�A�C�e�B���f�B�AID�z�{�o�^�����̂��m�点\r\n�E�E�E�d�d�c�c������������������������������������������������������������\r\n\r\n���̓x�́A�A�C�e�B���f�B�AID�ւ̓o�^�����\�����݂��������A���ɂ��肪�Ƃ�\r\n�������܂��B\r\n���\�����݂������������e�Ŗ{�o�^���������܂����B\r\n\r\n���킹�āA�ȉ��̃T�[�r�X�̗��p�J�n�葱�����������܂����B\r\n\r\n�����\�����݂����������T�[�r�X\r\n�E��IT�ʐM\r\n�E��IT�V������\r\n�E��IT�����헪������Weekly \r\n\r\n���u�A�C�e�B���f�B�AID�v�Ƃ�\r\n�A�C�e�B���f�B�A������Ђ��񋟂������o�^���̊e��T�[�r�X��R���e���c��\r\n���p���邽�߂�ID�ł��B����ID�ɓo�^����ƁA�L���A���[�g���͂��߂Ƃ���e��\r\n�T�[�r�X�̗��p�ƈꊇ�Ǘ��A�o�^���̊m�F�ƕύX�Ȃǂ��s����悤�ɂȂ�܂��B\r\n\r\n���ꕔ�A�A�C�e�B���f�B�AID�ɑΉ����Ă��Ȃ����f�B�A��T�[�r�X������܂��B\r\n\r\n���o�^���e��ύX�������ꍇ\r\n https://id.itmedia.jp/profile \r\n\r\n���p�X���[�h�����Y��̏ꍇ\r\n https://id.itmedia.jp/password_reset \r\n\r\n���A�C�e�B���f�B�AID���폜�������ꍇ\r\n https://id.itmedia.jp/unsubscribe \r\n\r\n--------------------------------------------------------------------------\r\n��������\r\n--------------------------------------------------------------------------\r\n�E���̃��[���́A�A�C�e�B���f�B�AID�ւ̓o�^�����\�����݂������������Ɏ���\r\n�@���M���Ă��܂��B�{���[���ɂ��S�����肪�Ȃ��ꍇ�́A���ɋ������܂����A\r\n�@�j�����Ă��������܂��悤���肢�\���グ�܂��B\r\n\r\n�E���̃A�h���X�͑��M��p�̂��߁A���̃��[���ɂ��ԐM���������Ă��񓚂ł���\r\n�@����B���炩���߂��������������B\r\n\r\n--------------------------------------------------------------------------\r\n�����₢���킹\r\n--------------------------------------------------------------------------\r\n���w���v�^�悭���邲����\r\n http://id.itmedia.jp/help/ \r\n�����₢���킹�y�[�W\r\n http://id.itmedia.jp/help/inquiry.html \r\n\r\n�A�C�e�B���f�B�A������Ё@�A�C�e�B���f�B�AID ������\r\n��102-0094�@�����s���c��I���䒬3-12 �I���䒬�r��13F\r\n--------------------------------------------------------------------------\r\nCopyright 2000-2018 ITmedia Inc. All Rights Reserved. ",
            };

            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = AppSetting.ConnectionString_UnikktleCollect;
                cn.Open();

                mail.CollectTargetNo = MorphologicalAnalysisDB.GetCollectTargetMail_No(cn, mail);
            }
        }

        [TestMethod]
        public void _2_CollectEmail_Write()
        {
            try
            {
                var mail = new CollectMailRow()
                {
                    CollectTargetNo = 0,
                    SendDate = DateTime.Parse("2018/04/28 13:10:44"),
                    FromMailAddress = "itmid-membership@noreply.itmedia.jp",
                    FromDisplayName = "�A�C�e�B���f�B�AID������",
                    CurrentMessageID = "20180428131044.60774E058@itmidsh02.itmedia.co.jp",
                    //CurrentSubject = " �y�A�C�e�B���f�B�AID�z�{�o�^�����̂��m�点",
                    CurrentBody = "\r\n�������������������������������������������������������������c�c�d�d�E�E�E\r\n�y�A�C�e�B���f�B�AID�z�{�o�^�����̂��m�点\r\n�E�E�E�d�d�c�c������������������������������������������������������������\r\n\r\n���̓x�́A�A�C�e�B���f�B�AID�ւ̓o�^�����\�����݂��������A���ɂ��肪�Ƃ�\r\n�������܂��B\r\n���\�����݂������������e�Ŗ{�o�^���������܂����B\r\n\r\n���킹�āA�ȉ��̃T�[�r�X�̗��p�J�n�葱�����������܂����B\r\n\r\n�����\�����݂����������T�[�r�X\r\n�E��IT�ʐM\r\n�E��IT�V������\r\n�E��IT�����헪������Weekly \r\n\r\n���u�A�C�e�B���f�B�AID�v�Ƃ�\r\n�A�C�e�B���f�B�A������Ђ��񋟂������o�^���̊e��T�[�r�X��R���e���c��\r\n���p���邽�߂�ID�ł��B����ID�ɓo�^����ƁA�L���A���[�g���͂��߂Ƃ���e��\r\n�T�[�r�X�̗��p�ƈꊇ�Ǘ��A�o�^���̊m�F�ƕύX�Ȃǂ��s����悤�ɂȂ�܂��B\r\n\r\n���ꕔ�A�A�C�e�B���f�B�AID�ɑΉ����Ă��Ȃ����f�B�A��T�[�r�X������܂��B\r\n\r\n���o�^���e��ύX�������ꍇ\r\n https://id.itmedia.jp/profile \r\n\r\n���p�X���[�h�����Y��̏ꍇ\r\n https://id.itmedia.jp/password_reset \r\n\r\n���A�C�e�B���f�B�AID���폜�������ꍇ\r\n https://id.itmedia.jp/unsubscribe \r\n\r\n--------------------------------------------------------------------------\r\n��������\r\n--------------------------------------------------------------------------\r\n�E���̃��[���́A�A�C�e�B���f�B�AID�ւ̓o�^�����\�����݂������������Ɏ���\r\n�@���M���Ă��܂��B�{���[���ɂ��S�����肪�Ȃ��ꍇ�́A���ɋ������܂����A\r\n�@�j�����Ă��������܂��悤���肢�\���グ�܂��B\r\n\r\n�E���̃A�h���X�͑��M��p�̂��߁A���̃��[���ɂ��ԐM���������Ă��񓚂ł���\r\n�@����B���炩���߂��������������B\r\n\r\n--------------------------------------------------------------------------\r\n�����₢���킹\r\n--------------------------------------------------------------------------\r\n���w���v�^�悭���邲����\r\n http://id.itmedia.jp/help/ \r\n�����₢���킹�y�[�W\r\n http://id.itmedia.jp/help/inquiry.html \r\n\r\n�A�C�e�B���f�B�A������Ё@�A�C�e�B���f�B�AID ������\r\n��102-0094�@�����s���c��I���䒬3-12 �I���䒬�r��13F\r\n--------------------------------------------------------------------------\r\nCopyright 2000-2018 ITmedia Inc. All Rights Reserved. ",
                };

                CollectEmail.Write(mail);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }



        //[TestMethod]
        //public void _4_Kaiseki()
        //{
        //    try
        //    {
        //        var morph = new MorphologicalAnalysisInfo()
        //        {
        //            No = 0,
        //            ���[�����M���� = DateTime.Parse("2018/04/28 13:10:44"),
        //            From_MailAddress = "itmid-membership@noreply.itmedia.jp",
        //            From_DisplayName = "�A�C�e�B���f�B�AID������",
        //            CurrentMessageID = "20180428131044.60774E058@itmidsh02.itmedia.co.jp",
        //            MorphologicalAnalysis = "�e�X�g\t����,�T�ϐڑ�,*,*,*,*,�e�X�g,�e�X�g,�e�X�g\n�f�[�^\t����,���,*,*,*,*,�f�[�^,�f�[�^,�f�[�^\nEOS\n",
        //        };

        //        using (SqlConnection cn = new SqlConnection())
        //        {
        //            cn.ConnectionString = DBConst.ConnectionString_UnikktleCollect;
        //            cn.Open();

        //            MeCabDB.Kaiseki(cn, morph.No, morph.���[�����M����, morph.MorphologicalAnalysis);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Trace.WriteLine(ex.Message);
        //    }
        //}

    }
}
