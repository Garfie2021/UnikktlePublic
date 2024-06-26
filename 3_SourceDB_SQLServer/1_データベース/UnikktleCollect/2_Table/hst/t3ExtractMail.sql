USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- メールは1つに複数件含まれることは無いので、WEB検索のようにtExtract〇〇テーブルは設けない
CREATE TABLE [hst].[t3ExtractMail](
	-- カラムの並び順を変えたら、BulkCopyの並びも合わせないと、書き込みに失敗するので要注意
	[CollectTargetNo]			[bigint]		NOT NULL,	-- [mst].[tCollectTarget].[No]とリレーション。　※旧「FromMailAddressNo」
	[SendDate]					[datetime]		NOT NULL,
	[登録日時]					[datetime]		NOT NULL default getdate(),
	[更新日時]					[datetime]		NOT NULL default getdate(),
	[MeCabState]				[tinyint]		NOT NULL default 0,	-- 0：未解析　1：形態素解析済み。
	[言語判定]					[tinyint]		NOT NULL default 0,	-- 0：未判定　1：日本語　2：英語？
	[不要文字列除外後]			[nvarchar](max)	NULL,
	[英語連結名詞]				[nvarchar](max)	NULL,	-- ExtractEnglishConcatNoun.exe で抽出した英語連結名詞
	[英語連結名詞除外後]		[nvarchar](max)	NULL,	-- 件名と本文を連結し、英語連結名詞を除外した、MeCab計算対象の文字列。文節毎に改行してる。
	[MeCab名詞]					[nvarchar](max)	NULL,	-- MeCabExec.exe で抽出した名詞
CONSTRAINT [PK_hst_t3ExtractMail] PRIMARY KEY CLUSTERED
(
	[CollectTargetNo] ASC,
	[SendDate] ASC,
	[登録日時] ASC	-- 登録日時もプライマリキーに含め、BulkCopyで同じメールが複数あった場合、BulkCopyが落ちる問題に対処。
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE)) ON [PRIMARY]
GO

