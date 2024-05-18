USE [UnikktleWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [usr].[tBusiness](
	[UserNo]				[bigint] 		NOT NULL,	-- [usr].[tUserSetting].[No]列にリレーション
	[No]					[smallint] 		NOT NULL,
	[Category]				[tinyint] 		NOT NULL,	-- 法人 = 1,  個人 = 2,  その他 = 3,
	[OrganizationName]		[nvarchar](50)	NOT NULL,	--組織名
	[OrganizationURL]		[varchar](200)	NOT NULL,	--組織URL
	--[BusinessURL]			[varchar](200)	NOT NULL,	--ビジネスURL
CONSTRAINT [PK_usr_tBusiness] PRIMARY KEY CLUSTERED
(
	[UserNo] ASC,
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

