USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mst].[tUrl](
	[DomainNo]		[bigint]				NOT NULL,	-- [mst].[tDomain]テーブルの[No]にリレーション。
	[UrlNo]			[bigint]				NOT NULL,	-- Domain内での URL No。Domain毎に1からインクリメント。
	[CollectDate]	[datetime]				NULL,
	[Url]			[varchar](max)			NOT NULL,
CONSTRAINT [PK_tUrl] PRIMARY KEY CLUSTERED
(
	[DomainNo] ASC,
	[UrlNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

-- インデックス作成限界超えるURLが存在する
--CREATE UNIQUE NONCLUSTERED INDEX [IX_Url_1] ON [mst].[tUrl]
--(
--	[Url] ASC
--)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
--GO

