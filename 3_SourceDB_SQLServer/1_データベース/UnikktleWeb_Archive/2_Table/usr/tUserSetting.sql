USE [UnikktleWeb_Archive]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [usr].[tUserSetting](
	[ACV日時]			[datetime] 		NOT NULL default getdate(),	-- アーカイブした日時
	[No]					[bigint] 		NOT NULL,
	[BackgroundColor]		[tinyint]		NOT NULL default 1,
	[ExternalSearchEngine]	[tinyint]		NOT NULL default 1,
	[Gender]				[tinyint]		NOT NULL default 1,
	[BirthDate]				[date]			NOT NULL,
	[Career]				[int]			NOT NULL default 1,
	[IPv4]					[nvarchar](20)	NOT NULL,
	[OwnedCredit]			[int]			NOT NULL default 0,
CONSTRAINT [PK_usr_tUserSetting] PRIMARY KEY CLUSTERED
(
	[ACV日時] ASC,
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

