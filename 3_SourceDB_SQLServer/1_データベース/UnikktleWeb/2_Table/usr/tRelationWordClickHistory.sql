USE [UnikktleWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 広告
CREATE TABLE [usr].[tRelationWordClickHistory](
	[WordNo]				[bigint] 		NOT NULL,	-- [clt].[tKeyword].[No]列にリレーション
	-- ログインしているユーザーはUserNoでユニークユーザーとする。
	-- ログインしていないユーザーは[ClickUserIP]と[ClickUserSessionId]の組み合わせでユニークユーザーとする。
	[ClickUserNo]			[bigint] 		NOT NULL,	-- [dbo].[AspNetUsers].[No]列にリレーション　-1：未ログイン
	[ClickUserIP]			[varchar](20)	NOT NULL
CONSTRAINT [PK_usr_tRelationWordClickHistory] PRIMARY KEY CLUSTERED
(
	[WordNo] ASC,
	[ClickUserNo] ASC,
	[ClickUserIP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

