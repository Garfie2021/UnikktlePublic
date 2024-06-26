USE [UnikktleWeb_Archive]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 広告
CREATE TABLE [usr].[tAdverRelation](
	[ACV日時]			[datetime] 		NOT NULL default getdate(),	-- アーカイブした日時
	[UserNo]			[bigint] 		NOT NULL,	-- [usr].[tUserSetting].[No]列にリレーション
	[BusinessNo]		[smallint] 		NOT NULL,	-- [usr].[tBusiness].[No]列にリレーション
	[No]				[int] 			NOT NULL,
	[Valid]				[tinyint] 		NOT NULL,	-- 無効=0 有効=1
	[Category]			[tinyint] 		NOT NULL,	-- 無料=1 有料=2
	[AdverName]			[nvarchar](100)	NOT NULL,	-- 広告名
	[AdverURL]			[varchar](200)	NOT NULL,	-- 広告URL
	[AdvertisingBudget]	[int] 			NOT NULL,	-- 広告予算
	[更新日時]			[datetime] 		NOT NULL default getdate(),
	-- PRクリックでインクリメント
	[ClickCnt]			[int] 			NOT NULL default 0,	-- クリックカウンター
	[TotalClickCost]	[int] 			NOT NULL default 0,	-- クリック総コスト
CONSTRAINT [PK_usr_tAdverRelation] PRIMARY KEY CLUSTERED
(
	[ACV日時] ASC,
	[UserNo] ASC,
	[BusinessNo] ASC,
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

