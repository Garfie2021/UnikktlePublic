using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestData
{
    public static class BcpProcess
    {
        public static void Exec(string arguments)
        {
            ログ.Add($"BCP Arguments:{arguments}");

            //Processオブジェクトを作成
            var p = new Process();

            //ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
            //p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            p.StartInfo.FileName = "bcp";

            //出力を読み取れるようにする
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = false;

            //ウィンドウを表示しないようにする
            p.StartInfo.CreateNoWindow = true;

            //コマンドラインを指定（"/c"は実行後閉じるために必要）
            //p.StartInfo.Arguments = @"UnikktleCollect.hst.t6CollaborateKeyword OUT D:\Unikktele\out\8Export\t6CollaborateKeyword.tsv -S localhost -U sa -P xxx -T -c -t \t -r \r\n";
            //p.StartInfo.Arguments = @"UnikktleCollect.hst.t6CollaborateKeyword OUT D:\Unikktele\Work\t6CollaborateKeyword.tsv -T -c -t \t -r \r\n";
            //p.StartInfo.Arguments = @"SELECT [KeywordNo_元],[KeywordNo_先],[同時出現ドキュメント数],[登録日時],[更新日時] FROM [UnikktleCollect].[hst].[t6CollaborateKeyword] WHERE [更新日時] > ddddd OUT D:\Unikktele\Work\t6CollaborateKeyword.tsv -T -c -t \t -r \r\n";
            p.StartInfo.Arguments = arguments;
            //p.StartInfo.Arguments = "\"select * from db名.dbo.テーブル名\" queryout \"C:\\test_exp.csv\" -S サーバー名 -U sa -P パスワード -N"

            //起動
            p.Start();

            // 標準出力に書き出されたテキストをログへ出力する
            ログ.Add(p.StandardOutput.ReadToEnd());

            //プロセス終了まで待機する
            //WaitForExitはReadToEndの後である必要がある
            //(親プロセス、子プロセスでブロック防止のため)
            p.WaitForExit();
            p.Close();
        }
    }
}
