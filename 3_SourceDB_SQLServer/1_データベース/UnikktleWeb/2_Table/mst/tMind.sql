USE [UnikktleWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mst].[tMind](
	[No]					[bigint]								NOT NULL	IDENTITY(1,1),
	[UserNo]				[bigint] 								NOT NULL,	-- [dbo].[AspNetUsers].[No]列にリレーション
	[PublishOnlyToMe]		[bit]									NOT NULL	DEFAULT 0,
	[AllowOtherEdit]		[bit]									NOT NULL,
	[LastUpdate]			[datetime]								NOT NULL,
	[Title]					[nvarchar](100)	COLLATE Japanese_CI_AS	NOT NULL,
	[Explanation]			[ntext]			COLLATE Japanese_CI_AS	NOT NULL,
	--[Item]					[ntext]									NOT NULL,
	[Item_SpaceSeparator]	[ntext]			COLLATE Japanese_CI_AS	NOT NULL,	-- 全文検索用。Item列の値をスペース区切りにしたデータ。
	[ItemRelation]			[ntext]									NOT NULL,
	[JsonViewModel]			[ntext]									NOT NULL,
CONSTRAINT [PK_mst_tMind2] PRIMARY KEY CLUSTERED
(
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO


-- テストデータ
--INSERT INTO [mst].[tMind] ([Title],[Unikktile],[UnikktileJson]) VALUES ('タイトル１', 'Rect|1|10|20|100|50|#ffffa3|orange|5\n', 'b');
--INSERT INTO [mst].[tMind] ([Title],[Unikktile],[UnikktileJson]) VALUES ('タイトル２', 'a', 'b');
