/*
使ってない

USE [UnikktleMentorWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [hst].[tC01粗点](
	[UserNo]	[bigint] 	NOT NULL,	-- [dbo].[AspNetUsers].[No]列にリレーション
	[AnswerId]	[int] 		NOT NULL,	-- [hst].[tAnswerHistory].[AnswerId]列にリレーション
	[S]			[tinyint]   NOT NULL,
	[A]			[tinyint]   NOT NULL,
	[T]			[tinyint]   NOT NULL,
	[R]			[tinyint]   NOT NULL,
	[G]			[tinyint]   NOT NULL,
	[Ag]		[tinyint]   NOT NULL,
	[Co]		[tinyint]   NOT NULL,
	[O]			[tinyint]   NOT NULL,
	[N]			[tinyint]   NOT NULL,
	[I]			[tinyint]   NOT NULL,
	[C]			[tinyint]   NOT NULL,
	[D]			[tinyint]   NOT NULL,
CONSTRAINT [PK_hst_C01粗点] PRIMARY KEY CLUSTERED
(
	[UserNo] ASC,
	[AnswerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

*/