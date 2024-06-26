USE [UnikktleWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 広告
CREATE TABLE [usr].[tAdverSearchClickHistory](
	[UserNo]				[bigint] 		NOT NULL,	-- [dbo].[AspNetUsers].[No]列にリレーション
	[BusinessNo]			[smallint] 		NOT NULL,	-- [usr].[tBusiness].[No]列にリレーション
	[AdverNo]				[int] 			NOT NULL,	-- [usr].[tAdverSearch].[No]列にリレーション
	[WordNo]				[bigint] 		NOT NULL,	-- [msr].[tSearchWord].[No]列にリレーション
	[ClickDate]				[datetime] 		NOT NULL default getdate(),
	[ClickCost]				[int] 			NOT NULL,
	-- ログインしているユーザーはUserNoでユニークユーザーとする。
	-- ログインしていないユーザーは[ClickUserIP]と[ClickUserSessionId]の組み合わせでユニークユーザーとする。
	[ClickUserNo]			[bigint] 		NOT NULL,	-- [dbo].[AspNetUsers].[No]列にリレーション　-1：未ログイン
	[ClickUserIP]			[varchar](20)	NOT NULL,
	--[ClickUserSessionId]	[varchar](50)	NOT NULL 使って無い。
CONSTRAINT [PK_usr_tAdverSearchClickHistory] PRIMARY KEY CLUSTERED
(
	[UserNo] ASC,
	[BusinessNo] ASC,
	[AdverNo] ASC,
	[WordNo] ASC,
	[ClickDate] ASC,
	[ClickUserNo] ASC,
	[ClickUserIP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO
