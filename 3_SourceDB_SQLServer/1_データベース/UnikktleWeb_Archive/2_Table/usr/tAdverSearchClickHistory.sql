USE [UnikktleWeb_Archive]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 広告
CREATE TABLE [usr].[tAdverSearchClickHistory](
	[ACV日時]			[datetime] 		NOT NULL default getdate(),	-- アーカイブした日時
	[UserNo]		[bigint] 		NOT NULL,	-- [usr].[tUserSetting].[No]列にリレーション
	[BusinessNo]	[smallint] 		NOT NULL,	-- [usr].[tBusiness].[No]列にリレーション
	[AdverNo]		[int] 			NOT NULL,	-- [usr].[tAdverSearch].[No]列にリレーション
	[ClickUserNo]	[bigint] 		NOT NULL,	-- [usr].[tUserSetting].[No]列にリレーション
	[WordNo]		[bigint] 		NOT NULL,	-- [msr].[tSearchWord].[No]列にリレーション
	[ClickDate]		[datetime] 		NOT NULL default getdate(),
	[ClickCost]		[int] 			NOT NULL
CONSTRAINT [PK_usr_tAdverSearchClickHistory] PRIMARY KEY CLUSTERED
(
	[ACV日時] ASC,
	[UserNo] ASC,
	[BusinessNo] ASC,
	[AdverNo] ASC,
	[ClickUserNo] ASC,
	[WordNo] ASC,
	[ClickDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

