USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- カラムの並び順を変えたら、BulkCopyの並びも合わせないと、書き込みに失敗するので要注意
CREATE TABLE [hst].[t2HtmlParseGoogle](
	[SearchKeywordNo]			[bigint]			NOT NULL,	-- [mst].[tKeyword].[No]とリレーション
	[SearchDate]				[datetime]			NOT NULL,
	[State]						[tinyint]			NOT NULL default 0,	-- 0：未解析　1：解析済み
	[HtmlTag除外後1段階目]		[nvarchar](max)		NULL,	-- Google固有Htmlタグの内、大きいものを除去した状態。
	[HtmlTag除外後2段階目]		[nvarchar](max)		NULL,	-- Htmlタグの内、大きいものを除去した状態。
CONSTRAINT [PK_hst_t2HtmlParseGoogle] PRIMARY KEY CLUSTERED
(
	[SearchKeywordNo] ASC,
	[SearchDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE)) ON [PRIMARY]
GO

