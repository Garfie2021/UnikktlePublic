USE [UnikktleMentorWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [hst].[tAnswerDetail](
	[UserNo]		[bigint] 		NOT NULL,	-- [dbo].[AspNetUsers].[No]列にリレーション
	[AnswerId]		[int] 			NOT NULL,	-- [hst].[tAnswerHistory].[AnswerId]列にリレーション
	[AnswerNo]		[tinyint] 		NOT NULL,	-- 回答No（※設問120項目に対応）
	[Answer]		[tinyint] 		NOT NULL,	-- 1:はい　2:いいえ　3:どちらともいえない　4:未回答
CONSTRAINT [PK_hst_Keyword] PRIMARY KEY CLUSTERED
(
	[UserNo] ASC,
	[AnswerId] ASC,
	[AnswerNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

