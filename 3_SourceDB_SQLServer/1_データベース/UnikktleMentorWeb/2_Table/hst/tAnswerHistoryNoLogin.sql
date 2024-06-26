USE [UnikktleMentorWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [hst].[tAnswerHistoryNoLogin](
	[SessionId]			[varchar](100) 	NOT NULL,	-- 未ログインユーザーのSessionId
	[AnswerDateStart]	[datetime] 		NOT NULL,	-- 回答開始日時
	[AnswerDateEnd]		[datetime] 		NOT NULL,	-- 回答終了日時
CONSTRAINT [PK_hst_AnswerHistoryNoLogin] PRIMARY KEY CLUSTERED
(
	[SessionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

