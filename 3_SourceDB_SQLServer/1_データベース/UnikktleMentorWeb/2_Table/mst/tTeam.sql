USE [UnikktleMentorWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [mst].[tTeam](
	[No]					[int] 			NOT NULL IDENTITY(1,1),
	[TeamID]				[nvarchar](100)	NOT NULL,
	[TeamExplanation]		[nvarchar](200)	NOT NULL,
	[AllowPublic]			[bit]			NOT NULL,	-- false：公開しない　true：公開する　※checkboxの選択状態として使うので、enumにはしない。
	[AllowApplyToJoinTeam]	[bit]			NOT NULL,	-- false：参加申請を受け付けない　true：参加申請を受け付ける　※checkboxの選択状態として使うので、enumにはしない。
	[CreateUserNo]			[bigint] 		NOT NULL,
	[CreateDate]			[datetime] 		NOT NULL default getdate(),
	[UpdateUserNo]			[bigint] 		NOT NULL,
	[UpdateDate]			[datetime] 		NOT NULL default getdate(),
CONSTRAINT [PK_mst_tTeam] PRIMARY KEY CLUSTERED
(
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UX_mst_Team_TeamID] ON [mst].[tTeam]
(
	[TeamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO
