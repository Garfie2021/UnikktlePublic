﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UnikktleMentorEngine
{
	public static class C04基礎因子判定
	{
		private const string 特性 = "特性";
		private const string 傾向 = "傾向";
		private const string 虚脱感 = "虚脱感";
		private const string 自信欠如 = "自信欠如";
		private const string 神経質 = "神経質";
		private const string 倦怠感 = "倦怠感";
		private const string 閉鎖的_悲観 = "閉鎖的（悲観）";
		private const string 小心 = "小心";
		private const string 集中力欠如 = "集中力欠如";
		private const string お天気屋 = "お天気屋";
		private const string 感情的 = "感情的";
		private const string 消極的 = "消極的";
		private const string 優柔不断 = "優柔不断";
		private const string 劣等感 = "劣等感";
		private const string 事なかれ主義 = "事なかれ主義";
		private const string ロマンチスト = "ロマンチスト";
		private const string 分裂傾向 = "分裂傾向";
		private const string 対人不信感 = "対人不信感";
		private const string 不運_不遇感 = "不運（不遇感）";
		private const string 不満感 = "不満感";
		private const string 活動的 = "活動的";
		private const string 攻撃性 = "攻撃性";
		private const string 衝動的 = "衝動的";
		private const string 自尊心 = "自尊心";
		private const string 短気 = "短気";
		private const string 好奇心旺盛 = "好奇心旺盛";
		private const string 協調的_善意 = "協調的（善意）";
		private const string 敏腕_高能率 = "敏腕（高能率）";
		private const string 快活_楽天的 = "快活（楽天的）";
		private const string 順応的 = "順応的";
		private const string 多弁_お喋り = "多弁（お喋り）";
		private const string 軽率_気軽 = "軽率（気軽）";
		private const string 開放的_楽観 = "開放的（楽観）";
		private const string 非思索的 = "非思索的";
		private const string 無頓着_のん気 = "無頓着（のん気）";
		private const string 自己顕示 = "自己顕示";
		private const string 世話好き = "世話好き";
		private const string 自信家 = "自信家";
		private const string 指導的 = "指導的";
		private const string 社交的 = "社交的";
		private const string 派手好き = "派手好き";
		private const string 充実感 = "充実感";
		private const string 楽天的 = "楽天的";
		private const string 元気 = "元気";
		private const string 大胆_安心感 = "大胆（安心感）";
		private const string 集中力旺盛 = "集中力旺盛";
		private const string 穏健 = "穏健";
		private const string 冷静 = "冷静";
		private const string 積極的 = "積極的";
		private const string 果断_決断 = "果断（決断）";
		private const string 優越感 = "優越感";
		private const string 現実主義 = "現実主義";
		private const string 安定的 = "安定的";
		private const string 幸運感 = "幸運感";
		private const string 満足感 = "満足感";
		private const string 行動不活発 = "行動不活発";
		private const string 慎重 = "慎重";
		private const string 自己卑下 = "自己卑下";
		private const string 気長 = "気長";
		private const string 現状維持的 = "現状維持的";
		private const string 非協調的 = "非協調的";
		private const string 不器用_非能率 = "不器用（非能率）";
		private const string 陰気_幻滅感 = "陰気（幻滅感）";
		private const string 批判的 = "批判的";
		private const string 無口 = "無口";
		private const string 熟慮_重厚 = "熟慮（重厚）";
		private const string 思索的 = "思索的";
		private const string 警戒心 = "警戒心";
		private const string 引込み思案 = "引込み思案";
		private const string 不干渉主義 = "不干渉主義";
		private const string 非指導的 = "非指導的";
		private const string 非社交的 = "非社交的";
		private const string 地味 = "地味";

		private const string あり = "○";
		private const string なし = "";

		
		public static void 基礎因子判定(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			//初期化();

			高得点_虚脱感(c02結果, c04結果);
			高得点_自信欠如(c02結果, c04結果);
			高得点_神経質(c02結果, c04結果);
			高得点_倦怠感(c02結果, c04結果);
			高得点_閉鎖的_悲観(c02結果, c04結果);
			高得点_小心(c02結果, c04結果);
			高得点_集中力欠如(c02結果, c04結果);
			高得点_お天気屋(c02結果, c04結果);
			高得点_感情的(c02結果, c04結果);
			高得点_消極的(c02結果, c04結果);
			高得点_優柔不断(c02結果, c04結果);
			高得点_劣等感(c02結果, c04結果);
			高得点_事なかれ主義(c02結果, c04結果);
			高得点_ロマンチスト(c02結果, c04結果);
			高得点_分裂傾向(c02結果, c04結果);
			高得点_対人不信感(c02結果, c04結果);
			高得点_不運_不遇感(c02結果, c04結果);
			高得点_不満感(c02結果, c04結果);
			高得点_活動的(c02結果, c04結果);
			高得点_果断_決断(c02結果, c04結果);
			高得点_攻撃性(c02結果, c04結果);
			高得点_衝動的(c02結果, c04結果);
			高得点_自尊心(c02結果, c04結果);
			高得点_短気(c02結果, c04結果);
			高得点_好奇心旺盛(c02結果, c04結果);
			高得点_協調的(c02結果, c04結果);
			高得点_敏腕_高能率(c02結果, c04結果);
			高得点_快活_楽天的(c02結果, c04結果);
			高得点_順応的(c02結果, c04結果);
			高得点_多弁_お喋り(c02結果, c04結果);
			高得点_軽率_気軽(c02結果, c04結果);
			高得点_開放的_楽観(c02結果, c04結果);
			高得点_非思索的(c02結果, c04結果);
			高得点_無頓着_のん気(c02結果, c04結果);
			高得点_自己顕示(c02結果, c04結果);
			高得点_世話好き(c02結果, c04結果);
			高得点_自信家(c02結果, c04結果);
			高得点_指導的(c02結果, c04結果);
			高得点_社交的(c02結果, c04結果);
			高得点_派手好き(c02結果, c04結果);
			高得点_強気(c02結果, c04結果);
			高得点_キビキビ(c02結果, c04結果);

			//低得点_充実感(c02結果, c04結果);
			低得点_自信家(c02結果, c04結果);
			低得点_楽天的(c02結果, c04結果);
			低得点_元気(c02結果, c04結果);
			低得点_開放的_楽観(c02結果, c04結果);
			低得点_大胆_安心感(c02結果, c04結果);
			低得点_集中力旺盛(c02結果, c04結果);
			低得点_穏健(c02結果, c04結果);
			低得点_冷静(c02結果, c04結果);
			低得点_積極的(c02結果, c04結果);
			低得点_果断_決断(c02結果, c04結果);
			低得点_優越感(c02結果, c04結果);
			低得点_攻撃性(c02結果, c04結果);
			低得点_現実主義(c02結果, c04結果);
			低得点_安定的(c02結果, c04結果);
			低得点_協調的_善意(c02結果, c04結果);
			低得点_幸運感(c02結果, c04結果);
			低得点_満足感(c02結果, c04結果);
			低得点_行動不活発(c02結果, c04結果);
			低得点_優柔不断(c02結果, c04結果);
			低得点_事なかれ主義(c02結果, c04結果);
			低得点_慎重(c02結果, c04結果);
			低得点_自己卑下(c02結果, c04結果);
			低得点_気長(c02結果, c04結果);
			低得点_現状維持的(c02結果, c04結果);
			低得点_非協調的(c02結果, c04結果);
			低得点_不器用_非能率(c02結果, c04結果);
			低得点_陰気_幻滅感(c02結果, c04結果);
			低得点_批判的(c02結果, c04結果);
			低得点_無口(c02結果, c04結果);
			低得点_熟慮_重厚(c02結果, c04結果);
			低得点_閉鎖的_悲観(c02結果, c04結果);
			低得点_思索的(c02結果, c04結果);
			低得点_警戒心(c02結果, c04結果);
			低得点_引込み思案(c02結果, c04結果);
			低得点_不干渉主義(c02結果, c04結果);
			低得点_自信欠如(c02結果, c04結果);
			低得点_非指導的(c02結果, c04結果);
			低得点_非社交的(c02結果, c04結果);
			低得点_地味(c02結果, c04結果);
			低得点_弱気(c02結果, c04結果);
			低得点_グズ(c02結果, c04結果);

			//極小_人間嫌い();

			c04結果.mstr固定 = "【基礎因子判定】" + "<br>";
			c04結果.mstr固定 += 特性 + "\t" + 傾向 + "<br>";
			c04結果.mstr固定 += 虚脱感 + c04結果.虚脱感 + "<br>";
			c04結果.mstr固定 += 自信欠如 + c04結果.自信欠如 + "<br>";
			c04結果.mstr固定 += 神経質 + c04結果.神経質 + "<br>";
			c04結果.mstr固定 += 倦怠感 + c04結果.倦怠感 + "<br>";
			c04結果.mstr固定 += 閉鎖的_悲観 + c04結果.閉鎖的_悲観 + "<br>";
			c04結果.mstr固定 += 小心 + c04結果.小心 + "<br>";
			c04結果.mstr固定 += 集中力欠如 + c04結果.集中力欠如 + "<br>";
			c04結果.mstr固定 += お天気屋 + c04結果.お天気屋 + "<br>";
			c04結果.mstr固定 += 感情的 + c04結果.感情的 + "<br>";
			c04結果.mstr固定 += 消極的 + c04結果.消極的 + "<br>";
			c04結果.mstr固定 += 優柔不断 + c04結果.優柔不断 + "<br>";
			c04結果.mstr固定 += 劣等感 + c04結果.劣等感 + "<br>";
			c04結果.mstr固定 += 事なかれ主義 + c04結果.事なかれ主義 + "<br>";
			c04結果.mstr固定 += ロマンチスト + c04結果.ロマンチスト + "<br>";
			c04結果.mstr固定 += 分裂傾向 + c04結果.分裂傾向 + "<br>";
			c04結果.mstr固定 += 対人不信感 + c04結果.対人不信感 + "<br>";
			c04結果.mstr固定 += 不運_不遇感 + c04結果.不運_不遇感 + "<br>";
			c04結果.mstr固定 += 不満感 + c04結果.不満感 + "<br>";
			c04結果.mstr固定 += 活動的 + c04結果.活動的 + "<br>";
			c04結果.mstr固定 += 攻撃性 + c04結果.攻撃性 + "<br>";
			c04結果.mstr固定 += 衝動的 + c04結果.衝動的 + "<br>";
			c04結果.mstr固定 += 自尊心 + c04結果.自尊心 + "<br>";
			c04結果.mstr固定 += 短気 + c04結果.短気 + "<br>";
			c04結果.mstr固定 += 好奇心旺盛 + c04結果.好奇心旺盛 + "<br>";
			c04結果.mstr固定 += 協調的_善意 + c04結果.協調的_善意 + "<br>";
			c04結果.mstr固定 += 敏腕_高能率 + c04結果.敏腕_高能率 + "<br>";
			c04結果.mstr固定 += 快活_楽天的 + c04結果.快活_楽天的 + "<br>";
			c04結果.mstr固定 += 順応的 + c04結果.順応的 + "<br>";
			c04結果.mstr固定 += 多弁_お喋り + c04結果.多弁_お喋り + "<br>";
			c04結果.mstr固定 += 軽率_気軽 + c04結果.軽率_気軽 + "<br>";
			c04結果.mstr固定 += 開放的_楽観 + c04結果.開放的_楽観 + "<br>";
			c04結果.mstr固定 += 非思索的 + c04結果.非思索的 + "<br>";
			c04結果.mstr固定 += 無頓着_のん気 + c04結果.無頓着_のん気 + "<br>";
			c04結果.mstr固定 += 自己顕示 + c04結果.自己顕示 + "<br>";
			c04結果.mstr固定 += 世話好き + c04結果.世話好き + "<br>";
			c04結果.mstr固定 += 自信家 + c04結果.自信家 + "<br>";
			c04結果.mstr固定 += 指導的 + c04結果.指導的 + "<br>";
			c04結果.mstr固定 += 社交的 + c04結果.社交的 + "<br>";
			c04結果.mstr固定 += 派手好き + c04結果.派手好き + "<br>";
			c04結果.mstr固定 += "強気" + c04結果.強気 + "<br>";
			c04結果.mstr固定 += "キビキビ" + c04結果.キビキビ + "<br>";

			c04結果.mstr固定 += 充実感 + c04結果.充実感 + "<br>";
			c04結果.mstr固定 += 楽天的 + c04結果.楽天的 + "<br>";
			c04結果.mstr固定 += 元気 + c04結果.元気 + "<br>";
			c04結果.mstr固定 += 大胆_安心感 + c04結果.大胆_安心感 + "<br>";
			c04結果.mstr固定 += 集中力旺盛 + c04結果.集中力旺盛 + "<br>";
			c04結果.mstr固定 += 穏健 + c04結果.穏健 + "<br>";
			c04結果.mstr固定 += 冷静 + c04結果.冷静 + "<br>";
			c04結果.mstr固定 += 積極的 + c04結果.積極的 + "<br>";
			c04結果.mstr固定 += 果断_決断 + c04結果.果断_決断 + "<br>";
			c04結果.mstr固定 += 優越感 + c04結果.優越感 + "<br>";
			c04結果.mstr固定 += 現実主義 + c04結果.現実主義 + "<br>";
			c04結果.mstr固定 += 安定的 + c04結果.安定的 + "<br>";
			c04結果.mstr固定 += 幸運感 + c04結果.幸運感 + "<br>";
			c04結果.mstr固定 += 満足感 + c04結果.満足感 + "<br>";
			c04結果.mstr固定 += 行動不活発 + c04結果.行動不活発 + "<br>";
			c04結果.mstr固定 += 慎重 + c04結果.慎重 + "<br>";
			c04結果.mstr固定 += 自己卑下 + c04結果.自己卑下 + "<br>";
			c04結果.mstr固定 += 気長 + c04結果.気長 + "<br>";
			c04結果.mstr固定 += 現状維持的 + c04結果.現状維持的 + "<br>";
			c04結果.mstr固定 += 非協調的 + c04結果.非協調的 + "<br>";
			c04結果.mstr固定 += 不器用_非能率 + c04結果.不器用_非能率 + "<br>";
			c04結果.mstr固定 += 陰気_幻滅感 + c04結果.陰気_幻滅感 + "<br>";
			c04結果.mstr固定 += 批判的 + c04結果.批判的 + "<br>";
			c04結果.mstr固定 += 無口 + c04結果.無口 + "<br>";
			c04結果.mstr固定 += 熟慮_重厚 + c04結果.熟慮_重厚 + "<br>";
			c04結果.mstr固定 += 思索的 + c04結果.思索的 + "<br>";
			c04結果.mstr固定 += 警戒心 + c04結果.警戒心 + "<br>";
			c04結果.mstr固定 += 引込み思案 + c04結果.引込み思案 + "<br>";
			c04結果.mstr固定 += 不干渉主義 + c04結果.不干渉主義 + "<br>";
			c04結果.mstr固定 += 非指導的 + c04結果.非指導的 + "<br>";
			c04結果.mstr固定 += 非社交的 + c04結果.非社交的 + "<br>";
			c04結果.mstr固定 += 地味 + c04結果.地味 + "<br>";
			c04結果.mstr固定 += "弱気" + c04結果.弱気 + "<br>";
			c04結果.mstr固定 += "グズ" + c04結果.グズ + "<br>";

			//c04結果.mstr固定 += "人間嫌い" + c04結果.人間嫌い + "<br>";

		}


		private static void 高得点_虚脱感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点D_抑うつ性)
				c04結果.虚脱感 = "\t" + あり;
			else if (3 < c02結果.標準点O_主観性)
				c04結果.虚脱感 = "\t" + あり;
		}

		private static void 高得点_自信欠如(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点D_抑うつ性)
				c04結果.自信欠如 = "\t" + あり;
			else if (3 < c02結果.標準点I_劣等感)
				c04結果.自信欠如 = "\t" + あり;
			else if (3 < c02結果.標準点N_神経質)
				c04結果.自信欠如 = "\t" + あり;
		}

		private static void 高得点_神経質(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点D_抑うつ性)
				c04結果.神経質 = "\t" + あり;
			else if (3 < c02結果.標準点I_劣等感)
				c04結果.神経質 = "\t" + あり;
			else if (3 < c02結果.標準点N_神経質)
				c04結果.神経質 = "\t" + あり;
			else if (3 < c02結果.標準点O_主観性)
				c04結果.神経質 = "\t" + あり;
		}

		private static void 高得点_倦怠感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点D_抑うつ性)
				c04結果.倦怠感 = "\t" + あり;
		}

		private static void 高得点_閉鎖的_悲観(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点D_抑うつ性)
				c04結果.閉鎖的_悲観 = "\t" + あり;
		}

		private static void 高得点_小心(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点C_気分の変化)
				c04結果.小心 = "\t" + あり;
			else if (3 < c02結果.標準点I_劣等感)
				c04結果.小心 = "\t" + あり;
			else if (3 < c02結果.標準点N_神経質)
				c04結果.小心 = "\t" + あり;
			else if (3 < c02結果.標準点O_主観性)
				c04結果.小心 = "\t" + あり;
		}

		private static void 高得点_集中力欠如(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点C_気分の変化)
				c04結果.集中力欠如 = "\t" + あり;
		}

		private static void 高得点_お天気屋(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点C_気分の変化)
				c04結果.お天気屋 = "\t" + あり;
			else if (3 < c02結果.標準点O_主観性)
				c04結果.お天気屋 = "\t" + あり;
		}

		private static void 高得点_感情的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点C_気分の変化)
				c04結果.感情的 = "\t" + あり;
		}

		private static void 高得点_消極的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点I_劣等感)
				c04結果.消極的 = "\t" + あり;
		}

		private static void 高得点_優柔不断(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点I_劣等感)
				c04結果.優柔不断 = "\t" + あり;
		}

		private static void 高得点_劣等感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点I_劣等感)
				c04結果.劣等感 = "\t" + あり;
		}

		private static void 高得点_事なかれ主義(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点O_主観性)
				c04結果.事なかれ主義 = "\t" + あり;
		}

		private static void 高得点_ロマンチスト(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点O_主観性)
				c04結果.ロマンチスト = "\t" + あり;
		}

		private static void 高得点_分裂傾向(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点O_主観性)
				c04結果.分裂傾向 = "\t" + あり;
		}

		private static void 高得点_対人不信感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Co_協調性)
				c04結果.対人不信感 = "\t" + あり;
		}

		private static void 高得点_不運_不遇感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Co_協調性)
				c04結果.不運_不遇感 = "\t" + あり;
		}

		private static void 高得点_不満感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点N_神経質)
				c04結果.不満感 = "\t" + あり;
			else if (3 < c02結果.標準点Co_協調性)
				c04結果.不満感 = "\t" + あり;
		}

		private static void 高得点_活動的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
				c04結果.活動的 = "\t" + あり;
			else if (3 < c02結果.標準点G_活動性)
				c04結果.活動的 = "\t" + あり;
			else if (3 < c02結果.標準点R_のん気)
				c04結果.活動的 = "\t" + あり;
			else if (3 < c02結果.標準点T_思考性)
				c04結果.活動的 = "\t" + あり;
			else if (3 < c02結果.標準点A_支配性)
				c04結果.活動的 = "\t" + あり;
		}

		private static void 高得点_果断_決断(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
				c04結果.果断_決断 = "\t" + あり;
		}

		private static void 高得点_攻撃性(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
				c04結果.攻撃性 = "\t" + あり;
		}

		private static void 高得点_衝動的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
				c04結果.衝動的 = "\t" + あり;
		}

		private static void 高得点_自尊心(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
				c04結果.自尊心 = "\t" + あり;
		}

		private static void 高得点_短気(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
				c04結果.短気 = "\t" + あり;
		}

		private static void 高得点_好奇心旺盛(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
				c04結果.好奇心旺盛 = "\t" + あり;
			else if (3 < c02結果.標準点R_のん気)
				c04結果.好奇心旺盛 = "\t" + あり;
		}

		private static void 高得点_協調的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点G_活動性)
				c04結果.協調的_善意 = "\t" + あり;
		}

		private static void 高得点_敏腕_高能率(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点G_活動性)
				c04結果.敏腕_高能率 = "\t" + あり;
		}

		private static void 高得点_快活_楽天的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点G_活動性)
				c04結果.快活_楽天的 = "\t" + あり;
			else if (3 < c02結果.標準点R_のん気)
				c04結果.快活_楽天的 = "\t" + あり;
		}

		private static void 高得点_順応的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点G_活動性)
				c04結果.順応的 = "\t" + あり;
		}

		private static void 高得点_多弁_お喋り(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点R_のん気)
				c04結果.多弁_お喋り = "\t" + あり;
			else if (3 < c02結果.標準点A_支配性)
				c04結果.多弁_お喋り = "\t" + あり;
			else if (3 < c02結果.標準点S_社会性)
				c04結果.多弁_お喋り = "\t" + あり;
		}

		private static void 高得点_軽率_気軽(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点R_のん気)
				c04結果.軽率_気軽 = "\t" + あり;
			else if (3 < c02結果.標準点T_思考性)
				c04結果.軽率_気軽 = "\t" + あり;
		}

		private static void 高得点_開放的_楽観(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点T_思考性)
				c04結果.開放的_楽観 = "\t" + あり;
			else if (3 < c02結果.標準点S_社会性)
				c04結果.開放的_楽観 = "\t" + あり;
		}

		private static void 高得点_非思索的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点R_のん気)
				c04結果.非思索的 = "\t" + あり;
			else if (3 < c02結果.標準点T_思考性)
				c04結果.非思索的 = "\t" + あり;
		}

		private static void 高得点_無頓着_のん気(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点T_思考性)
				c04結果.無頓着_のん気 = "\t" + あり;
		}

		private static void 高得点_自己顕示(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点A_支配性)
				c04結果.自己顕示 = "\t" + あり;
		}

		private static void 高得点_世話好き(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点A_支配性)
				c04結果.世話好き = "\t" + あり;
		}

		private static void 高得点_自信家(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点A_支配性)
				c04結果.自信家 = "\t" + あり;
			else if (3 < c02結果.標準点S_社会性)
				c04結果.自信家 = "\t" + あり;
		}

		private static void 高得点_指導的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点A_支配性)
				c04結果.指導的 = "\t" + あり;
		}

		private static void 高得点_社交的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点S_社会性)
				c04結果.社交的 = "\t" + あり;
		}

		private static void 高得点_派手好き(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点S_社会性)
				c04結果.派手好き = "\t" + あり;
		}

		private static void 高得点_強気(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点Ag_攻撃性)
				c04結果.強気 = "\t" + あり;
		}

		private static void 高得点_キビキビ(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 < c02結果.標準点G_活動性)
				c04結果.キビキビ = "\t" + あり;
		}


		private static void 低得点_充実感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点D_抑うつ性)
				c04結果.充実感 = "\t" + あり;
			else if (3 > c02結果.標準点O_主観性)
				c04結果.充実感 = "\t" + あり;
		}

		private static void 低得点_自信家(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点D_抑うつ性)
				c04結果.自信家 = "\t" + あり;
			else if (3 > c02結果.標準点I_劣等感)
				c04結果.自信家 = "\t" + あり;
			else if (3 > c02結果.標準点N_神経質)
				c04結果.自信家 = "\t" + あり;
		}

		private static void 低得点_楽天的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点D_抑うつ性)
				c04結果.楽天的 = "\t" + あり;
			else if (3 > c02結果.標準点I_劣等感)
				c04結果.楽天的 = "\t" + あり;
			else if (3 > c02結果.標準点N_神経質)
				c04結果.楽天的 = "\t" + あり;
			else if (3 > c02結果.標準点O_主観性)
				c04結果.楽天的 = "\t" + あり;
		}

		private static void 低得点_元気(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点D_抑うつ性)
				c04結果.元気 = "\t" + あり;
		}

		private static void 低得点_開放的_楽観(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点D_抑うつ性)
				c04結果.開放的_楽観 = "\t" + あり;
		}

		private static void 低得点_大胆_安心感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点C_気分の変化)
				c04結果.大胆_安心感 = "\t" + あり;
			else if (3 > c02結果.標準点I_劣等感)
				c04結果.大胆_安心感 = "\t" + あり;
			else if (3 > c02結果.標準点N_神経質)
				c04結果.大胆_安心感 = "\t" + あり;
			else if (3 > c02結果.標準点O_主観性)
				c04結果.大胆_安心感 = "\t" + あり;
		}

		private static void 低得点_集中力旺盛(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点C_気分の変化)
				c04結果.集中力旺盛 = "\t" + あり;
		}

		private static void 低得点_穏健(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点C_気分の変化)
				c04結果.穏健 = "\t" + あり;
			else if (3 > c02結果.標準点O_主観性)
				c04結果.穏健 = "\t" + あり;
		}

		private static void 低得点_冷静(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点C_気分の変化)
				c04結果.冷静 = "\t" + あり;
		}

		private static void 低得点_積極的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点I_劣等感)
				c04結果.積極的 = "\t" + あり;
		}

		private static void 低得点_果断_決断(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点I_劣等感)
				c04結果.果断_決断 = "\t" + あり;
		}

		private static void 低得点_優越感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点I_劣等感)
				c04結果.優越感 = "\t" + あり;
		}

		private static void 低得点_攻撃性(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点O_主観性)
				c04結果.攻撃性 = "\t" + あり;
		}

		private static void 低得点_現実主義(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点O_主観性)
				c04結果.現実主義 = "\t" + あり;
		}

		private static void 低得点_安定的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点O_主観性)
				c04結果.安定的 = "\t" + あり;
		}

		private static void 低得点_協調的_善意(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Co_協調性)
				c04結果.協調的_善意 = "\t" + あり;
		}

		private static void 低得点_幸運感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Co_協調性)
				c04結果.幸運感 = "\t" + あり;
		}

		private static void 低得点_満足感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点N_神経質)
				c04結果.満足感 = "\t" + あり;
			else if (3 > c02結果.標準点Co_協調性)
				c04結果.満足感 = "\t" + あり;
		}

		private static void 低得点_行動不活発(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Ag_攻撃性)
				c04結果.行動不活発 = "\t" + あり;
			else if (3 > c02結果.標準点G_活動性)
				c04結果.行動不活発 = "\t" + あり;
			else if (3 > c02結果.標準点R_のん気)
				c04結果.行動不活発 = "\t" + あり;
			else if (3 > c02結果.標準点T_思考性)
				c04結果.行動不活発 = "\t" + あり;
			else if (3 > c02結果.標準点A_支配性)
				c04結果.行動不活発 = "\t" + あり;
		}

		private static void 低得点_優柔不断(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Ag_攻撃性)
				c04結果.優柔不断 = "\t" + あり;
		}

		private static void 低得点_事なかれ主義(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Ag_攻撃性)
				c04結果.事なかれ主義 = "\t" + あり;
		}

		private static void 低得点_慎重(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Ag_攻撃性)
				c04結果.慎重 = "\t" + あり;
		}

		private static void 低得点_自己卑下(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Ag_攻撃性)
				c04結果.自己卑下 = "\t" + あり;
		}

		private static void 低得点_気長(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Ag_攻撃性)
				c04結果.気長 = "\t" + あり;
		}

		private static void 低得点_現状維持的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Ag_攻撃性)
				c04結果.現状維持的 = "\t" + あり;
			else if (3 > c02結果.標準点R_のん気)
				c04結果.現状維持的 = "\t" + あり;
		}

		private static void 低得点_非協調的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点G_活動性)
				c04結果.非協調的 = "\t" + あり;
		}

		private static void 低得点_不器用_非能率(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点G_活動性)
				c04結果.不器用_非能率 = "\t" + あり;
		}

		private static void 低得点_陰気_幻滅感(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点G_活動性)
				c04結果.陰気_幻滅感 = "\t" + あり;
			else if (3 > c02結果.標準点R_のん気)
				c04結果.陰気_幻滅感 = "\t" + あり;
		}

		private static void 低得点_批判的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点G_活動性)
				c04結果.批判的 = "\t" + あり;
		}

		private static void 低得点_無口(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点R_のん気)
				c04結果.無口 = "\t" + あり;
			else if (3 > c02結果.標準点A_支配性)
				c04結果.無口 = "\t" + あり;
			else if (3 > c02結果.標準点S_社会性)
				c04結果.無口 = "\t" + あり;
		}

		private static void 低得点_熟慮_重厚(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点R_のん気)
				c04結果.熟慮_重厚 = "\t" + あり;
			else if (3 > c02結果.標準点T_思考性)
				c04結果.熟慮_重厚 = "\t" + あり;
		}

		private static void 低得点_閉鎖的_悲観(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点T_思考性)
				c04結果.閉鎖的_悲観 = "\t" + あり;
			else if (3 > c02結果.標準点S_社会性)
				c04結果.閉鎖的_悲観 = "\t" + あり;
		}

		private static void 低得点_思索的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点R_のん気)
				c04結果.思索的 = "\t" + あり;
			else if (3 > c02結果.標準点T_思考性)
				c04結果.思索的 = "\t" + あり;
		}

		private static void 低得点_警戒心(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点T_思考性)
				c04結果.警戒心 = "\t" + あり;
		}

		private static void 低得点_引込み思案(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点A_支配性)
				c04結果.引込み思案 = "\t" + あり;
		}

		private static void 低得点_不干渉主義(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点A_支配性)
				c04結果.不干渉主義 = "\t" + あり;
		}

		private static void 低得点_自信欠如(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点A_支配性)
				c04結果.自信欠如 = "\t" + あり;
			else if (3 > c02結果.標準点S_社会性)
				c04結果.自信欠如 = "\t" + あり;
		}

		private static void 低得点_非指導的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点A_支配性)
				c04結果.非指導的 = "\t" + あり;
		}

		private static void 低得点_非社交的(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点S_社会性)
				c04結果.非社交的 = "\t" + あり;
		}

		private static void 低得点_地味(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点S_社会性)
				c04結果.地味 = "\t" + あり;
		}

		private static void 低得点_弱気(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点Ag_攻撃性)
				c04結果.弱気 = "\t" + あり;
		}

		private static void 低得点_グズ(C02系統値計算_結果 c02結果, C04基礎因子判定_結果 c04結果)
		{
			if (3 > c02結果.標準点G_活動性)
				c04結果.グズ = "\t" + あり;
		}


		//private static void 極小_人間嫌い()
		//{
		//    if (5 == c02結果.mi標準点S)
		//        c04結果.人間嫌い = "\t" + あり;
		//    else if (3 > c02結果.mi標準点O)
		//        c04結果.人間嫌い = "\t" + あり;
		//}


	}
}
