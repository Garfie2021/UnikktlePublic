using System;
using System.Collections.Generic;
using System.Text;
using UnikktleMentorEngine;

namespace UnikktleMentorEngine
{
	public static class C01粗点計算
	{
		////////////////////////////////////////////////////////////////////////
		// 非公開 文言変数
		////////////////////////////////////////////////////////////////////////

		private const string はい = "はい";
		private const string どちらともいえない = "どちらともいえない";


		public static byte[] mia基礎因子S_社会性 = new byte[] {  0, 12, 24, 36, 48, 60, 72, 84,  96, 108 };
		public static byte[] mia基礎因子A_支配性 = new byte[] {  1, 13, 25, 37, 49, 61, 73, 85,  97, 109 };
		public static byte[] mia基礎因子T_思考性 = new byte[] {  2, 14, 26, 38, 50, 62, 74, 86,  98, 110 };
		public static byte[] mia基礎因子R_のん気 = new byte[] {  3, 15, 27, 39, 51, 63, 75, 87,  99, 111 };
		public static byte[] mia基礎因子G_活動性 = new byte[] {  4, 16, 28, 40, 52, 64, 76, 88, 100, 112 };
		public static byte[] mia基礎因子Ag_攻撃性 = new byte[] {  5, 17, 29, 41, 53, 65, 77, 89, 101, 113 };
		public static byte[] mia基礎因子Co_協調性 = new byte[] {  6, 18, 30, 42, 54, 66, 78, 90, 102, 114 };
		public static byte[] mia基礎因子O_主観性 = new byte[] {  7, 19, 31, 43, 55, 67, 79, 91, 103, 115 };
		public static byte[] mia基礎因子N_神経質 = new byte[] {  8, 20, 32, 44, 56, 68, 80, 92, 104, 116 };
		public static byte[] mia基礎因子I_劣等感 = new byte[] {  9, 21, 33, 45, 57, 69, 81, 93, 105, 117 };
		public static byte[] mia基礎因子C_気分の変化 = new byte[] { 10, 22, 34, 46, 58, 70, 82, 94, 106, 118 };
		public static byte[] mia基礎因子D_抑うつ性 = new byte[] { 11, 23, 35, 47, 59, 71, 83, 95, 107, 119 };



		public static void 粗点計算(List<AnswerDetail> anserList, C01粗点計算_結果 c01結果)
		{
			//初期化();

			//string[] stra回答 = str回答.Split('\t');

			基礎因子別に粗点計算(anserList, mia基礎因子S_社会性, ref c01結果.粗点S_社会性, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子A_支配性, ref c01結果.粗点A_支配性, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子T_思考性, ref c01結果.粗点T_思考性, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子R_のん気, ref c01結果.粗点R_のん気, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子G_活動性, ref c01結果.粗点G_活動性, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子Ag_攻撃性, ref c01結果.粗点Ag_攻撃性, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子Co_協調性, ref c01結果.粗点Co_協調性, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子O_主観性, ref c01結果.粗点O_主観性, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子N_神経質, ref c01結果.粗点N_神経質, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子I_劣等感, ref c01結果.粗点I_劣等感, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子C_気分の変化, ref c01結果.粗点C_気分の変化, c01結果);
			基礎因子別に粗点計算(anserList, mia基礎因子D_抑うつ性, ref c01結果.粗点D_抑うつ性, c01結果);

			信頼度判定(c01結果);
		}



		private static void 基礎因子別に粗点計算(List<AnswerDetail> anserList, byte[] ia尺度, ref byte i粗点, C01粗点計算_結果 c01結果)
		{
			for (byte iCnt = 0; iCnt < ia尺度.Length; iCnt++)
			{
				if (anserList[ia尺度[iCnt]].回答 == 回答選択肢.はい)
				{
					i粗点 += 2;
					c01結果.mbytはいCnt++;
				}
				else if (anserList[ia尺度[iCnt]].回答 == 回答選択肢.どちらともいえない)
				{
					i粗点 += 1;
					c01結果.mbytどちらともいえないCnt++;
				}
				else if (anserList[ia尺度[iCnt]].回答 == 回答選択肢.いいえ)
				{
					c01結果.mbytいいえCnt++;
				}
				else if (anserList[ia尺度[iCnt]].回答 == 回答選択肢.訂正回答)
				{
					c01結果.mbyt訂正回答Cnt++;
				}
				else if (anserList[ia尺度[iCnt]].回答 == 回答選択肢.未回答)
				{
					c01結果.mbyt未回答Cnt++;
				}
			}
		}

		private static void 信頼度判定(C01粗点計算_結果 c01結果)
		{
			c01結果.mstr信頼度_固定 += "回答" + "\t" + "選択数" + "<br>";
			c01結果.mstr信頼度_固定 += "はい" + "\t" + c01結果.mbytはいCnt.ToString() + "<br>";
			c01結果.mstr信頼度_固定 += "どちらともいえない" + "\t" + c01結果.mbytどちらともいえないCnt.ToString() + "<br>";
			c01結果.mstr信頼度_固定 += "いいえ" + "\t" + c01結果.mbytいいえCnt.ToString() + "<br>";
			c01結果.mstr信頼度_固定 += "訂正回答" + "\t" + c01結果.mbyt訂正回答Cnt.ToString() + "<br>";
			c01結果.mstr信頼度_固定 += "未回答" + "\t" + c01結果.mbyt未回答Cnt.ToString() + "<br>";
			c01結果.mstr信頼度_固定 += "<br>";

			float f信頼度の低い設問数 = c01結果.mbyt訂正回答Cnt + c01結果.mbyt未回答Cnt + (c01結果.mbytどちらともいえないCnt / 2);
			c01結果.信頼度 = (byte)(100 - ((float)(f信頼度の低い設問数 / (float)120) * (float)100));

			c01結果.mstr信頼度_固定 += "信頼度" + "<br>" + c01結果.信頼度.ToString() + "<br>";

			//c01結果.信頼度 = 0;
		}


	}
}
