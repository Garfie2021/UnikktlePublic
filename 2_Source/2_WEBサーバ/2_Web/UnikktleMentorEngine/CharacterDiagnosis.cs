using System;
using System.Collections.Generic;
using System.Text;
using UnikktleMentorEngine;


namespace UnikktleMentorEngine
{
	public static class CharacterDiagnosis
	{
		private const string 系統 = "【系統】";
		private const string 因子得点 = "【因子得点】";
		private const string 基礎因子判定 = "【基礎因子判定】";
		private const string 集合因子判定 = "【集合因子判定】";
		private const string 集合因子判定補足 = "【集合因子判定補足】";
		private const string 多く見られる行動と傾向 = "【多く見られる行動と傾向】";
		private const string 長所判定 = "【長所判定】";
		private const string 短所判定 = "【短所判定】";
		private const string 男性 = "男性";
		private const string 女性 = "女性";
		private const string 性別が正しくありません = "性別が正しくありません。";
		private const string 男性か女性のいずれかを入力して下さい = "「男性」か「女性」のいずれかを入力して下さい。";
		private const string 入力された性別 = "入力された性別：";
		private const string _120設問の回答数を満たしていません = "120設問の回答数を満たしていません。";
		private const string 入力された回答数 = "入力された回答数：";
		private const string はい = "はい";
		private const string どちらともいえない = "どちらともいえない";
		private const string いいえ = "いいえ";
		private const string 訂正回答 = "訂正あり";
		private const string 未回答 = "未回答";
		private const string 回答に選択肢には無い値があります = "回答に選択肢には無い値があります。";
		private const string 入力された値 = "入力された値：";



		//////////////////////////////////////////////////////////////////////////
		//// メンバ変数
		//////////////////////////////////////////////////////////////////////////
		//#region メンバ変数

		//public static Gender mstr性別 = "";

		//#endregion




		public static void Diagnosis(Gender gender, List<AnswerDetail> anserList,
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
			out string strエラー)
		{
			str診断結果 = "";
			strエラー = "";
			//mstr性別 = gender;

			//string[] stra回答 = str回答.Split('\t');

			//if (入力チェック(gender, stra回答, out strエラー) == false)
			//	return;

			c01結果 = new C01粗点計算_結果();
			c02結果 = new C02系統値計算_結果();
			c03因子結果 = new C03因子得点_結果();
			c03系統結果 = new C03系統判定_結果();
			c04結果 = new C04基礎因子判定_結果();
			c05結果 = new C05基礎因子長短判定_結果();
			c06結果 = new C06関連因子判定_結果();
			c07結果 = new C07集合因子判定_結果();
			c08結果 = new C08類型別集合因子判定_結果();
			c09結果 = new C09ノイローゼ因子判定_結果();
			c10結果 = new C10リーダー資質判定_結果();
			c11結果 = new C11職種別適応性判定_結果();

			C01粗点計算.粗点計算(anserList, c01結果);
			C02系統値計算.系統値計算(gender, c01結果, c02結果);

			#if DEBUG
			//検算
			if ((c02結果.mi系統値A + c02結果.mi系統値B + c02結果.mi系統値C) != 12)
				return;
			if ((c02結果.mi系統値A + c02結果.mi系統値D + c02結果.mi系統値E) != 12)
				return;
			#endif

			C03因子得点.因子得点(c02結果, c03因子結果);
			C03系統判定.系統判定(c02結果, c03系統結果);
			C04基礎因子判定.基礎因子判定(c02結果, c04結果);
			C05基礎因子長短判定.基礎因子長短判定(c02結果, c05結果);
			C06関連因子判定.関連因子判定(c02結果, c06結果, c10結果);
			C07集合因子判定.集合因子判定(c02結果, c06結果, c07結果);
			C08類型別集合因子判定.類型別集合因子判定(c08結果);
			C09ノイローゼ因子判定.ノイローゼ因子判定(c02結果, c09結果);
			C10リーダー資質判定.リーダー資質判定(c02結果, c07結果, c10結果);
			C11職種別適応性判定.職種別適応性判定(gender, c02結果, c03系統結果, c07結果, c11結果);

			//str診断結果 += c01結果.mstr信頼度_固定 + "<br><br>";
			str診断結果 += c03系統結果.mstr固定 + "<br><br>";
			//str診断結果 += c03因子結果.mstr固定 + "<br><br>";
			str診断結果 += c07結果.mstr固定 + "<br><br>";
			str診断結果 += c04結果.mstr固定 + "<br><br>";
			str診断結果 += c10結果.mstr固定 + "<br><br>";
			str診断結果 += c11結果.mstr固定_一般職 + "<br><br>";
			str診断結果 += c11結果.mstr固定_専門職 + "<br><br>";

			//str診断結果 += C06集合因子判定.mstr可変_補足 + "<br><br>";
			str診断結果 += c06結果.mstr可変 + "<br><br>";
			str診断結果 += c05結果.mstr可変 + "<br><br>";
			str診断結果 += c09結果.mstr可変 + "<br><br>";
			//str診断結果 += C08類型別集合因子判定.mstr可変 + "<br><br>";
			str診断結果 += c10結果.mstr可変_タイプ + "<br><br>";
			//str診断結果 += c10結果.mstr可変_補足 + "<br><br>";
			str診断結果 += c10結果.mstr可変_阻害因子 + "<br><br>";

		}


		public static bool 入力チェック(string str性別, string[] stra回答, out string strエラー)
		{
			strエラー = "";

			if (str性別.IndexOf(男性) < 0 && str性別.IndexOf(女性) < 0)
			{
				strエラー += 性別が正しくありません + "<br>";
				strエラー += 男性か女性のいずれかを入力して下さい + "<br>";
				strエラー += 入力された性別 + str性別;
				return false;
			}

			if (stra回答.Length < 120)
			{
				strエラー += _120設問の回答数を満たしていません + "<br>";
				strエラー += 入力された回答数 + stra回答.Length.ToString();
				return false;
			}

			for (byte iCnt = 0; iCnt < stra回答.Length; iCnt++)
			{
				if (stra回答[iCnt].IndexOf(はい) < 0 &&
					stra回答[iCnt].IndexOf(どちらともいえない) < 0 &&
					stra回答[iCnt].IndexOf(いいえ) < 0 &&
					stra回答[iCnt].IndexOf(訂正回答) < 0 &&
					stra回答[iCnt].IndexOf(未回答) < 0 &&
					stra回答[iCnt].Length != 0)
				{
					strエラー += 回答に選択肢には無い値があります + "<br>";
					strエラー += 入力された値 + stra回答[iCnt];
					return false;
				}
			}

			return true;
		}





	}
}
