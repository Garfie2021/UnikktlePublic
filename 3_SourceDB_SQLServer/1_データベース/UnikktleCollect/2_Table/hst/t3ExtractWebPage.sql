USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- カラムの並び順を変えたら、BulkCopyの並びも合わせないと、書き込みに失敗するので要注意
CREATE TABLE [hst].[t3ExtractWebPage](
	[DomainNo]				[bigint]	NOT NULL,	-- [mst].[tDomain].[No]にリレーション。
	[UrlNo]					[bigint]	NOT NULL,	-- [mst].[tUrl].[No]とリレーション。
	[登録日時]				[datetime]			NOT NULL default getdate(),
	[更新日時]				[datetime]			NOT NULL default getdate(),
	[MeCabState]			[tinyint]			NULL,	-- null：未解析　1：形態素解析済み。
	[言語判定]				[tinyint]			NULL,	-- null：未判定　1：日本語　2：英語？
	[HtmlTag除外後]			[nvarchar](max)		NULL,
	[不要文字列除外後]		[nvarchar](max)		NULL,
	[英語連結名詞]			[nvarchar](max)		NULL,	-- ExtractEnglishConcatNoun.exe で抽出した英語連結名詞
	[英語連結名詞除外後]	[nvarchar](max)		NULL,	-- 英語連結名詞を除外した、MeCab計算対象の文字列。文節毎に改行してる。
	[MeCab名詞]				[nvarchar](max)		NULL,	-- MeCabExec.exe で抽出した名詞
CONSTRAINT [PK_hst_t3ExtractWebPage] PRIMARY KEY CLUSTERED
(
	[DomainNo] ASC,
	[UrlNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE)) ON [PRIMARY]
GO

