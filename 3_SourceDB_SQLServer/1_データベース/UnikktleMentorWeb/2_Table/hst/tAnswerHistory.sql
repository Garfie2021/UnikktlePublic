USE [UnikktleMentorWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [hst].[tAnswerHistory](
	[UserNo]			[bigint] 		NOT NULL,	-- [dbo].[AspNetUsers].[No]列にリレーション
	[AnswerId]			[int]			NOT NULL,	--　ユーザの診断毎のNo。　※「回答日時」のId化はミリ秒がロストする問題が発生して断念。
	[AnswerDateStart]	[datetime] 		NOT NULL,	-- 回答開始日時
	[AnswerDateEnd]		[datetime] 		NOT NULL,	-- 回答終了日時
CONSTRAINT [PK_hst_AnswerHistory] PRIMARY KEY CLUSTERED
(
	[UserNo] ASC,
	[AnswerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

