USE [UnikktleWeb_Archive]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 広告
CREATE TABLE [usr].[tAdverRelationWord](
	[ACV日時]			[datetime] 		NOT NULL default getdate(),	-- アーカイブした日時
	[UserNo]			[bigint] 		NOT NULL,	-- [usr].[tUserSetting].[No]列にリレーション
	[BusinessNo]		[smallint] 		NOT NULL,	-- [usr].[tBusiness].[No]列にリレーション
	[AdverNo]			[int] 			NOT NULL,	-- [usr].[tAdver].[No]列にリレーション
	[RelationWordNo]	[bigint] 		NOT NULL,	-- [clt].[tKeyword].[No]列にリレーション
	[ClickCost]			[int] 			NOT NULL default 0,
CONSTRAINT [PK_usr_tAdverRelationWord] PRIMARY KEY CLUSTERED
(
	[ACV日時] ASC,
	[UserNo] ASC,
	[BusinessNo] ASC,
	[AdverNo] ASC,
	[RelationWordNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

