USE [UnikktleWebCollectWork]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [hst].[tCollaborateKeywordCount_WebServer](
	[KeywordNo_元]			[bigint]	NOT NULL,	-- 相関元キーワード
	[KeywordNo_先_Count]	[bigint]	NOT NULL,	-- [KeywordNo_元]に紐付く[KeywordNo_先]の行数 WebServer側
CONSTRAINT [PK_hst_tCollaborateKeywordCount_WebServer] PRIMARY KEY CLUSTERED
(
	[KeywordNo_元] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO


