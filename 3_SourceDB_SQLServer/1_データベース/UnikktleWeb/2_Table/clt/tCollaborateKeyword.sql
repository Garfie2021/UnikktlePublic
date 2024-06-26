USE [UnikktleWeb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [clt].[tCollaborateKeyword](
	[KeywordNo_元]				[bigint]	NOT NULL DEFAULT 0,
	[KeywordNo_先]				[bigint]	NOT NULL DEFAULT 0,
	[同時出現ドキュメント数]	[bigint]	NOT NULL DEFAULT 0,
CONSTRAINT [PK_clt_tCollaborateKeyword] PRIMARY KEY CLUSTERED 
(
	[KeywordNo_元] ASC,
	[KeywordNo_先] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO
