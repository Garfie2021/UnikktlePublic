using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using 定数;


namespace AppDirectory
{
    public static class AppSetting
    {
        public static string Version;
        public static string CurrentDirectory;
        public static string LogPath;

        public static string AppDataFilePath;

        public static string HtmlParse_ErrorHtmlFolder;

        public static string ExtractEnglishConcatNoun_ErrorHtmlFolder;
        public static string ExtractEnglishConcatNoun_WarningHtmlFolder;
        public static string ExtractEnglishConcatNoun_ErrorMailFolder;

        public static string ResultPath_CollectEmailMEagazine;
        public static string ResultPath_MeCabExec;

        public static string BcpExportFilePath_hst_tCollaborateKeywordCount_WebServer;
        public static string BcpExportFilePath_mst_tKeyword;
        public static string BcpExportFilePath_hst_t6CollaborateKeyword;

        public static string BcpExportArgument_hst_tCollaborateKeywordCount_WebServer;
        //public static string BcpExportArgument_mst_tKeyword;
        //public static string BcpExportArgument_hst_t6CollaborateKeyword;

        public static string BcpImportArgument_hst_tCollaborateKeywordCount_WebServer;
        public static string BcpImportArgument_mst_tKeyword;
        public static string BcpImportArgument_hst_t6CollaborateKeyword;

        public static string OutputFolder_SiteMap;
        public static string OutputFolder_SiteMap_Bing;
        public static string OutputFolder_SiteMap_Google;

        public static string SSH_HostName;
        public static string SSH_Port;
        public static string SSH_KeyFile;
        public static string SSH_UserName;
        public static string SSH_PassPhrase;
        public static string UploadFileName_hst_t6CollaborateKeyword;
        public static string UploadFileName_mst_tKeyword;

        public static string Cmd_mvfile_SiteMap;
        

        public static string ConnectionString_UnikktleCmn;
        public static string ConnectionString_UnikktleCollect;
        public static string ConnectionString_UnikktleWeb;
        public static string ConnectionString_UnikktleWebCollectWork_WebServer;
        public static string ConnectionString_UnikktleWebCollectWork_CollectServer;
        public static string ConnectionString_Msdb;
        

        public static string Mail_Host;
        public static int Mail_Port;
        public static string Mail_Username;
        public static string Mail_Password;

        public static AppData AppData;


        public static void ReadAppSetting(string jsonFile)
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(jsonFile);

            var config = builder.Build();

            Version = config["Version"];

            CurrentDirectory = config["CurrentDirectory"];

            LogPath = config["LogPath"];

            AppDataFilePath = config["AppDataFilePath"];

            HtmlParse_ErrorHtmlFolder = config["HtmlParse_ErrorHtmlFolder"];

            BcpExportFilePath_hst_tCollaborateKeywordCount_WebServer = config["BcpExportFilePath_hst_tCollaborateKeywordCount_WebServer"];
            BcpExportFilePath_mst_tKeyword = config["BcpExportFilePath_mst_tKeyword"];
            BcpExportFilePath_hst_t6CollaborateKeyword = config["BcpExportFilePath_hst_t6CollaborateKeyword"];

            OutputFolder_SiteMap = config["OutputFolder_SiteMap"];
            OutputFolder_SiteMap_Bing = config["OutputFolder_SiteMap_Bing"];
            OutputFolder_SiteMap_Google = config["OutputFolder_SiteMap_Google"];

            BcpExportArgument_hst_tCollaborateKeywordCount_WebServer = config["BcpExportArgument_hst_tCollaborateKeywordCount_WebServer"];
            //BcpExportArgument_mst_tKeyword = config["BcpExportArgument_mst_tKeyword"];
            //BcpExportArgument_hst_t6CollaborateKeyword = config["BcpExportArgument_hst_t6CollaborateKeyword"];

            SSH_HostName = config["SSH_HostName"];
            SSH_Port = config["SSH_Port"];
            SSH_UserName = config["SSH_UserName"];
            SSH_KeyFile = config["SSH_KeyFile"];
            SSH_PassPhrase = config["SSH_PassPhrase"];

            UploadFileName_mst_tKeyword = config["UploadFileName_mst_tKeyword"];
            UploadFileName_hst_t6CollaborateKeyword = config["UploadFileName_hst_t6CollaborateKeyword"];

            BcpImportArgument_hst_tCollaborateKeywordCount_WebServer = config["BcpImportArgument_hst_tCollaborateKeywordCount_WebServer"];
            BcpImportArgument_mst_tKeyword = config["BcpImportArgument_mst_tKeyword"];
            BcpImportArgument_hst_t6CollaborateKeyword = config["BcpImportArgument_hst_t6CollaborateKeyword"];

            Cmd_mvfile_SiteMap = config["Cmd_mvfile_SiteMap"];

            ExtractEnglishConcatNoun_ErrorHtmlFolder = config["ExtractEnglishConcatNoun_ErrorHtmlPath"];
            ExtractEnglishConcatNoun_WarningHtmlFolder = config["ExtractEnglishConcatNoun_WarningHtmlPath"];
            ExtractEnglishConcatNoun_ErrorMailFolder = config["ExtractEnglishConcatNoun_ErrorMailPath"];

            ResultPath_CollectEmailMEagazine = config["ResultPath_CollectEmailMEagazine"];
            ResultPath_MeCabExec = config["ResultPath_MeCabExec"];

            ConnectionString_UnikktleCmn = config["ConnectionString_UnikktleCmn"];
    		ConnectionString_UnikktleCollect = config["ConnectionString_UnikktleCollect"];
    		ConnectionString_UnikktleWeb = config["ConnectionString_UnikktleWeb"];
            ConnectionString_UnikktleWebCollectWork_WebServer = config["ConnectionString_UnikktleWebCollectWork_WebServer"];
            ConnectionString_UnikktleWebCollectWork_CollectServer = config["ConnectionString_UnikktleWebCollectWork_CollectServer"];
            ConnectionString_Msdb = config["ConnectionString_Msdb"];

            Mail_Host = config["Mail_Host"];
            Mail_Port = int.Parse(config["Mail_Port"]);
            Mail_Username = config["Mail_Username"];
            Mail_Password = config["Mail_Password"];
        }

        public static void フォルダ初期化()
        {
            if (Directory.Exists(AppSetting.LogPath) == false)
            {
                Directory.CreateDirectory(AppSetting.LogPath);
            }

            if (Directory.Exists(AppSetting.ResultPath_CollectEmailMEagazine) == false)
            {
                Directory.CreateDirectory(AppSetting.ResultPath_CollectEmailMEagazine);
            }

            if (Directory.Exists(AppSetting.ResultPath_MeCabExec) == false)
            {
                Directory.CreateDirectory(AppSetting.ResultPath_MeCabExec);
            }
        }

    }
}
