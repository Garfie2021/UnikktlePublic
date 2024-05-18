USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- カラムの並び順を変えたら、BulkCopyの並びも合わせないと、書き込みに失敗するので要注意
CREATE TABLE [hst].[t1CollectWebPage](
	[DomainNo]				[bigint]		NOT NULL,			-- [mst].[tDomain].[No]にリレーション。
	[UrlNo]					[bigint]		NOT NULL,			-- [mst].[tUrl].[No]とリレーション。
	[CollectState]			[tinyint]		NOT NULL default 0,	-- 0：収集失敗　1：収集成功
	--[Language判定1段階目]	[tinyint]		NOT NULL default 0,	-- 言語判定結果。 0：未解析　1：日本語を含む。解析対象。　2：日本語を含まない。解析対象外。
	[CutoutStateUrl]		[tinyint]		NULL,				-- URL切り出しフラグ。	null：未解析　0：解析失敗　1：解析成功
	[CutoutStateHtml]		[tinyint]		NULL,				-- HTML切り出しフラグ。	null：未解析　0：解析失敗　1：解析成功
	--[HtmlParseResult]		[tinyint]		NULL,				-- null：未解析　0：解析失敗　1：解析成功
	[Html]					[ntext]			NULL,				-- ディスク容量を圧迫してきたら、この列のデータを削除する
CONSTRAINT [PK_hst_t1CollectWebPage] PRIMARY KEY CLUSTERED
(
	[DomainNo] ASC,
	[UrlNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE)) ON [PRIMARY]
GO

