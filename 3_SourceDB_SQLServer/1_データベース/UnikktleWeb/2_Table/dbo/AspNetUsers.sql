USE [UnikktleWeb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUsers](
	[No]							[bigint] IDENTITY(1,1)	NOT NULL,
	[Id]					[nvarchar](450) COLLATE Japanese_CI_AS NOT NULL,
	[UserName]				[nvarchar](256) COLLATE Japanese_CI_AS NULL,
	[NormalizedUserName]	[nvarchar](256) COLLATE Japanese_CI_AS NULL,
	[Email]					[nvarchar](256) COLLATE Japanese_CI_AS NULL,
	[NormalizedEmail]		[nvarchar](256) COLLATE Japanese_CI_AS NULL,
	[EmailConfirmed]		[bit] NOT NULL,
	[PasswordHash]			[nvarchar](max) COLLATE Japanese_CI_AS NULL,
	[SecurityStamp]			[nvarchar](max) COLLATE Japanese_CI_AS NULL,
	[ConcurrencyStamp]		[nvarchar](max) COLLATE Japanese_CI_AS NULL,
	[PhoneNumber]			[nvarchar](max) COLLATE Japanese_CI_AS NULL,
	[PhoneNumberConfirmed]	[bit] NOT NULL,
	[TwoFactorEnabled]		[bit] NOT NULL,
	[LockoutEnd]			[datetimeoffset](7) NULL,
	[LockoutEnabled]		[bit] NOT NULL,
	[AccessFailedCount]		[int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO



CREATE UNIQUE NONCLUSTERED INDEX [NoIndex] ON [dbo].[AspNetUsers]
(
	[No] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
GO



UPDATE STATISTICS [dbo].[AspNetUsers]([EmailIndex]) WITH STATS_STREAM = 0x010000000200000000000000000000000EB219520000000058000000000000000000000000000000E7020000E7000000000200000000000010D0000000000000E7030000E7000000840300000000000010D0000000000000, ROWCOUNT = 1, PAGECOUNT = 1
GO

UPDATE STATISTICS [dbo].[AspNetUsers]([PK_AspNetUsers]) WITH STATS_STREAM = 0x01000000010000000000000000000000802CDC930000000040000000000000000000000000000000E7030000E7000000840300000000000010D0000000000000, ROWCOUNT = 1, PAGECOUNT = 1
GO
