USE [UnikktleCollect]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [hst].[t6CollaborateKeyword](
	[KeywordNo_元]				[bigint]		NOT NULL,	-- 相関元キーワード
	[KeywordNo_先]				[bigint]		NOT NULL,	-- 相関先キーワード
	[同時出現ドキュメント数]	[bigint]		NOT NULL,
	[登録日時]					[datetime]		NULL default getdate(),
	[更新日時]					[datetime]		NULL default getdate(),
CONSTRAINT [PK_t6CollaborateKeyword] PRIMARY KEY CLUSTERED
(
	[KeywordNo_元] ASC,
	[KeywordNo_先] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO


