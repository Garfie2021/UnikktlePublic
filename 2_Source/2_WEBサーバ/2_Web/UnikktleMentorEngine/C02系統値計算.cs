using System;
using System.Collections.Generic;
using System.Text;
using UnikktleMentorEngine;

namespace UnikktleMentorEngine
{
	public static class C02系統値計算
	{
		//private const string 男性 = "男性";

		private static byte[,] mia男性打点D_抑うつ性 = new byte[,] { { 0, 2 }, { 3, 8 }, { 9, 14 }, { 15, 19 }, { 20, 20 } };
		private static byte[,] mia男性打点C_気分の変化 = new byte[,] { { 0, 2 }, { 3, 7 }, { 8, 12 }, { 13, 17 }, { 18, 20 } };
		private static byte[,] mia男性打点I_劣等感 = new byte[,] { { 0, 0 }, { 1, 5 }, { 6, 12 }, { 13, 17 }, { 18, 20 } };
		private static byte[,] mia男性打点N_神経質 = new byte[,] { { 0, 1 }, { 2, 6 }, { 7, 12 }, { 13, 17 }, { 18, 20 } };
		private static byte[,] mia男性打点O_主観性 = new byte[,] { { 0, 1 }, { 2, 5 }, { 6, 10 }, { 11, 14 }, { 15, 20 } };
		private static byte[,] mia男性打点Co_協調性 = new byte[,] { { 0, 2 }, { 3, 6 }, { 7, 10 }, { 11, 14 }, { 15, 20 } };
		private static byte[,] mia男性打点Ag_攻撃性 = new byte[,] { { 0, 4 }, { 5, 8 }, { 9, 13 }, { 14, 17 }, { 18, 20 } };
		private static byte[,] mia男性打点G_活動性 = new byte[,] { { 0, 2 }, { 3, 8 }, { 9, 13 }, { 14, 18 }, { 19, 20 } };
		private static byte[,] mia男性打点R_のん気 = new byte[,] { { 0, 2 }, { 3, 7 }, { 8, 12 }, { 13, 17 }, { 18, 20 } };
		private static byte[,] mia男性打点T_思考性 = new byte[,] { { 0, 1 }, { 2, 5 }, { 6, 10 }, { 11, 15 }, { 16, 20 } };
		private static byte[,] mia男性打点A_支配性 = new byte[,] { { 0, 0 }, { 1, 4 }, { 5, 11 }, { 12, 17 }, { 18, 20 } };
		private static byte[,] mia男性打点S_社会性 = new byte[,] { { 0, 1 }, { 2, 6 }, { 7, 13 }, { 14, 18 }, { 19, 20 } };

		private static byte[,] mia女性打点D_抑うつ性 = new byte[,] { { 0, 2 }, { 3, 8 }, { 9, 15 }, { 16, 17 }, { 18, 20 } };
		private static byte[,] mia女性打点C_気分の変化 = new byte[,] { { 0, 2 }, { 3, 7 }, { 8, 13 }, { 14, 17 }, { 18, 20 } };
		private static byte[,] mia女性打点I_劣等感 = new byte[,] { { 0, 1 }, { 2, 5 }, { 6, 12 }, { 13, 17 }, { 18, 20 } };
		private static byte[,] mia女性打点N_神経質 = new byte[,] { { 0, 2 }, { 3, 7 }, { 8, 12 }, { 13, 17 }, { 18, 20 } };
		private static byte[,] mia女性打点O_主観性 = new byte[,] { { 0, 2 }, { 3, 5 }, { 6, 10 }, { 11, 14 }, { 15, 20 } };
		private static byte[,] mia女性打点Co_協調性 = new byte[,] { { 0, 1 }, { 2, 4 }, { 5,  8 }, {  9, 13 }, { 14, 20 } };
		private static byte[,] mia女性打点Ag_攻撃性 = new byte[,] { { 0, 3 }, { 4, 8 }, { 9, 12 }, { 13, 16 }, { 17, 20 } };
		private static byte[,] mia女性打点G_活動性 = new byte[,] { { 0, 2 }, { 3, 7 }, { 8, 13 }, { 14, 18 }, { 19, 20 } };
		private static byte[,] mia女性打点R_のん気 = new byte[,] { { 0, 2 }, { 3, 6 }, { 7, 11 }, { 12, 16 }, { 17, 20 } };
		private static byte[,] mia女性打点T_思考性 = new byte[,] { { 0, 1 }, { 2, 5 }, { 6, 10 }, { 11, 15 }, { 16, 20 } };
		private static byte[,] mia女性打点A_支配性 = new byte[,] { { 0, 1 }, { 2, 4 }, { 5, 11 }, { 12, 17 }, { 18, 20 } };
		private static byte[,] mia女性打点S_社会性 = new byte[,] { { 0, 2 }, { 3, 7 }, { 8, 13 }, { 14, 18 }, { 19, 20 } };



		public static void 系統値計算(Gender gender, C01粗点計算_結果 c01結果, C02系統値計算_結果 c02結果)
		{
			//初期化();

			標準点計算(gender, c01結果, c02結果);
			打点数計算(c02結果);
			系統値計算2(c02結果);
		}

		public static void 標準点計算(Gender gender, C01粗点計算_結果 c01結果, C02系統値計算_結果 c02結果)
		{
			if (gender == Gender.Male)
			{
				標準点計算(c01結果.粗点D_抑うつ性, mia男性打点D_抑うつ性, ref c02結果.標準点D_抑うつ性);
				標準点計算(c01結果.粗点C_気分の変化, mia男性打点C_気分の変化, ref c02結果.標準点C_気分の変化);
				標準点計算(c01結果.粗点I_劣等感, mia男性打点I_劣等感, ref c02結果.標準点I_劣等感);
				標準点計算(c01結果.粗点N_神経質, mia男性打点N_神経質, ref c02結果.標準点N_神経質);
				標準点計算(c01結果.粗点O_主観性, mia男性打点O_主観性, ref c02結果.標準点O_主観性);
				標準点計算(c01結果.粗点Co_協調性, mia男性打点Co_協調性, ref c02結果.標準点Co_協調性);
				標準点計算(c01結果.粗点Ag_攻撃性, mia男性打点Ag_攻撃性, ref c02結果.標準点Ag_攻撃性);
				標準点計算(c01結果.粗点G_活動性, mia男性打点G_活動性, ref c02結果.標準点G_活動性);
				標準点計算(c01結果.粗点R_のん気, mia男性打点R_のん気, ref c02結果.標準点R_のん気);
				標準点計算(c01結果.粗点T_思考性, mia男性打点T_思考性, ref c02結果.標準点T_思考性);
				標準点計算(c01結果.粗点A_支配性, mia男性打点A_支配性, ref c02結果.標準点A_支配性);
				標準点計算(c01結果.粗点S_社会性, mia男性打点S_社会性, ref c02結果.標準点S_社会性);
			}
			else
			{
				標準点計算(c01結果.粗点D_抑うつ性, mia女性打点D_抑うつ性, ref c02結果.標準点D_抑うつ性);
				標準点計算(c01結果.粗点C_気分の変化, mia女性打点C_気分の変化, ref c02結果.標準点C_気分の変化);
				標準点計算(c01結果.粗点I_劣等感, mia女性打点I_劣等感, ref c02結果.標準点I_劣等感);
				標準点計算(c01結果.粗点N_神経質, mia女性打点N_神経質, ref c02結果.標準点N_神経質);
				標準点計算(c01結果.粗点O_主観性, mia女性打点O_主観性, ref c02結果.標準点O_主観性);
				標準点計算(c01結果.粗点Co_協調性, mia女性打点Co_協調性, ref c02結果.標準点Co_協調性);
				標準点計算(c01結果.粗点Ag_攻撃性, mia女性打点Ag_攻撃性, ref c02結果.標準点Ag_攻撃性);
				標準点計算(c01結果.粗点G_活動性, mia女性打点G_活動性, ref c02結果.標準点G_活動性);
				標準点計算(c01結果.粗点R_のん気, mia女性打点R_のん気, ref c02結果.標準点R_のん気);
				標準点計算(c01結果.粗点T_思考性, mia女性打点T_思考性, ref c02結果.標準点T_思考性);
				標準点計算(c01結果.粗点A_支配性, mia女性打点A_支配性, ref c02結果.標準点A_支配性);
				標準点計算(c01結果.粗点S_社会性, mia女性打点S_社会性, ref c02結果.標準点S_社会性);
			}
		}

		public static void 打点数計算(C02系統値計算_結果 c02結果)
		{
			打点数計算_DからCo(c02結果.標準点D_抑うつ性, c02結果);
			打点数計算_DからCo(c02結果.標準点C_気分の変化, c02結果);
			打点数計算_DからCo(c02結果.標準点I_劣等感, c02結果);
			打点数計算_DからCo(c02結果.標準点N_神経質, c02結果);
			打点数計算_DからCo(c02結果.標準点O_主観性, c02結果);
			打点数計算_DからCo(c02結果.標準点Co_協調性, c02結果);
			打点数計算_AgからS(c02結果.標準点Ag_攻撃性, c02結果);
			打点数計算_AgからS(c02結果.標準点G_活動性, c02結果);
			打点数計算_AgからS(c02結果.標準点R_のん気, c02結果);
			打点数計算_AgからS(c02結果.標準点T_思考性, c02結果);
			打点数計算_AgからS(c02結果.標準点A_支配性, c02結果);
			打点数計算_AgからS(c02結果.標準点S_社会性, c02結果);
		}

		public static void 系統値計算2(C02系統値計算_結果 c02結果)
		{
			c02結果.mi系統値A = (byte)(c02結果.miブロック別打点数Ⅴ + c02結果.miブロック別打点数Ⅵ);
			c02結果.mi系統値B = (byte)(c02結果.miブロック別打点数Ⅲ + c02結果.miブロック別打点数Ⅳ);
			c02結果.mi系統値C = (byte)(c02結果.miブロック別打点数Ⅰ + c02結果.miブロック別打点数Ⅱ);
			c02結果.mi系統値D = (byte)(c02結果.miブロック別打点数Ⅰ + c02結果.miブロック別打点数Ⅳ);
			c02結果.mi系統値E = (byte)(c02結果.miブロック別打点数Ⅲ + c02結果.miブロック別打点数Ⅱ);
		}


		private static void 標準点計算(byte i粗点, byte[,] ia打点, ref byte i標準点)
		{
			for (byte iCnt = 0; iCnt < ia打点.Length; iCnt++)
			{
				if (ia打点[iCnt, 0] <= i粗点 && i粗点 <= ia打点[iCnt, 1])
				{
					i標準点 = (byte)(iCnt + 1);
					return;
				}
			}
		}

		private static void 打点数計算_DからCo(byte i標準点, C02系統値計算_結果 c02結果)
		{
			if (1 <= i標準点 && i標準点 <= 2)
				c02結果.miブロック別打点数Ⅰ++;
			else if (3 == i標準点)
				c02結果.miブロック別打点数Ⅴ++;
			else
				c02結果.miブロック別打点数Ⅲ++;
		}

		private static void 打点数計算_AgからS(byte i標準点, C02系統値計算_結果 c02結果)
		{
			if (1 <= i標準点 && i標準点 <= 2)
				c02結果.miブロック別打点数Ⅱ++;
			else if (3 == i標準点)
				c02結果.miブロック別打点数Ⅵ++;
			else
				c02結果.miブロック別打点数Ⅳ++;
		}

	}
}
