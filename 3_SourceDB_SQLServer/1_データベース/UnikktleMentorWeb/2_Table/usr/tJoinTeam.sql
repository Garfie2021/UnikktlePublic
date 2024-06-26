USE [UnikktleMentorWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [usr].[tJoinTeam](
	[UserNo]	[bigint] 	NOT NULL,			-- [usr].[tUserSetting].[No]列にリレーション
	[TeamNo]	[int] 		NOT NULL,			-- [mst].[tTeam].[No]列にリレーション
	[Status]	[tinyint]	NOT NULL default 1,	-- 1：参加申請中　2：参加済み　3：チームオーナー
CONSTRAINT [PK_usr_tJoinTeam] PRIMARY KEY CLUSTERED
(
	[UserNo] ASC,
	[TeamNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

