USE [UnikktleWebCollectWork]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mst].[tKeyword](
	[No]				[bigint]			NOT NULL,
	[r_w]				[smallint]			NOT NULL,		-- Rectの幅
	[Word]				[nvarchar](100)		COLLATE Japanese_BIN2	NOT NULL,
	[FullTextSupple]	[nvarchar](max)		COLLATE Japanese_BIN2	NULL,	-- 全文検索で使うWordの説明文
CONSTRAINT [PK_mst_tKeyword] PRIMARY KEY CLUSTERED
(
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

