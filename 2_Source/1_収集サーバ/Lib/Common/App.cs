using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using 定数;
using Logging;
using AppDirectory;


namespace Common
{
    public static class App
    {
        public static void Initialize(string args, string プログラム名)
        {
            AppSetting.ReadAppSetting(args);

            if (File.Exists(AppSetting.AppDataFilePath))
            {
                var str = File.ReadAllText(AppSetting.AppDataFilePath, Encoding.UTF8);
                AppSetting.AppData = JsonUtility.Deserialize<AppData>(str);
            }
            else
            {
                AppSetting.AppData = new AppData();
            }

            AppSetting.フォルダ初期化();

            //Directory.SetCurrentDirectory(AppSetting.CurrentDirectory);
            ログ.ログフォルダ = AppSetting.LogPath;
            ログ.HtmlParse_ErrorHtmlFolder = AppSetting.HtmlParse_ErrorHtmlFolder;
            ログ.ExtractEnglishConcatNoun_ErrorHtmlFolder = AppSetting.ExtractEnglishConcatNoun_ErrorHtmlFolder;
            ログ.ExtractEnglishConcatNoun_WarningHtmlFolder = AppSetting.ExtractEnglishConcatNoun_WarningHtmlFolder;
            ログ.プログラム名 = プログラム名;

            ログ.ログ書き出し("Start");
            ログ.ログ書き出し("args[0]：" + args);
            //ログ.ログ書き出し("CurrentDirectory：" + Directory.GetCurrentDirectory());
        }

        // 現在のアプリケーションデータを書き込み
        public static void WriteAllAppDate()
        {
            var json = JsonUtility.Serialize(AppSetting.AppData);
            File.WriteAllText(AppSetting.AppDataFilePath, json, Encoding.UTF8);
        }
    }
}
