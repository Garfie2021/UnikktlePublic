using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;


namespace MeCabExec
{
    public static class MeCabConst
    {
        const string MECAB_LIB_PATH = @"C:\Program Files (x86)\MeCab\bin\libmecab.dll";

        [DllImport(MECAB_LIB_PATH, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr mecab_new2(String arg);

        [DllImport(MECAB_LIB_PATH, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr mecab_sparse_tostr(IntPtr ptrMeCab, String arg);

        [DllImport(MECAB_LIB_PATH, CallingConvention = CallingConvention.Cdecl)]
        public extern static void mecab_destroy(IntPtr ptrMeCab);


        //// mecab_t *mecab_new (int argc, char **argv)
        //// mecab のインスタンスを生成
        //// 引数には, C 言語の, main 関数で使用される argc, argv スタイルの引数を与えます.
        //// この引数は, mecab コマンドと同じ方法で処理されます
        //// 成功すれば, mecab_t 型のポインタが返ってきます. このポインタを通して解析 を行います. 失敗すれば NULL が返ってきます
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_new")]
        //public static extern IntPtr mecab_new(string arg);

        //// mecab_t *mecab_new2 (const char *arg)
        //// mecab のインスタンスを生成
        //// 引数には, 一つの文字列として表現したパラメータを与えます
        //// 成功すれば, mecab_t 型のポインタが返ってきます. このポインタを通して解析を行います
        ////[DllImport("libmecab.dll")]
        ////public extern static IntPtr mecab_new2(string arg);
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_new2")]
        //public static extern IntPtr mecab_new2(string arg);

        //// const char *mecab_strerror (mecab_t* m)
        //// エラーの内容を文字列として取得します. mecab_sparse_tostr 等で, NULL が返ってきた場合に, mecab_strerror を呼ぶことで, 
        //// エラーの内容を取得できます. mecab_new,mecab_new2 のエラーは, m を NULL と指定してください
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_strerror")]
        //public static extern string mecab_strerror(IntPtr m);

        //// const char *mecab_sparse_tostr (mecab_t *m, const char *str)
        //// 解析を行います. 引数には, mecab_new で得られた mecab_t 型のポインタと,
        //// 解析したい文を char 型のポインタ文字列として与えます.
        //// 成功すれば, 解析後の結果が char 型のポインタとして返り, 失敗すれば, NULL が返ってきます
        //// 戻り値のポインタが指すメモリ領域は, 呼び出し側で管理する必要はありませんが
        //// mecab_sparse_tostr を呼ぶ度に上書きされます
        //// また, mecab_destroy を呼ぶと解放されます
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_sparse_tostr")]
        //public static extern IntPtr mecab_sparse_tostr(IntPtr m, string str);

        //// const char *mecab_sparse_tostr2 (mecab_t *m, const char *str, size_t len
        //// mecab_sparse_tostr とほぼ同じですが, len にて, 解析する文の長さを指定できます
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_sparse_tostr2")]
        //public static extern string mecab_sparse_tostr2(IntPtr m, string str, int len);

        //// char *mecab_sparse_tostr3 (mecab_t *m, const char *istr,size_t ilen char *ostr, size_t olen)
        //// mecab_sparse_tostr2 に加え, 出力用のバッファ領域 (ostr), 及びその長さ (olen) を指定できます. ostr の領域の管理は, 
        //// 呼び出し側が行います. 成功すれば, 解析後の結果が char 型のポインタとして返ってきます. これは, ostr と同じになります. 
        //// もし, 解析結果の長さが olen 以上になった場合は, 解析失敗とみなし, NULL を返します
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_sparse_tostr3")]
        //public static extern string mecab_sparse_tostr3(IntPtr m, string istr, int ilen, string ostr, int olen);

        //// const char *mecab_nbest_sparse_tostr (mecab_t *m, size_t N, const char *str)
        //// mecab_sparse_tostr () の N-Best 解出力 version です. N にて解析結果の個数を指定します. また, N-Best の機能を使う場合は, 
        //// 必ず mecab_new にて -l 1 オプションを指定する必要があります
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_nbest_sparse_tostr")]
        //public static extern string mecab_nbest_sparse_tostr(IntPtr m, int N, string str);

        //// const char *mecab_nbest_sparse_tostr2 (mecab_t *m, size_t N, const char *str, size_t len)
        //// mecab_sparse_tostr2 () の N-Best 解出力 version です. N にて解析結果の個数を指定します
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_nbest_sparse_tostr2")]
        //public static extern string mecab_nbest_sparse_tostr2(IntPtr m, int N, string str, int ilen);

        //// char *mecab_nbest_sparse_tostr3 (mecab_t *m, size_t N, const char *str, size_t len, char *ostr, size_t olen)
        //// mecab_sparse_tostr3 () の N-Best 解出力 version です. N にて解析結果の個数を指定します
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_nbest_sparse_tostr3")]
        //public static extern string mecab_nbest_sparse_tostr3(IntPtr m, int N, string istr, int ilen, string ostr, int olen);

        //// int mecab_nbest_init (mecab_t* m, const char* str); 
        //// 解析結果を, 確からしいものから順番に取得する場合にこの関数で初期化を行います. 解析したい文を str に指定します. 
        //// 初期化に成功すると 1 を, 失敗すると 0 を返します. 失敗の原因は, mecab_strerror で取得できます.
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_nbest_init")]
        //public static extern int mecab_nbest_init(IntPtr m, string str);

        //// int mecab_nbest_init2 (mecab_t* m, const char* str, len); 
        //// mecab_nbest_init () とほぼ同じですが, len にて文の長さを指定できます. 
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_nbest_init2")]
        //public static extern int mecab_nbest_init2(IntPtr m, string istr, int len);

        //// const char *mecab_nbest_next_tostr (mecab_t* m) 
        //// mecab_nbest_init() の後, この関数を順次呼ぶことで, 確からしい解析結果 を, 順番に取得できます. 失敗すると
        ////  (これ以上の解が存在しない場合) NULL を返します. 失敗の原因は, mecab_strerror で取得できます. 
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_nbest_next_tostr")]
        //public static extern string mecab_nbest_next_tostr(IntPtr m);

        //// char *mecab_nbest_next_tostr2 (mecab_t *m , char *ostr, size_t olen) 
        //// mecab_nbest_tostr() とほぼ同じですが, ostr, olen にて出力用のバッファを 指定できます. 失敗すると
        ////  (これ以上の解が存在しない場合) NULL を返します. また, 出力バッファが溢れた場合も NULL を返します. 
        //// 失敗の原因は, mecab_strerror で取得できます. 
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_nbest_next_tostr2")]
        //public static extern string mecab_nbest_next_tostr2(IntPtr m, string ostr, int olen);

        //// void mecab_destroy(mecab_t *m) 
        //// mecab_t 型のポインタを解放します.
        //[DllImport(MECAB_LIB_PATH, EntryPoint = "mecab_destroy")]
        //public static extern string mecab_destroy(IntPtr m);


    }
}
