USE [UnikktleMentorWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [hst].[tAnswerDetailSupplement](
	[UserNo]			[bigint] 		NOT NULL,	-- [dbo].[AspNetUsers].[No]列にリレーション
	[AnswerId]			[int] 			NOT NULL,	-- [hst].[tAnswerHistory].[AnswerId]列にリレーション
	[Gender]			[tinyint]		NOT NULL default 1,	-- 診断した時の性別
	[Career]			[int]			NOT NULL default 1,	-- 診断した時の職業
	[RecentHappenings]	[ntext] 		NOT NULL,	-- 最近の出来事
CONSTRAINT [PK_hst_AnswerDetailSupplement] PRIMARY KEY CLUSTERED
(
	[UserNo] ASC,
	[AnswerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

