using System;
using System.Collections.Generic;
using System.Text;

namespace UnikktleMentorEngine
{
	public static class C00Common
	{
		public static bool ChkDCINで突出がある(C02系統値計算_結果 c02結果)
		{
			int i最大値 = c02結果.標準点D_抑うつ性;
			int i最小値 = c02結果.標準点D_抑うつ性;

			if (i最大値 < c02結果.標準点C_気分の変化)
				i最大値 = c02結果.標準点C_気分の変化;
			else if (i最小値 > c02結果.標準点C_気分の変化)
				i最小値 = c02結果.標準点C_気分の変化;

			if (i最大値 < c02結果.標準点I_劣等感)
				i最大値 = c02結果.標準点I_劣等感;
			else if (i最小値 > c02結果.標準点I_劣等感)
				i最小値 = c02結果.標準点I_劣等感;

			if (i最大値 < c02結果.標準点N_神経質)
				i最大値 = c02結果.標準点N_神経質;
			else if (i最小値 > c02結果.標準点N_神経質)
				i最小値 = c02結果.標準点N_神経質;

			if (1 < (i最大値 - i最小値))
				return true;

			return false;
		}
	}
}
