using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using 定数;
using AppDirectory;


namespace MeCabExec
{
    public static class MeCabResult
    {
        public static void Write(MorphologicalAnalysisInfo morph)
        {
            File.AppendAllText(AppSetting.ResultPath_MeCabExec + "\\" + Path.GetRandomFileName(),
                morph.No + "\t" + morph.メール送信日時 + "\t" + morph.From_MailAddress + "\t" + morph.From_DisplayName + "\t" + morph.CurrentMessageID + "\r\n" +
                //morph.CurrentSubject + "\r\n" +
                morph.MorphologicalAnalysis,
                Encoding.UTF8);
        }

    }
}
