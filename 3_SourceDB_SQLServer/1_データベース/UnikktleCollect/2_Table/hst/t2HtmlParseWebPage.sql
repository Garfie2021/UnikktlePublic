USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- カラムの並び順を変えたら、BulkCopyの並びも合わせないと、書き込みに失敗するので要注意
CREATE TABLE [hst].[t2HtmlParseWebPage](
	[DomainNo]				[bigint]	NOT NULL,	-- [mst].[tDomain].[No]にリレーション。
	[UrlNo]					[bigint]	NOT NULL,	-- [mst].[tUrl].[No]とリレーション。
	[State]					[tinyint]	NULL,		-- Null：未解析	0：解析失敗　1：解析成功
	[言語判定]				[tinyint]	NULL,		-- 言語判定結果。 Null：未解析　1：日本語を含む。解析対象。　2：日本語を含まない。解析対象外。
	[HtmlTag除外後1段階目]	[ntext]		NULL,		-- この列のデータは削除せずに再計算で使う。
	[HtmlTag除外後2段階目]	[ntext]		NOT NULL,	-- この列のデータは削除せずに再計算で使う。
CONSTRAINT [PK_hst_t2HtmlParseWebPage] PRIMARY KEY CLUSTERED
(
	[DomainNo] ASC,
	[UrlNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE)) ON [PRIMARY]
GO

