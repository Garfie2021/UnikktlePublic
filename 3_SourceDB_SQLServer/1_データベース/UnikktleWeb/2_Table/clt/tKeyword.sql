USE [UnikktleWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [clt].[tKeyword](
	[No]				[bigint]			NOT NULL,
	[r_w]				[smallint]			NOT NULL,		-- Rectの幅
	[Word]				[nvarchar](100)		COLLATE Japanese_BIN2	NOT NULL,
	[FullTextSupple]	[nvarchar](max)		COLLATE Japanese_BIN2	NOT NULL,	-- 全文検索で使うWordの説明文
CONSTRAINT [PK_clt_Keyword] PRIMARY KEY CLUSTERED
(
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_clt_Keyword] ON [clt].[tKeyword]
(
	[Word] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

