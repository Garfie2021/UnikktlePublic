﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UnikktleMentorEngine
{
	public static class C10リーダー資質判定
	{
		public static void リーダー資質判定(C02系統値計算_結果 c02結果, C07集合因子判定_結果 c07結果, C10リーダー資質判定_結果 c10結果)
		{
			//初期化();

			c10結果.mstr固定 += "【リーダー資質判定】" + "<br>";
			c10結果.mstr固定 += "因子" + "\t" + "傾向" + "<br>";

			c10結果.mstr固定 += "気力" + "\t" + 気力(c02結果) + "<br>";
			c10結果.mstr固定 += "決断力" + "\t" + 決断力(c02結果) + "<br>";
			c10結果.mstr固定 += "行動力" + "\t" + 行動力(c02結果) + "<br>";
			c10結果.mstr固定 += "指導者意識" + "\t" + 指導者意識(c02結果) + "<br>";

			c10結果.mstr固定 += "仕事へ取組む姿勢が積極的" + "\t" + 仕事へ取組む姿勢が積極的(c02結果) + "<br>";
			//c10結果.mstr固定 += "指導者意識が旺盛" + "\t" + 指導者意識が旺盛() + "<br>";
			c10結果.mstr固定 += "人間関係が解放的" + "\t" + 人間関係が解放的(c02結果) + "<br>";
			c10結果.mstr固定 += "仕事を取り巻く状況を、冷静に客観的に判断できる" + "\t" + 仕事を取り巻く状況を冷静に客観的に判断できる(c02結果) + "<br>";
			c10結果.mstr固定 += "果断な意思決定ができる" + "\t" + 果断な意思決定ができる(c02結果) + "<br>";
			c10結果.mstr固定 += "アイディアに優れている" + "\t" + アイディアに優れている(c02結果) + "<br>";
			c10結果.mstr固定 += "適度な内省性をそなえている" + "\t" + 適度な内省性をそなえている(c02結果) + "<br>";
			c10結果.mstr固定 += "部下に対する細かい配慮ができる" + "\t" + 部下に対する細かい配慮ができる(c02結果) + "<br>";
			c10結果.mstr固定 += "<br>";

			c10結果.mstr可変_タイプ += "【リーダータイプ判定】" + "<br>";
			c10結果.mstr可変_タイプ += リーダータイプ判定(c02結果, c07結果) + "<br>";

			//mstr可変_補足 += "【リーダーシップ判定補足】" + "<br>";
			//mstr可変_補足 += リーダーシップ判定補足() + "<br>";

			//mstr可変_阻害因子 += "【リーダーシップ阻害因子判定】" + "<br>";
			//mstr可変_阻害因子 += リーダーシップ阻害因子判定() + "<br>";
			リーダーシップ阻害因子判定(c02結果, c10結果);
		}


		public static void 阻害因子を追加(string str阻害因子, C10リーダー資質判定_結果 c10結果)
		{
			if (0 > c10結果.mstr可変_阻害因子.IndexOf(str阻害因子))
			{
				c10結果.mstr可変_阻害因子 += str阻害因子 + "<br>";
				c10結果.mi阻害因子Cnt++;
			}
		}



		private static string 気力(C02系統値計算_結果 c02結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
				return "○";

			return "";
		}

		private static string 決断力(C02系統値計算_結果 c02結果)
		{
			if (3 < c02結果.標準点R_のん気)
				return "○";

			return "";
		}

		private static string 行動力(C02系統値計算_結果 c02結果)
		{
			if (3 < c02結果.標準点G_活動性)
				return "○";

			return "";
		}

		private static string 指導者意識(C02系統値計算_結果 c02結果)
		{
			if (3 < c02結果.標準点A_支配性 ||
				3 < c02結果.標準点S_社会性)
				return "○";

			return "";
		}

		private static string 仕事へ取組む姿勢が積極的(C02系統値計算_結果 c02結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性 ||
				3 < c02結果.標準点G_活動性)
				return "○";

			return "";
		}

		private static string 人間関係が解放的(C02系統値計算_結果 c02結果)
		{
			if (3 > c02結果.標準点O_主観性 ||
				3 > c02結果.標準点Co_協調性)
				return "○";

			return "";
		}

		private static string 仕事を取り巻く状況を冷静に客観的に判断できる(C02系統値計算_結果 c02結果)
		{
			if (3 > c02結果.標準点D_抑うつ性 ||
				3 > c02結果.標準点C_気分の変化 ||
				3 > c02結果.標準点O_主観性)
				return "○";

			return "";
		}

		private static string 果断な意思決定ができる(C02系統値計算_結果 c02結果)
		{
			if (c02結果.標準点R_のん気 > c02結果.標準点G_活動性)
				return "○";

			return "";
		}

		private static string アイディアに優れている(C02系統値計算_結果 c02結果)
		{
			if ((3 <= c02結果.標準点C_気分の変化 && c02結果.標準点C_気分の変化 <= 4) ||
				(3 <= c02結果.標準点O_主観性 && c02結果.標準点O_主観性 <= 4))
				return "○";

			return "";
		}

		private static string 適度な内省性をそなえている(C02系統値計算_結果 c02結果)
		{
			if (3 > c02結果.標準点R_のん気 &&
				3 > c02結果.標準点T_思考性)
				return "○";

			return "";
		}

		private static string 部下に対する細かい配慮ができる(C02系統値計算_結果 c02結果)
		{
			if (3 < c02結果.標準点N_神経質 &&
				3 > c02結果.標準点O_主観性 &&
				3 > c02結果.標準点Co_協調性)
				return "○";

			return "";
		}



		private static string リーダータイプ判定(C02系統値計算_結果 c02結果, C07集合因子判定_結果 c07結果)
		{
			string str = "";

			if (3 < c02結果.標準点Ag_攻撃性 &&
				3 < c02結果.標準点G_活動性)
			{
				str += "業績重視型（能動的積極行動型）" + "<br>";
				str += "業績を重視する鬼部隊長型のリーダー。売上高や生産高は企業の生死を決する重大問題だととらえ、部下の人間関係を無視するのではないが、それよりも業績を重視するタイプ。" + "<br>";
			}
			else if ((2 <= c02結果.標準点Ag_攻撃性 && c02結果.標準点Ag_攻撃性 <= 3) &&
				(4 <= c02結果.標準点G_活動性 && c02結果.標準点G_活動性 <= 5))
			{
				str += "人間関係重視型（受動的積極行動型）" + "<br>";
				str += "業績を軽視するわけではないが、チームの人間関係を重視するリーダー。一人ひとりの業績よりも、長期的視野にたったチーム力開発こそ、業績向上の基礎だと考えるタイプ。" + "<br>";
			}
			else
			{
				if (c07結果.mstr主導性 != "普通" &&
					c07結果.mstr主導性 != "服従型" &&
					c07結果.mstr主導性 != "自己顕示型")
				{
					str += "業績重視型にも人間関係重視型にも該当しないリーダータイプ。" + "<br>";
				}
				else
				{
					str += "リーダーには向かないタイプ。" + "<br>";
				}
			}

			if ((3 <= c02結果.標準点D_抑うつ性 && c02結果.標準点D_抑うつ性 <= 4) &&
				(3 <= c02結果.標準点C_気分の変化 && c02結果.標準点C_気分の変化 <= 4) &&
				(3 <= c02結果.標準点I_劣等感 && c02結果.標準点I_劣等感 <= 4) &&
				(3 <= c02結果.標準点N_神経質 && c02結果.標準点N_神経質 <= 4) &&
				(3 <= c02結果.標準点O_主観性 && c02結果.標準点O_主観性 <= 4) &&
				(3 <= c02結果.標準点Co_協調性 && c02結果.標準点Co_協調性 <= 4) &&
				3 < c02結果.標準点Ag_攻撃性 &&
				3 < c02結果.標準点G_活動性)
			{
				str += "アイディアに富み、孤独に強く、行動が積極的。" + "<br>";
			}

			return str;
		}

		//private static string リーダーシップ判定補足()
		//{
		//    string str = "";

		//    if ((3 <= c02結果.mi標準点D && c02結果.mi標準点D <= 4) &&
		//        (3 <= c02結果.mi標準点C && c02結果.mi標準点C <= 4) &&
		//        (3 <= c02結果.mi標準点I && c02結果.mi標準点I <= 4) &&
		//        (3 <= c02結果.mi標準点N && c02結果.mi標準点N <= 4) &&
		//        (3 <= c02結果.mi標準点O && c02結果.mi標準点O <= 4) &&
		//        (3 <= c02結果.mi標準点Co && c02結果.mi標準点Co <= 4))
		//    {
		//        mstr可変_補足 += "アイディアに富み、孤独に強く、行動が積極的。" + "<br>";
		//    }

		//    return str;
		//}

		private static void リーダーシップ阻害因子判定(C02系統値計算_結果 c02結果, C10リーダー資質判定_結果 c10結果)
		{

			if (1 == c02結果.標準点Ag_攻撃性)
			{
				//str += "気力に乏しい。" + "<br>";
				阻害因子を追加("気力に乏しい。", c10結果);
			}

			if (1 == c02結果.標準点R_のん気)
			{
				//str += "決断力に乏しい。" + "<br>";
				阻害因子を追加("決断力に乏しい。", c10結果);
			}

			if (1 == c02結果.標準点G_活動性)
			{
				//str += "行動力に乏しい。" + "<br>";
				阻害因子を追加("行動力に乏しい。", c10結果);
			}

			if (1 == c02結果.標準点A_支配性)
			{
				//str += "指導者意識に乏しい。" + "<br>";
				阻害因子を追加("指導者意識に乏しい。", c10結果);
			}

			if (1 == c02結果.標準点T_思考性)
			{
				//str += "強いリーダーシップの発揮は期待できない。" + "<br>";
				阻害因子を追加("強いリーダーシップの発揮は期待できない。", c10結果);
			}

			if (3 > c02結果.標準点G_活動性)
			{
				//str += "行動面にキビキビとした活動性がない。" + "<br>";
				阻害因子を追加("行動面にキビキビとした活動性がない。", c10結果);
			}

			if (3 > c02結果.標準点A_支配性 &&
				3 > c02結果.標準点S_社会性)
			{
				//str += "リーダー意識が高くない。" + "<br>";
				阻害因子を追加("リーダー意識が高くない。", c10結果);
			}
			//if (3 > c02結果.標準点A_支配性 &&
			//	3 > c02結果.標準点S_社会性)
			//{
			//	//str += "強いリーダーシップの発揮は期待できない。" + "<br>";
			//	阻害因子を追加("強いリーダーシップの発揮は期待できない。", c10結果);
			//}

			if (3 < c02結果.標準点R_のん気 &&
				3 < c02結果.標準点T_思考性)
			{
				//str += "のんきで非熟慮的な傾向が著しい。" + "<br>";
				阻害因子を追加("のんきで非熟慮的な傾向が著しい。", c10結果);
			}

			if (3 < c02結果.標準点I_劣等感 &&
				3 > c02結果.標準点Ag_攻撃性)
			{
				//str += "劣等感が強く、意欲も喪失している。" + "<br>";
				阻害因子を追加("劣等感が強く、意欲も喪失している。", c10結果);
			}

			if (3 > c02結果.標準点O_主観性 &&
				3 > c02結果.標準点Co_協調性)
			{
				//str += "周囲に妥協しやすい。" + "<br>";
				阻害因子を追加("周囲に妥協しやすい。", c10結果);
			}

			if (3 < c02結果.標準点O_主観性 &&
				3 < c02結果.標準点Co_協調性)
			{
				//str += "警戒心が強く人間関係も閉鎖的。" + "<br>";
				阻害因子を追加("警戒心が強く人間関係も閉鎖的。", c10結果);
			}

			if (3 < c02結果.標準点C_気分の変化 &&
				3 < c02結果.標準点N_神経質 &&
				3 < c02結果.標準点O_主観性)
			{
				//str += "情緒面の安定性は極めて悪い。" + "<br>";
				阻害因子を追加("情緒面の安定性が極めて悪い。", c10結果);
			}

			if (3 < c02結果.標準点G_活動性 &&
				3 > c02結果.標準点A_支配性)
			{
				//str += "個人レベルの行動力はあるが、集団レベルの行動力は低い。" + "<br>";
				阻害因子を追加("個人レベルの行動力はあるが、集団レベルの行動力は低い。", c10結果);
			}

			if (3 <= (c02結果.標準点Ag_攻撃性 - c02結果.標準点G_活動性) ||
				3 <= (c02結果.標準点Ag_攻撃性 - c02結果.標準点A_支配性) ||
				(3 < c02結果.標準点Co_協調性 && 3 < c02結果.標準点S_社会性))
			{
				//str += "矛盾した性格が問題になりやすい。" + "<br>";
				阻害因子を追加("矛盾した性格が問題になりやすい。", c10結果);
			}

			if (5 == c02結果.標準点D_抑うつ性 ||
				5 == c02結果.標準点C_気分の変化 ||
				5 == c02結果.標準点I_劣等感 ||
				5 == c02結果.標準点N_神経質 ||
				5 == c02結果.標準点O_主観性 ||
				5 == c02結果.標準点Co_協調性)
			{
				//str += "リーダーには向かない性格因子が高い値になっている。" + "<br>";
				阻害因子を追加("抑うつ性、気分の変化、劣等感、神経質、主観性、協調性、いずれも極端に高い値になっている。", c10結果);
			}

			// D類リーダー型を診断する時の注意点。のんき楽天型
			if (1 == c02結果.標準点D_抑うつ性 &&
				c02結果.標準点G_活動性 < c02結果.標準点R_のん気 &&
				c02結果.標準点G_活動性 < c02結果.標準点T_思考性)
			{
				//str += "のんき楽天的で問題意識が低い。中には知能の低い人も含まれるが、楽天的すぎて熟慮性に欠け、衝動的な行動が目立つ。" + "<br>";
				阻害因子を追加("のんき、楽天的で問題意識が低い。中には知能の低い人も含まれるが、楽天的すぎて熟慮性に欠け、衝動的な行動が目立つ。", c10結果);
			}

			// D類リーダー型を診断する時の注意点。個人プレイ型
			if (c02結果.標準点A_支配性 < c02結果.標準点G_活動性 &&
				c02結果.標準点S_社会性 < c02結果.標準点G_活動性)
			{
				//str += "個人レベルの行動は積極的で成績もよいが、集団レベルの行動は弱気で消極的。概して小心で劣等感が強く、人間関係の苦手な人。" + "<br>";
				//str += "リーダーに登用するよりは、そのままで十分に実力を発揮させる方が、本人のためにでもあり会社の為でもある。" + "<br>";
				阻害因子を追加("個人レベルの行動は積極的で成績もよいが、集団レベルの行動は弱気で消極的な場合が多い。", c10結果);
				阻害因子を追加("自信がない為に周囲に気を使い、何事も大勢に順応して行動する弱気なタイプ。", c10結果);
				//阻害因子を追加("リーダーには登用せずに、個人の力を発揮させる方が、本人のためにでもあり、会社の為でもある。", c10結果);
			}
			//// D類リーダー型を診断する時の注意点。弱気・おせじ型
			//if (c02結果.標準点A_支配性 < c02結果.標準点G_活動性 &&
			//	c02結果.標準点S_社会性 < c02結果.標準点G_活動性)
			//{
			//	//str += "小心で劣等感が強い場合が多く、自身がない為に周囲に気を使い、何事も大勢に順応して行動する弱気なタイプ。" + "<br>";
			//	阻害因子を追加("小心で劣等感が強い場合が多く、自身がない為に周囲に気を使い、何事も大勢に順応して行動する弱気なタイプ。", c10結果);
			//}

			// D類リーダー型を診断する時の注意点。強気・部下不信型
			if (5 == c02結果.標準点Ag_攻撃性 &&
				3 < c02結果.標準点O_主観性 &&
				3 < c02結果.標準点Co_協調性)
			{
				//str += "超強気で気性も激しい人だが、反面、人間不信感が強く、部下を信頼できない面がある。そのため本人はひと一倍よく働くが、部下の人望がなく仕事を部下に任せられない癖がある。" + "<br>";
				阻害因子を追加("超強気で気性も激しい人だが、反面、人間不信感が強く、部下を信頼できない面がある。そのため本人はひと一倍よく働くが、部下の人望がなく仕事を部下に任せられない癖がある。", c10結果);
			}

			// B類リーダー型を診断する時の注意点。情緒不安定型
			//str += リーダーシップ阻害因子判定_情緒不安定型();
			阻害因子を追加(リーダーシップ阻害因子判定_情緒不安定型(c02結果), c10結果);

			// B類リーダー型を診断する時の注意点。内省性欠如
			if (5 == c02結果.標準点C_気分の変化 &&
				5 == c02結果.標準点O_主観性 &&
				c02結果.標準点G_活動性 < c02結果.標準点R_のん気 &&
				c02結果.標準点G_活動性 < c02結果.標準点T_思考性)
			{
				//str += "空想性が強く内省性の欠如した我がままタイプ。未熟で幼児的なヒステリー性格者である事が多い。不安にもとずく欲求不満が爆発しやすい。ふだんは人間関係もよく社交的であるが、自己中心的な人。" + "<br>";
				阻害因子を追加("空想性が強く内省性の欠如したわがままタイプ。<br>未熟で幼児的なヒステリーを起こす事が多い。<br>不安にもとずく欲求不満が爆発しやすい。<br>ふだんは人間関係もよく社交的であっても自己中心的な人。", c10結果);
			}

			// B類リーダー型を診断する時の注意点。一匹狼型
			if (c02結果.標準点G_活動性 > c02結果.標準点A_支配性 &&
				c02結果.標準点G_活動性 > c02結果.標準点S_社会性)
			{
				//str += "情緒が不安定で強気。強気なわりには人間関係が苦手で、集団行動の場面では逃げてしまう。" + "<br>";
				//str += "リーダーに登用するよりは、そのままで十分に実力を発揮させる方が、本人のためにでもあり会社の為でもある。" + "<br>";
				阻害因子を追加("強気なわりには人間関係が苦手で、集団行動の場面では逃げてしまう。", c10結果);
				//阻害因子を追加("リーダーに登用するよりは、そのままで実力を発揮させる方が、本人のためにでもあり会社の為でもある。", c10結果);
			}

			if (c10結果.mi阻害因子Cnt == 0)
			{
				//return "リーダーシップを阻害する因子は見つかりませんでした。" + "<br>";
				阻害因子を追加("リーダーシップを阻害する要因は見つかりませんでした。", c10結果);
			}
		}

		private static string リーダーシップ阻害因子判定_情緒不安定型(C02系統値計算_結果 c02結果)
		{
			int iCnt = 0;
			if (5 == c02結果.標準点D_抑うつ性)
				iCnt++;

			if (5 == c02結果.標準点C_気分の変化)
				iCnt++;

			if (5 == c02結果.標準点I_劣等感)
				iCnt++;

			if (5 == c02結果.標準点N_神経質)
				iCnt++;

			if (5 == c02結果.標準点O_主観性)
				iCnt++;

			if (5 == c02結果.標準点Co_協調性)
				iCnt++;

			if (3 < iCnt)
				return "極めて不安定な情緒が行動に表れやすい。" + "<br>";
			else
				return "";
		}


	}
}
