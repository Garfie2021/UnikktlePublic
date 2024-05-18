using System;
using System.Collections.Generic;
using System.Text;

namespace UnikktleMentorEngine
{
	public static class C03因子得点
	{
		private const string 因子 = "因子";
		private const string 得点 = "得点";
		private const string 抑うつ性 = "抑うつ性";
		private const string 気分の変化 = "気分の変化";
		private const string 劣等感 = "劣等感";
		private const string 神経質 = "神経質";
		private const string 主観性 = "主観性";
		private const string 協調性 = "協調性";
		private const string 攻撃性 = "攻撃性";
		private const string 活動性 = "活動性";
		private const string のん気 = "のん気";
		private const string 思考性 = "思考性";
		private const string 支配性 = "支配性";
		private const string 社会性 = "社会性";



		public static void 因子得点(C02系統値計算_結果 c02結果, C03因子得点_結果 c03因子結果)
		{
			c03因子結果.mstr固定 = "【因子得点】" + "<br>";
			c03因子結果.mstr固定 += 因子 + "\t" + 得点 + "<br>";

			c03因子結果.mstr固定 += 抑うつ性 + "\t" + c02結果.標準点D_抑うつ性 + "<br>";
			c03因子結果.mstr固定 += 気分の変化 + "\t" + c02結果.標準点C_気分の変化 + "<br>";
			c03因子結果.mstr固定 += 劣等感 + "\t" + c02結果.標準点I_劣等感 + "<br>";
			c03因子結果.mstr固定 += 神経質 + "\t" + c02結果.標準点N_神経質 + "<br>";
			c03因子結果.mstr固定 += 主観性 + "\t" + c02結果.標準点O_主観性 + "<br>";
			c03因子結果.mstr固定 += 協調性 + "\t" + c02結果.標準点Co_協調性 + "<br>";
			c03因子結果.mstr固定 += 攻撃性 + "\t" + c02結果.標準点Ag_攻撃性 + "<br>";
			c03因子結果.mstr固定 += 活動性 + "\t" + c02結果.標準点G_活動性 + "<br>";
			c03因子結果.mstr固定 += のん気 + "\t" + c02結果.標準点R_のん気 + "<br>";
			c03因子結果.mstr固定 += 思考性 + "\t" + c02結果.標準点T_思考性 + "<br>";
			c03因子結果.mstr固定 += 支配性 + "\t" + c02結果.標準点A_支配性 + "<br>";
			c03因子結果.mstr固定 += 社会性 + "\t" + c02結果.標準点S_社会性 + "<br>";
			c03因子結果.mstr固定 += "※最小値：1　最大値：5　値が大きいほどその因子の傾向が強い事を意味します。" + "\t" + "<br>";
		}

	}
}
