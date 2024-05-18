using System;
using System.Collections.Generic;
using System.Text;

namespace UnikktleMentorEngine
{
	public static class C03系統判定
	{
		private const string タイプの向き = "タイプの向き";
		private const string タイプの強さ = "タイプの強さ";
		private const string 情緒性 = "情緒性";
		private const string 社会適応性 = "社会適応性";
		private const string 向性 = "向性";
		private const string 一般的特徴 = "【一般的特徴】";
		private const string 平均 = "平均";
		private const string A類 = "A類";
		private const string B類 = "B類";
		private const string C類 = "C類";
		private const string D類 = "D類";
		private const string E類 = "E類";
		private const string F類 = "F類";
		private const string 不安定 = "不安定";
		private const string 不適応 = "不適応";
		private const string 外向 = "外向";
		private const string 安定 = "安定";
		private const string 適応 = "適応";
		private const string 内向 = "内向";
		private const string 適応または平均 = "適応または平均";
		private const string 不適応または平均 = "不適応または平均";
		private const string 不明 = "不明";
		private const string 強い = "強い";
		private const string 中程度 = "中程度";
		private const string 弱い = "弱い";
		private const string 平均型 = "平均型";
		private const string 挑戦者型 = "挑戦者型";
		private const string 平穏型 = "平穏型";
		private const string 管理者型 = "管理者型";
		private const string オタク型 = "オタク型";
		private const string A類典型 = "A類典型(A)";
		private const string B類典型 = "B類典型(B)";
		private const string C類典型 = "C類典型(C)";
		private const string D類典型 = "D類典型(D)";
		private const string E類典型 = "E類典型(E)";
		private const string A類準型 = "A類準型(A')";
		private const string B類準型 = "B類準型(B')";
		private const string C類準型 = "C類準型(C')";
		private const string D類準型 = "D類準型(D')";
		private const string E類準型 = "E類準型(E')";
		private const string A類混合型 = "A類混合型(A'')";
		private const string B類AB混合型 = "B類AB混合型(AB)";
		private const string C類AC混合型 = "C類AC混合型(AC)";
		private const string D類AD混合型 = "D類AD混合型(AD)";
		private const string E類AE混合型 = "E類AE混合型(AE)";
		private const string 目立たない平均的なタイプで主導性は弱い_知能の低い場合は平凡_無気力の人が多い = "目立たない平均的なタイプで主導性は弱い。知能の低い場合は平凡、無気力の人が多い。";
		private const string 不安定だが積極的_対人関係の面で問題を起こしやすい_知能が低い場合は特にその傾向が強い = "不安定だが積極的。対人関係の面で問題を起こしやすい。知能が低い場合は特にその傾向が強い。";
		private const string 安定だが消極的_平穏だが受動的であり_リーダーとして他人を引っ張っていく力は弱い = "安定だが消極的。平穏だが受動的であり、リーダーとして他人を引っ張っていく力は弱い。";
		private const string 安定で積極的_対人関係で問題を起こすことが少なく_行動が積極的だから_仕事の面でもリクリエーションの面でもリーダーに向いた性格 = "安定で積極的。対人関係で問題を起こすことが少なく、行動が積極的だから、仕事の面でもリクリエーションの面でもリーダーに向いた性格。";
		private const string 不安定で消極的_引込み思案で積極性に欠ける引きこもり型だが_自分自身の内面は趣味や教養で充実していることが多い = "不安定で消極的。引込み思案で積極性に欠ける「引きこもり型」だが、自分自身の内面は趣味や教養で充実していることが多い。";
		private const string 理論上はあり得ないありえない性格で_回答内容に極端な歪曲が入っているか_人格の整合性が失われている可能性があります = "理論上はあり得ないありえない性格で、回答内容に極端な歪曲が入っているか、人格の整合性が失われている可能性があります。";








		public static void 系統判定(C02系統値計算_結果 c02結果, C03系統判定_結果 c03系統結果)
		{
			//初期化();

			判定(c02結果, c03系統結果);

			c03系統結果.mstr固定 += "【系統判定】" + "<br>";
			//mstr判定結果 += "系統コード\t" + "類型\t" + "傾向\t" + "情緒性\t" + "社会適応性\t" + "向性\t" + "一般的特徴<br>";
			//mstr判定結果 += mstr系統コード + "\t" + mstr類型 + "\t" + mstr傾向;
			c03系統結果.mstr固定 += タイプの向き + "\t" + タイプの強さ + "\t" + 情緒性 + "\t" + 社会適応性 + "\t" 
				+ 向性 + "\t" + "<br>";
			c03系統結果.mstr固定 += c03系統結果.mstr類型 + "\t" + c03系統結果.mstr傾向;

			if (c03系統結果.mstr系統コード.IndexOf(A類) > -1)
			{
				//c03系統結果.mstr判定結果 += "\t" + ;
				c03系統結果.mstr固定 += "\t" + 平均;
				c03系統結果.mstr固定 += "\t" + 平均;
				c03系統結果.mstr固定 += "\t" + 平均;
				c03系統結果.mstr固定 += "<br><br>" + 一般的特徴 + "<br>" + 目立たない平均的なタイプで主導性は弱い_知能の低い場合は平凡_無気力の人が多い;
				c03系統結果.mstr固定 += "<br>";
			}
			else if (c03系統結果.mstr系統コード.IndexOf(B類) > -1)
			{
				//c03系統結果.mstr判定結果 += "\t" + 挑戦者型;
				c03系統結果.mstr固定 += "\t" + 不安定;
				c03系統結果.mstr固定 += "\t" + 不適応;
				c03系統結果.mstr固定 += "\t" + 外向;
				c03系統結果.mstr固定 += "<br><br>" + 一般的特徴 + "<br>" + 不安定だが積極的_対人関係の面で問題を起こしやすい_知能が低い場合は特にその傾向が強い;
				c03系統結果.mstr固定 += "<br>";
			}
			else if (c03系統結果.mstr系統コード.IndexOf(C類) > -1)
			{
				//c03系統結果.mstr判定結果 += "\t" + 平穏型;
				c03系統結果.mstr固定 += "\t" + 安定;
				c03系統結果.mstr固定 += "\t" + 適応;
				c03系統結果.mstr固定 += "\t" + 内向;
				c03系統結果.mstr固定 += "<br><br>" + 一般的特徴 + "<br>" + 安定だが消極的_平穏だが受動的であり_リーダーとして他人を引っ張っていく力は弱い;
				c03系統結果.mstr固定 += "<br>";
			}
			else if (c03系統結果.mstr系統コード.IndexOf(D類) > -1)
			{
				//c03系統結果.mstr判定結果 += "\t" + 管理者型;
				c03系統結果.mstr固定 += "\t" + 安定;
				c03系統結果.mstr固定 += "\t" + 適応または平均;
				c03系統結果.mstr固定 += "\t" + 外向;
				c03系統結果.mstr固定 += "<br><br>" + 一般的特徴 + "<br>" + 安定で積極的_対人関係で問題を起こすことが少なく_行動が積極的だから_仕事の面でもリクリエーションの面でもリーダーに向いた性格;
				c03系統結果.mstr固定 += "<br>";
			}
			else if (c03系統結果.mstr系統コード.IndexOf(E類) > -1)
			{
				//c03系統結果.mstr判定結果 += "\t" + オタク型;
				c03系統結果.mstr固定 += "\t" + 不安定;
				c03系統結果.mstr固定 += "\t" + 不適応または平均;
				c03系統結果.mstr固定 += "\t" + 内向;
				c03系統結果.mstr固定 += "<br><br>" + 一般的特徴 + "<br>" + 不安定で消極的_引込み思案で積極性に欠ける引きこもり型だが_自分自身の内面は趣味や教養で充実していることが多い;
				c03系統結果.mstr固定 += "<br>";
			}
			else if (c03系統結果.mstr系統コード.IndexOf(F類) > -1)
			{
				//c03系統結果.mstr判定結果 += "\t" + "診断不可";
				c03系統結果.mstr固定 += "\t" + 不明;
				c03系統結果.mstr固定 += "\t" + 不明;
				c03系統結果.mstr固定 += "\t" + 不明;
				c03系統結果.mstr固定 += "<br><br>" + 一般的特徴 + "<br>" + 理論上はあり得ないありえない性格で_回答内容に極端な歪曲が入っているか_人格の整合性が失われている可能性があります;
				c03系統結果.mstr固定 += "<br>";
			}

			//c03系統結果.mstr固定 += "\t" + C01粗点計算.mi信頼度.ToString();

		}



		private static void 判定(C02系統値計算_結果 c02結果, C03系統判定_結果 c03系統結果)
		{
			if (典型判定(c02結果, c03系統結果) == true)
				return;

			if (準型判定(c02結果, c03系統結果) == true)
				return;

			if (混合型判定(c02結果, c03系統結果) == true)
				return;

			c03系統結果.mstr系統コード += F類;
			c03系統結果.mstr類型 += "矛盾型";
			c03系統結果.mstr傾向 += "不明";
		}

		private static bool 典型判定(C02系統値計算_結果 c02結果, C03系統判定_結果 c03系統結果)
		{
			if (c02結果.mi系統値A > c02結果.mi系統値B &&
				c02結果.mi系統値A > c02結果.mi系統値C &&
				c02結果.mi系統値A > c02結果.mi系統値D &&
				c02結果.mi系統値A > c02結果.mi系統値E &&
				9 <= c02結果.mi系統値A)
			{
				c03系統結果.mstr系統コード += A類典型;
				c03系統結果.mstr類型 += 平均型;
				c03系統結果.mstr傾向 += 強い;
				return true;
			}

			if (c02結果.mi系統値B > c02結果.mi系統値A &&
				c02結果.mi系統値B > c02結果.mi系統値C &&
				c02結果.mi系統値B > c02結果.mi系統値D &&
				c02結果.mi系統値B > c02結果.mi系統値E &&
				8 <= c02結果.mi系統値B)
			{
				c03系統結果.mstr系統コード += B類典型;
				c03系統結果.mstr類型 += 挑戦者型;
				c03系統結果.mstr傾向 += 強い;
				return true;
			}

			if (c02結果.mi系統値C > c02結果.mi系統値A &&
				c02結果.mi系統値C > c02結果.mi系統値B &&
				c02結果.mi系統値C > c02結果.mi系統値D &&
				c02結果.mi系統値C > c02結果.mi系統値E &&
				7 <= c02結果.mi系統値C)
			{
				c03系統結果.mstr系統コード += C類典型;
				c03系統結果.mstr類型 += 平穏型;
				c03系統結果.mstr傾向 += 強い;
				return true;
			}

			if (c02結果.mi系統値D > c02結果.mi系統値A &&
				c02結果.mi系統値D > c02結果.mi系統値B &&
				c02結果.mi系統値D > c02結果.mi系統値C &&
				c02結果.mi系統値D > c02結果.mi系統値E &&
				9 <= c02結果.mi系統値D)
			{
				c03系統結果.mstr系統コード += D類典型;
				c03系統結果.mstr類型 += 管理者型;
				c03系統結果.mstr傾向 += 強い;
				return true;
			}

			if (c02結果.mi系統値E > c02結果.mi系統値A &&
				c02結果.mi系統値E > c02結果.mi系統値B &&
				c02結果.mi系統値E > c02結果.mi系統値C &&
				c02結果.mi系統値E > c02結果.mi系統値D &&
				9 <= c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += E類典型;
				c03系統結果.mstr類型 += オタク型;
				c03系統結果.mstr傾向 += 強い;
				return true;
			}

			return false;
		}

		private static bool 準型判定(C02系統値計算_結果 c02結果, C03系統判定_結果 c03系統結果)
		{
			//A類準型　単純最大
			if (c02結果.mi系統値A > c02結果.mi系統値B &&
				c02結果.mi系統値A > c02結果.mi系統値C &&
				c02結果.mi系統値A > c02結果.mi系統値D &&
				c02結果.mi系統値A > c02結果.mi系統値E &&
				8 == c02結果.mi系統値A)
			{
				c03系統結果.mstr系統コード += A類準型;
				c03系統結果.mstr類型 += 平均型;
				c03系統結果.mstr傾向 += 中程度;
				return true;
			}


			//B類準型　単純最大
			if (c02結果.mi系統値B > c02結果.mi系統値A &&
				c02結果.mi系統値B > c02結果.mi系統値C &&
				c02結果.mi系統値B > c02結果.mi系統値D &&
				c02結果.mi系統値B > c02結果.mi系統値E &&
				5 <= c02結果.mi系統値B && c02結果.mi系統値B <= 7)
			{
				c03系統結果.mstr系統コード += B類準型;
				c03系統結果.mstr類型 += 挑戦者型;
				c03系統結果.mstr傾向 += 中程度;
				return true;
			}

			//B類準型　複数最大
			if (c02結果.mi系統値B == c02結果.mi系統値E &&
				c02結果.mi系統値B > c02結果.mi系統値A &&
				c02結果.mi系統値B > c02結果.mi系統値C &&
				c02結果.mi系統値B > c02結果.mi系統値D &&
				6 <= c02結果.mi系統値B && c02結果.mi系統値B <= 8)
			{
				c03系統結果.mstr系統コード += B類準型;
				c03系統結果.mstr類型 += 挑戦者型;
				c03系統結果.mstr傾向 += 中程度;
				return true;
			}

			//B類準型　複数最大
			if (c02結果.mi系統値B == c02結果.mi系統値A &&
				c02結果.mi系統値B > c02結果.mi系統値C &&
				c02結果.mi系統値B > c02結果.mi系統値D &&
				c02結果.mi系統値B > c02結果.mi系統値E &&
				6 == c02結果.mi系統値B)
			{
				c03系統結果.mstr系統コード += B類準型;
				c03系統結果.mstr類型 += 挑戦者型;
				c03系統結果.mstr傾向 += 中程度;
				return true;
			}


			//C類準型　単純最大
			if (c02結果.mi系統値C > c02結果.mi系統値A &&
				c02結果.mi系統値C > c02結果.mi系統値B &&
				c02結果.mi系統値C > c02結果.mi系統値D &&
				c02結果.mi系統値C > c02結果.mi系統値E &&
				5 <= c02結果.mi系統値C && c02結果.mi系統値C <= 6)
			{
				c03系統結果.mstr系統コード += C類準型;
				c03系統結果.mstr類型 += 平穏型;
				c03系統結果.mstr傾向 += 中程度;
				return true;
			}

			//C類準型　複数最大
			if (c02結果.mi系統値C == c02結果.mi系統値E &&
				c02結果.mi系統値C > c02結果.mi系統値A &&
				c02結果.mi系統値C > c02結果.mi系統値B &&
				c02結果.mi系統値C > c02結果.mi系統値D &&
				6 <= c02結果.mi系統値C && c02結果.mi系統値C <= 8)
			{
				c03系統結果.mstr系統コード += C類準型;
				c03系統結果.mstr類型 += 平穏型;
				c03系統結果.mstr傾向 += 中程度;
				return true;
			}

			//C類準型　複数最大
			if (c02結果.mi系統値C == c02結果.mi系統値A &&
				c02結果.mi系統値C > c02結果.mi系統値B &&
				c02結果.mi系統値C > c02結果.mi系統値D &&
				c02結果.mi系統値C > c02結果.mi系統値E &&
				6 == c02結果.mi系統値C)
			{
				c03系統結果.mstr系統コード += C類準型;
				c03系統結果.mstr類型 += 平穏型;
				c03系統結果.mstr傾向 += 中程度;
				return true;
			}


			//D類準型　単純最大
			if (c02結果.mi系統値D > c02結果.mi系統値A &&
				c02結果.mi系統値D > c02結果.mi系統値B &&
				c02結果.mi系統値D > c02結果.mi系統値C &&
				c02結果.mi系統値D > c02結果.mi系統値E &&
				5 <= c02結果.mi系統値D && c02結果.mi系統値D <= 8)
			{
				c03系統結果.mstr系統コード += D類準型;
				c03系統結果.mstr類型 += 管理者型;
				c03系統結果.mstr傾向 += 中程度;
				return true;
			}


			//E類準型　単純最大
			if (c02結果.mi系統値E > c02結果.mi系統値A &&
				c02結果.mi系統値E > c02結果.mi系統値B &&
				c02結果.mi系統値E > c02結果.mi系統値C &&
				c02結果.mi系統値E > c02結果.mi系統値D &&
				5 <= c02結果.mi系統値E && c02結果.mi系統値E <= 8)
			{
				c03系統結果.mstr系統コード += E類準型;
				c03系統結果.mstr類型 += オタク型;
				c03系統結果.mstr傾向 += 中程度;
				return true;
			}

			////複数最大
			//if (c02結果.mi系統値B == c02結果.mi系統値E &&
			//    c02結果.mi系統値B > c02結果.mi系統値A &&
			//    c02結果.mi系統値B > c02結果.mi系統値C &&
			//    c02結果.mi系統値B > c02結果.mi系統値D &&
			//    6 <= c02結果.mi系統値B && c02結果.mi系統値B <= 8)
			//{
			//    mstr系統コード += B類準型;
			//    mstr類型 += 挑戦者型;
			//    mstr傾向 += 中程度;
			//    return true;
			//}
			
			//if (c02結果.mi系統値B == c02結果.mi系統値A &&
			//    c02結果.mi系統値B > c02結果.mi系統値C &&
			//    c02結果.mi系統値B > c02結果.mi系統値D &&
			//    c02結果.mi系統値B > c02結果.mi系統値E &&
			//    6 == c02結果.mi系統値B)
			//{
			//    mstr系統コード += B類準型;
			//    mstr類型 += 挑戦者型;
			//    mstr傾向 += 中程度;
			//    return true;
			//}

			////C類準型　複数最大
			//if (c02結果.mi系統値C == c02結果.mi系統値E &&
			//    c02結果.mi系統値C > c02結果.mi系統値A &&
			//    c02結果.mi系統値C > c02結果.mi系統値B &&
			//    c02結果.mi系統値C > c02結果.mi系統値D &&
			//    6 <= c02結果.mi系統値C)
			//{
			//    mstr系統コード += C類準型;
			//    mstr類型 += 平穏型;
			//    mstr傾向 += 中程度;
			//    return true;
			//}

			////C類準型　複数最大
			//if (c02結果.mi系統値C == c02結果.mi系統値A &&
			//    c02結果.mi系統値C > c02結果.mi系統値B &&
			//    c02結果.mi系統値C > c02結果.mi系統値D &&
			//    c02結果.mi系統値C > c02結果.mi系統値E &&
			//    6 <= c02結果.mi系統値C)
			//{
			//    mstr系統コード += C類準型;
			//    mstr類型 += 平穏型;
			//    mstr傾向 += 中程度;
			//    return true;
			//}

			return false;
		}

		private static bool 混合型判定(C02系統値計算_結果 c02結果, C03系統判定_結果 c03系統結果)
		{
			//A類混合型(A'')
			if (5 <= c02結果.mi系統値A && c02結果.mi系統値A <= 7 &&
				5 > c02結果.mi系統値B &&
				5 > c02結果.mi系統値C &&
				5 > c02結果.mi系統値D &&
				5 > c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += A類混合型;
				c03系統結果.mstr類型 += 平均型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}
			
			if (4 == c02結果.mi系統値A &&
				4 == c02結果.mi系統値B &&
				4 == c02結果.mi系統値C &&
				4 == c02結果.mi系統値D &&
				4 == c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += A類混合型;
				c03系統結果.mstr類型 += 平均型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			//B類AB混合型
			if (6 <= c02結果.mi系統値A && c02結果.mi系統値A <= 7 &&
				5 == c02結果.mi系統値B &&
				5 == c02結果.mi系統値D &&
				5 > c02結果.mi系統値C &&
				5 > c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += B類AB混合型;
				c03系統結果.mstr類型 += 挑戦者型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			if (5 <= c02結果.mi系統値A && c02結果.mi系統値A <= 7 &&
				5 == c02結果.mi系統値B &&
				5 > c02結果.mi系統値C &&
				5 > c02結果.mi系統値D &&
				5 > c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += B類AB混合型;
				c03系統結果.mstr類型 += 挑戦者型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			if (5 <= c02結果.mi系統値B && c02結果.mi系統値B <= 7 &&
				c02結果.mi系統値B == c02結果.mi系統値D &&
				c02結果.mi系統値B > c02結果.mi系統値A &&
				c02結果.mi系統値B > c02結果.mi系統値C &&
				c02結果.mi系統値B > c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += B類AB混合型;
				c03系統結果.mstr類型 += 挑戦者型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}
			
			//C類AC混合型
			if (6 <= c02結果.mi系統値A && c02結果.mi系統値A <= 7 &&
				5 == c02結果.mi系統値C &&
				5 == c02結果.mi系統値D &&
				5 > c02結果.mi系統値B &&
				5 > c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += C類AC混合型;
				c03系統結果.mstr類型 += 平穏型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			if (5 <= c02結果.mi系統値A && c02結果.mi系統値A <= 7 &&
				5 <= c02結果.mi系統値C &&
				5 > c02結果.mi系統値B &&
				5 > c02結果.mi系統値D &&
				5 > c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += C類AC混合型;
				c03系統結果.mstr類型 += 平穏型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			if (5 <= c02結果.mi系統値C && c02結果.mi系統値C <= 7 && 
				c02結果.mi系統値C == c02結果.mi系統値D &&
				c02結果.mi系統値C > c02結果.mi系統値A &&
				c02結果.mi系統値C > c02結果.mi系統値B &&
				c02結果.mi系統値C > c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += C類AC混合型;
				c03系統結果.mstr類型 += 平穏型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			//D類AD混合型
			if (5 <= c02結果.mi系統値A && c02結果.mi系統値A <= 6 && 
				c02結果.mi系統値A == c02結果.mi系統値D &&
				c02結果.mi系統値A > c02結果.mi系統値B &&
				c02結果.mi系統値A > c02結果.mi系統値C &&
				c02結果.mi系統値A > c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += D類AD混合型;
				c03系統結果.mstr類型 += 管理者型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			if (5 <= c02結果.mi系統値A && c02結果.mi系統値A <= 7 &&
				5 <= c02結果.mi系統値D &&
				5 > c02結果.mi系統値B &&
				5 > c02結果.mi系統値C &&
				5 > c02結果.mi系統値E)
			{
				c03系統結果.mstr系統コード += D類AD混合型;
				c03系統結果.mstr類型 += 管理者型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			//E類AE混合型
			if (5 <= c02結果.mi系統値A && c02結果.mi系統値A <= 6 && 
				c02結果.mi系統値A == c02結果.mi系統値E &&
				c02結果.mi系統値A > c02結果.mi系統値B &&
				c02結果.mi系統値A > c02結果.mi系統値C &&
				c02結果.mi系統値A > c02結果.mi系統値D)
			{
				c03系統結果.mstr系統コード += E類AE混合型;
				c03系統結果.mstr類型 += オタク型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			if (5 <= c02結果.mi系統値A && c02結果.mi系統値A <= 7 &&
				5 <= c02結果.mi系統値E &&
				5 > c02結果.mi系統値B &&
				5 > c02結果.mi系統値C &&
				5 > c02結果.mi系統値D)
			{
				c03系統結果.mstr系統コード += E類AE混合型;
				c03系統結果.mstr類型 += オタク型;
				c03系統結果.mstr傾向 += 弱い;
				return true;
			}

			return false;
		}


	}
}
