USE [UnikktleWebCollectWork]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [hst].[tCollaborateKeyword](
	[KeywordNo_元]				[bigint]		NOT NULL,	-- 相関元キーワード
	[KeywordNo_先]				[bigint]		NOT NULL,	-- 相関先キーワード
	[同時出現ドキュメント数]	[bigint]		NOT NULL,
CONSTRAINT [PK_hst_tCollaborateKeyword] PRIMARY KEY CLUSTERED
(
	[KeywordNo_元] ASC,
	[KeywordNo_先] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO


