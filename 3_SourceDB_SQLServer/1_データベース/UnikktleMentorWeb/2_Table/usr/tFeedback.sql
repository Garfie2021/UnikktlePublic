USE [UnikktleMentorWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [usr].[tFeedback](
	[UserNo]		[bigint] 		NOT NULL,	-- [usr].[tUserSetting].[No]列にリレーション
	[No]			[int] 			NOT NULL,	
	[Category]		[tinyint]		NOT NULL default 1,
	[登録日時]		[datetime] 		NOT NULL default getdate(),
	[Subject]		[nvarchar](100)	NOT NULL,
	[Text]			[nvarchar](500)	NOT NULL,
CONSTRAINT [PK_usr_tFeedback] PRIMARY KEY CLUSTERED
(
	[UserNo] ASC,
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

