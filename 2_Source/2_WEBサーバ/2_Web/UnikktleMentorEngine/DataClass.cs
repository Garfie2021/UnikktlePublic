using System;
using System.Collections.Generic;
using System.Text;

namespace UnikktleMentorEngine
{
	public class Question
	{
		public byte Id;
		public string Setsumon;
		//public byte Answer { get; set; }
	}

	public class Question_Answer
	{
		public byte AnswerNo;
		public string Setsumon;
		public string Answer;
	}

	public class AnswerDetail
	{
        public byte Id { get; set; }             // [UnikktleMentorWeb].[hst].[tAnswer].AnswerNo
        public 回答選択肢 回答 { get; set; }   // [UnikktleMentorWeb].[hst].[tAnswer].Answer
    }

	public class AnswerDetailSupplement
	{
		public int Id { get; set; }						// [UnikktleMentorWeb].[hst].[tAnswer].AnswerNo
		public Gender Gender { get; set; }              // [UnikktleMentorWeb].[hst].[tAnswer].Gender
		public int Career { get; set; }                 // [UnikktleMentorWeb].[hst].[tAnswer].Career
		public string RecentHappenings { get; set; }	// [UnikktleMentorWeb].[hst].[tAnswer].Answer
	}


	public class C01粗点計算_結果
	{
		public byte 粗点S_社会性;
		public byte 粗点A_支配性;
		public byte 粗点T_思考性;
		public byte 粗点R_のん気;
		public byte 粗点G_活動性;
		public byte 粗点Ag_攻撃性;
		public byte 粗点Co_協調性;
		public byte 粗点O_主観性;
		public byte 粗点N_神経質;
		public byte 粗点I_劣等感;
		public byte 粗点C_気分の変化;
		public byte 粗点D_抑うつ性;

		public byte 信頼度;

		public byte mbytはいCnt;
		public byte mbytどちらともいえないCnt;
		public byte mbytいいえCnt;
		public byte mbyt訂正回答Cnt;
		public byte mbyt未回答Cnt;

		public string mstr信頼度_固定;

		public C01粗点計算_結果()
		{
			粗点S_社会性 = 0;
			粗点A_支配性 = 0;
			粗点T_思考性 = 0;
			粗点R_のん気 = 0;
			粗点G_活動性 = 0;
			粗点Ag_攻撃性 = 0;
			粗点Co_協調性 = 0;
			粗点O_主観性 = 0;
			粗点N_神経質 = 0;
			粗点I_劣等感 = 0;
			粗点C_気分の変化 = 0;
			粗点D_抑うつ性 = 0;

			mbytはいCnt = 0;
			mbytどちらともいえないCnt = 0;
			mbytいいえCnt = 0;
			mbyt訂正回答Cnt = 0;
			mbyt未回答Cnt = 0;

			信頼度 = 0;
			mstr信頼度_固定 = "【信頼度判定】" + "<br>";
		}
	}

	public class C02系統値計算_結果
	{
		//public Gender _Gender;

		public byte 標準点D_抑うつ性;
		public byte 標準点C_気分の変化;
		public byte 標準点I_劣等感;
		public byte 標準点N_神経質;
		public byte 標準点O_主観性;
		public byte 標準点Co_協調性;
		public byte 標準点Ag_攻撃性;
		public byte 標準点G_活動性;
		public byte 標準点R_のん気;
		public byte 標準点T_思考性;
		public byte 標準点A_支配性;
		public byte 標準点S_社会性;

		public byte miブロック別打点数Ⅰ;
		public byte miブロック別打点数Ⅱ;
		public byte miブロック別打点数Ⅲ;
		public byte miブロック別打点数Ⅳ;
		public byte miブロック別打点数Ⅴ;
		public byte miブロック別打点数Ⅵ;

		public byte mi系統値A;
		public byte mi系統値B;
		public byte mi系統値C;
		public byte mi系統値D;
		public byte mi系統値E;

		public C02系統値計算_結果()
		{
			//mi標準点D  = 3;
			//mi標準点C  = 3;
			//mi標準点I  = 3;
			//mi標準点N  = 3;
			//mi標準点O  = 3;
			//mi標準点Co = 3;
			//mi標準点Ag = 3;
			//mi標準点G  = 3;
			//mi標準点R  = 3;
			//mi標準点T  = 3;
			//mi標準点A  = 3;
			//mi標準点S  = 3;

			miブロック別打点数Ⅰ = 0;
			miブロック別打点数Ⅱ = 0;
			miブロック別打点数Ⅲ = 0;
			miブロック別打点数Ⅳ = 0;
			miブロック別打点数Ⅴ = 0;
			miブロック別打点数Ⅵ = 0;

			mi系統値A = 0;
			mi系統値B = 0;
			mi系統値C = 0;
			mi系統値D = 0;
			mi系統値E = 0;
		}

	}

	public class C03因子得点_結果
	{
		public string mstr固定;

		public C03因子得点_結果()
		{

		}
	}

	public class C03系統判定_結果
	{
		public string mstr固定;

		public string mstr系統コード;
		public string mstr類型;
		public string mstr傾向;

		public C03系統判定_結果()
		{
			mstr系統コード = "";
			mstr類型 = "";
			mstr傾向 = "";
			mstr固定 = "";
		}
	}

	public class C04基礎因子判定_結果
	{
		public string 虚脱感;
		public string 自信欠如;
		public string 神経質;
		public string 倦怠感;
		public string 閉鎖的_悲観;
		public string 小心;
		public string 集中力欠如;
		public string お天気屋;
		public string 感情的;
		public string 消極的;
		public string 優柔不断;
		public string 劣等感;
		public string 事なかれ主義;
		public string ロマンチスト;
		public string 分裂傾向;
		public string 対人不信感;
		public string 不運_不遇感;
		public string 不満感;
		public string 活動的;
		public string 果断_決断;
		public string 攻撃性;
		public string 衝動的;
		public string 自尊心;
		public string 短気;
		public string 好奇心旺盛;
		public string 協調的_善意;
		public string 敏腕_高能率;
		public string 快活_楽天的;
		public string 順応的;
		public string 多弁_お喋り;
		public string 軽率_気軽;
		public string 開放的_楽観;
		public string 非思索的;
		public string 無頓着_のん気;
		public string 自己顕示;
		public string 世話好き;
		public string 自信家;
		public string 指導的;
		public string 社交的;
		public string 派手好き;
		public string 充実感;
		public string 楽天的;
		public string 元気;
		public string 大胆_安心感;
		public string 集中力旺盛;
		public string 穏健;
		public string 冷静;
		public string 積極的;
		public string 優越感;
		public string 現実主義;
		public string 安定的;
		public string 幸運感;
		public string 満足感;
		public string 行動不活発;
		public string 慎重;
		public string 自己卑下;
		public string 気長;
		public string 現状維持的;
		public string 非協調的;
		public string 不器用_非能率;
		public string 陰気_幻滅感;
		public string 批判的;
		public string 無口;
		public string 熟慮_重厚;
		public string 思索的;
		public string 警戒心;
		public string 引込み思案;
		public string 不干渉主義;
		public string 非指導的;
		public string 非社交的;
		public string 地味;
		//public string 人間嫌い;
		public string 強気;
		public string 弱気;
		public string キビキビ;
		public string グズ;

		public string mstr固定;


		public C04基礎因子判定_結果()
		{
			mstr固定 = "";

			虚脱感 = "";
			自信欠如 = "";
			神経質 = "";
			倦怠感 = "";
			閉鎖的_悲観 = "";
			小心 = "";
			集中力欠如 = "";
			お天気屋 = "";
			感情的 = "";
			消極的 = "";
			優柔不断 = "";
			劣等感 = "";
			事なかれ主義 = "";
			ロマンチスト = "";
			分裂傾向 = "";
			対人不信感 = "";
			不運_不遇感 = "";
			不満感 = "";
			活動的 = "";
			攻撃性 = "";
			衝動的 = "";
			自尊心 = "";
			短気 = "";
			好奇心旺盛 = "";
			協調的_善意 = "";
			敏腕_高能率 = "";
			快活_楽天的 = "";
			順応的 = "";
			多弁_お喋り = "";
			軽率_気軽 = "";
			開放的_楽観 = "";
			非思索的 = "";
			無頓着_のん気 = "";
			自己顕示 = "";
			世話好き = "";
			自信家 = "";
			指導的 = "";
			社交的 = "";
			派手好き = "";
			充実感 = "";
			楽天的 = "";
			元気 = "";
			大胆_安心感 = "";
			集中力旺盛 = "";
			穏健 = "";
			冷静 = "";
			積極的 = "";
			果断_決断 = "";
			優越感 = "";
			現実主義 = "";
			安定的 = "";
			幸運感 = "";
			満足感 = "";
			行動不活発 = "";
			慎重 = "";
			自己卑下 = "";
			気長 = "";
			現状維持的 = "";
			非協調的 = "";
			不器用_非能率 = "";
			陰気_幻滅感 = "";
			批判的 = "";
			無口 = "";
			熟慮_重厚 = "";
			思索的 = "";
			警戒心 = "";
			引込み思案 = "";
			不干渉主義 = "";
			非指導的 = "";
			非社交的 = "";
			地味 = "";
			//人間嫌い = "";
			強気 = "";
			弱気 = "";
			キビキビ = "";
			グズ = "";
		}
	}

	public class C05基礎因子長短判定_結果
	{
		public string mstr可変;

		public string mstr長所;
		public string mstr短所;

		public C05基礎因子長短判定_結果()
		{
			mstr可変 = "";
			mstr長所 = "【長所判定】" + "<br>";
			mstr短所 = "【短所判定】" + "<br>";
		}
	}

	public class C06関連因子判定_結果
	{
		public string mstr可変;

		//public string mstr特徴;

		public C06関連因子判定_結果()
		{
			mstr可変 = "【多く見られる行動と傾向】" + "<br>";
		}
	}

	public class C07集合因子判定_結果
	{
		public string mstr固定;
		//public string mstr可変_補足;

		public string mstr情緒安定性;
		public string mstr社会適応性;
		public string mstr活動性;
		//private string mstr衝動性;
		public string mstr内省性;
		public string mstr主導性;
		public string mstr社交性;
		public string mstr共感性;

		public string mstr特徴;

		public C07集合因子判定_結果()
		{
			mstr情緒安定性 = "";
			mstr社会適応性 = "";
			mstr活動性 = "";
			//mstr衝動性 = "";
			mstr内省性 = "";
			mstr主導性 = "";
			mstr社交性 = "";
			mstr共感性 = "";

			mstr固定 = "";
			//mstr可変_補足 = "【集合因子判定補足】" + "<br>";
		}
	}

	public class C08類型別集合因子判定_結果
	{
		public string mstr可変;

		public C08類型別集合因子判定_結果()
		{
			mstr可変 = "";
		}
	}

	public class C09ノイローゼ因子判定_結果
	{
		public string mstr可変;

		public int mi因子Cnt;

		public C09ノイローゼ因子判定_結果()
		{
			//mstr可変 = "【ノイローゼ因子判定】" + "<br>";
			mstr可変 = "";

			mi因子Cnt = 0;
		}
	}

	public class C10リーダー資質判定_結果
	{
		public string mstr固定;
		public string mstr可変_タイプ;
		//public string mstr可変_補足;
		public string mstr可変_阻害因子;

		public int mi阻害因子Cnt;

		public C10リーダー資質判定_結果()
		{
			mstr固定 = "";

			mstr可変_タイプ = "";
			//mstr可変_補足 = "";

			//mstr可変_阻害因子 = "【リーダーシップ阻害因子判定】" + "<br>";
			mstr可変_阻害因子 = "";

			mi阻害因子Cnt = 0;
		}

	}

	public class C11職種別適応性判定_結果
	{
		public string mstr固定_一般職;
		public string mstr固定_専門職;

		public C11職種別適応性判定_結果()
		{
			mstr固定_一般職 = "";
			mstr固定_専門職 = "";
		}
	}

}
