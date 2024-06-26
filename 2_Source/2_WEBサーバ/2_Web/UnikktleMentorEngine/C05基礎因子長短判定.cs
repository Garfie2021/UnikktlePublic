﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UnikktleMentorEngine
{
	public static class C05基礎因子長短判定
	{
		private const string 適度な問題意識を持つことができる = "適度な問題意識を持つことができる。";
		private const string 無気力_虚脱感がある_陰湿な面がある = "無気力、虚脱感がある。陰湿な面がある。";
		private const string 充実感がある_楽天的に考えることができる = "充実感がある。楽天的に考えることができる。";
		private const string 楽天的に考えすぎて_自己満足で終わることがある = "楽天的に考えすぎて、自己満足で終わることがある。";
		private const string 情緒が豊か = "情緒が豊か。";
		private const string 感情的になりやすい_臆病な面がある_お天気屋な面がある = "感情的になりやすい。臆病な面がある。お天気屋な面がある。";
		private const string いつも冷静でいられる_理性的に行動することができる = "いつも冷静でいられる。理性的に行動することができる。";
		private const string 自身の感情や他者の感情に気付かない_冷めていると見られがち = "自身の感情や他者の感情に気付かない。冷めていると見られがち。";
		private const string 向上心が高い_謙虚さがある = "向上心が高い。謙虚さがある。";
		private const string 劣等感がある_優柔不断な状態にある_自信が欠如した状態にある = "劣等感がある。優柔不断な状態にある。自信が欠如した状態にある。";
		private const string 自信を持った行動ができる = "自信を持った行動ができる。";
		private const string 自身過剰な状態にある_本心と裏腹なことを言ったり_思いと正反対の行動をとることがある = "自身過剰な状態にある。本心と裏腹なことを言ったり、思いと正反対の行動をとることがある。";
		private const string 良く気がつく_感受性が高い = "良く気がつく。感受性が高い。";
		private const string 心配性で情緒が不安定な状態にある_小さな事でイライラしたりくよくよすることがある = "心配性で情緒が不安定な状態にある。小さな事でイライラしたりくよくよすることがある。";
		private const string 情緒が安定している_小さな事でくよくよしない = "情緒が安定している。小さな事でくよくよしない。";
		private const string 気配りができない_鈍感な面がある = "気配りができない。鈍感な面がある。";
		private const string 信念が強い_理想の為に行動できる = "信念が強い。理想の為に行動できる。";
		private const string 空想にふけり現実世界に背を向けることがある_主観的になり自己中心的な行動をとりがち = "空想にふけり現実世界に背を向けることがある。主観的になり自己中心的な行動をとりがち。";
		private const string 客観的に考える事ができる_常識的で現実的な行動をとることができる = "客観的に考える事ができる。常識的で現実的な行動をとることができる。";
		private const string 妥協し易く信念に乏しい = "妥協し易く信念に乏しい。";
		private const string 適度な警戒心を持っている = "適度な警戒心を持っている。";
		private const string 対人不信感が強い_警戒心が強い_不満感が強い_閉鎖的人間関係_現状否定的 = "対人不信感が強い。警戒心が強い、不満感が強い。閉鎖的人間関係。現状否定的";
		private const string 人間信頼_開放的人間信頼_現状肯定的 = "人間信頼。開放的人間信頼。現状肯定的。";
		private const string 協調的すぎる_警戒心_主体性が乏しい = "協調的すぎる。警戒心、主体性が乏しい。";
		private const string 主体を持って_積極的_意欲的に行動できる = "主体を持って、積極的、意欲的に行動できる。";
		private const string 短気で怒りっぽく_攻撃的な面がある_直情傾向がある_自尊心が強い = "短気で怒りっぽく、攻撃的な面がある。直情傾向がある。自尊心が強い。";
		private const string 気が長い_温順_受動的 = "気が長い。温順。受動的。";
		private const string 消極的すぎる_意欲欠如_怒れない = "消極的すぎる。意欲欠如。怒れない。";
		private const string 活動的_快活_敏速_能率的 = "活動的。快活。敏速。能率的。";
		private const string 自分だけが動いてしまう_他人に任せ切れない = "自分だけが動いてしまう。他人に任せ切れない。";
		private const string 温順_順応性が高い = "温順。順応性が高い。";
		private const string 鈍重_非能率的_陰気 = "鈍重。非能率的。陰気。";
		private const string 気軽さ_行動的_身軽さ_決断力がある = "気軽さ。行動的。身軽さ。決断力がある。";
		private const string 軽率で衝動的な行動をとる事がある = "軽率で衝動的な行動をとる事がある。";
		private const string 慎重な行動ができる = "慎重な行動ができる。";
		private const string 優柔不断_腰が重い = "優柔不断。腰が重い。";
		private const string 決断力がある_明るい見方_小さな事を気にしない = "決断力がある。明るい見方。小さな事を気にしない。";
		private const string 非熟慮的_無計画_無頓着 = "非熟慮的。無計画。無頓着。";
		private const string 几帳面_熟慮的で計画的な行動がとれる = "几帳面。熟慮的で計画的な行動がとれる。";
		private const string 小さな事を考えすぎる_逡巡_懐疑的 = "小さな事を考えすぎる。逡巡。懐疑的。";
		private const string 指導者意識が高い_集団の中で行動力を発揮できる = "指導者意識が高い。集団の中で行動力を発揮できる。";
		private const string 自己顕示欲が強い = "自己顕示欲が強い。";
		private const string 従順でいられる = "従順でいられる。";
		private const string 追従的で妥協しがち = "追従的で妥協しがち。";
		private const string 社交的_人間好き = "社交的。人間好き。";
		private const string 軽薄な行動をとりがち_派手好き = "軽薄な行動をとりがち。派手好き。";
		private const string 地味な人柄 = "地味な人柄。";
		private const string 非社交性_人間嫌い = "非社交性。人間嫌い。";



		public static void 基礎因子長短判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			//初期化();

			D判定(c02結果, c05結果);
			C判定(c02結果, c05結果);
			I判定(c02結果, c05結果);
			N判定(c02結果, c05結果);
			O判定(c02結果, c05結果);
			Co判定(c02結果, c05結果);
			Ag判定(c02結果, c05結果);
			G判定(c02結果, c05結果);
			R判定(c02結果, c05結果);
			T判定(c02結果, c05結果);
			A判定(c02結果, c05結果);
			S判定(c02結果, c05結果);

			c05結果.mstr可変 += c05結果.mstr長所 + "<br><br>";

			c05結果.mstr可変 += c05結果.mstr短所 + "<br>";

		}

		public static void 長所を追加(string str長所, C05基礎因子長短判定_結果 c05結果)
		{
			if (0 > c05結果.mstr長所.IndexOf(str長所))
				c05結果.mstr長所 += str長所 + "<br>";
		}

		public static void 短所を追加(string str短所, C05基礎因子長短判定_結果 c05結果)
		{
			if (0 > c05結果.mstr短所.IndexOf(str短所))
				c05結果.mstr短所 += str短所 + "<br>";
		}


		private static void D判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点D_抑うつ性)
			{
				長所を追加(適度な問題意識を持つことができる, c05結果);
				短所を追加(無気力_虚脱感がある_陰湿な面がある, c05結果);
			}
			else if (3 > c02結果.標準点D_抑うつ性)
			{
				長所を追加(充実感がある_楽天的に考えることができる, c05結果);
				短所を追加(楽天的に考えすぎて_自己満足で終わることがある, c05結果);
			}
		}

		private static void C判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点C_気分の変化)
			{
				長所を追加(情緒が豊か, c05結果);
				短所を追加(感情的になりやすい_臆病な面がある_お天気屋な面がある, c05結果);
			}
			else if (3 > c02結果.標準点C_気分の変化)
			{
				長所を追加(いつも冷静でいられる_理性的に行動することができる, c05結果);
				短所を追加(自身の感情や他者の感情に気付かない_冷めていると見られがち, c05結果);
			}
		}

		private static void I判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点I_劣等感)
			{
				長所を追加(向上心が高い_謙虚さがある, c05結果);
				短所を追加(劣等感がある_優柔不断な状態にある_自信が欠如した状態にある, c05結果);
			}
			else if (3 > c02結果.標準点I_劣等感)
			{
				長所を追加(自信を持った行動ができる, c05結果);
				短所を追加(自身過剰な状態にある_本心と裏腹なことを言ったり_思いと正反対の行動をとることがある, c05結果);
			}
		}

		private static void N判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点N_神経質)
			{
				長所を追加(良く気がつく_感受性が高い, c05結果);
				短所を追加(心配性で情緒が不安定な状態にある_小さな事でイライラしたりくよくよすることがある, c05結果);
			}
			else if (3 > c02結果.標準点N_神経質)
			{
				長所を追加(情緒が安定している_小さな事でくよくよしない, c05結果);
				短所を追加(気配りができない_鈍感な面がある, c05結果);
			}
		}

		private static void O判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点O_主観性)
			{
				長所を追加(信念が強い_理想の為に行動できる, c05結果);
				短所を追加(空想にふけり現実世界に背を向けることがある_主観的になり自己中心的な行動をとりがち, c05結果);
			}
			else if (3 > c02結果.標準点O_主観性)
			{
				長所を追加(客観的に考える事ができる_常識的で現実的な行動をとることができる, c05結果);
				短所を追加(妥協し易く信念に乏しい, c05結果);
			}
		}

		private static void Co判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点Co_協調性)
			{
				長所を追加(適度な警戒心を持っている, c05結果);
				短所を追加(対人不信感が強い_警戒心が強い_不満感が強い_閉鎖的人間関係_現状否定的, c05結果);
			}
			else if (3 > c02結果.標準点Co_協調性)
			{
				長所を追加(人間信頼_開放的人間信頼_現状肯定的, c05結果);
				短所を追加(協調的すぎる_警戒心_主体性が乏しい, c05結果);
			}
		}

		private static void Ag判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
			{
				長所を追加(主体を持って_積極的_意欲的に行動できる, c05結果);
				短所を追加(短気で怒りっぽく_攻撃的な面がある_直情傾向がある_自尊心が強い, c05結果);
			}
			else if (3 > c02結果.標準点Ag_攻撃性)
			{
				長所を追加(気が長い_温順_受動的, c05結果);
				短所を追加(消極的すぎる_意欲欠如_怒れない, c05結果);
			}
		}

		private static void G判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点G_活動性)
			{
				長所を追加(活動的_快活_敏速_能率的, c05結果);
				短所を追加(自分だけが動いてしまう_他人に任せ切れない, c05結果);
			}
			else if (3 > c02結果.標準点G_活動性)
			{
				長所を追加(温順_順応性が高い, c05結果);
				短所を追加(鈍重_非能率的_陰気, c05結果);
			}
		}

		private static void R判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点R_のん気)
			{
				長所を追加(気軽さ_行動的_身軽さ_決断力がある, c05結果);
				短所を追加(軽率で衝動的な行動をとる事がある, c05結果);
			}
			else if (3 > c02結果.標準点R_のん気)
			{
				長所を追加(慎重な行動ができる, c05結果);
				短所を追加(優柔不断_腰が重い, c05結果);
			}
		}

		private static void T判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点T_思考性)
			{
				長所を追加(決断力がある_明るい見方_小さな事を気にしない, c05結果);
				短所を追加(非熟慮的_無計画_無頓着, c05結果);
			}
			else if (3 > c02結果.標準点T_思考性)
			{
				長所を追加(几帳面_熟慮的で計画的な行動がとれる, c05結果);
				短所を追加(小さな事を考えすぎる_逡巡_懐疑的, c05結果);
			}
		}

		private static void A判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点A_支配性)
			{
				長所を追加(指導者意識が高い_集団の中で行動力を発揮できる, c05結果);
				短所を追加(自己顕示欲が強い, c05結果);
			}
			else if (3 > c02結果.標準点A_支配性)
			{
				長所を追加(従順でいられる, c05結果);
				短所を追加(追従的で妥協しがち, c05結果);
			}
		}

		private static void S判定(C02系統値計算_結果 c02結果, C05基礎因子長短判定_結果 c05結果)
		{
			if (3 < c02結果.標準点S_社会性)
			{
				長所を追加(社交的_人間好き, c05結果);
				短所を追加(自己顕示欲が強い, c05結果);
				短所を追加(軽薄な行動をとりがち_派手好き, c05結果);
			}
			else if (3 > c02結果.標準点S_社会性)
			{
				長所を追加(地味な人柄, c05結果);
				短所を追加(非社交性_人間嫌い, c05結果);
			}
		}

	}
}
