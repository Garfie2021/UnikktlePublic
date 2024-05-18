USE [UnikktleWeb]
GO

/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 2019/04/28 19:25:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) COLLATE Japanese_CI_AS NOT NULL,
	[ClaimType] [nvarchar](max) COLLATE Japanese_CI_AS NULL,
	[ClaimValue] [nvarchar](max) COLLATE Japanese_CI_AS NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[AspNetRoleClaims] TO  SCHEMA OWNER 
GO

/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 2019/04/28 19:25:58 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]') AND name = N'IX_AspNetRoleClaims_RoleId')
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO


/****** Object:  Statistic [IX_AspNetRoleClaims_RoleId]    Script Date: 2019/04/28 19:25:58 ******/
if  exists (select * from sys.stats where name = N'IX_AspNetRoleClaims_RoleId' and object_id = object_id(N'[dbo].[AspNetRoleClaims]'))
UPDATE STATISTICS [dbo].[AspNetRoleClaims]([IX_AspNetRoleClaims_RoleId]) WITH STATS_STREAM = 0x0100000002000000000000000000000084D810120000000058000000000000000000000000000000E7030000E7000000840300000000000010D0000000000000380300003800000004000A00000000000000000000000000, ROWCOUNT = 0, PAGECOUNT = 0
GO

/****** Object:  Statistic [PK_AspNetRoleClaims]    Script Date: 2019/04/28 19:25:58 ******/
if  exists (select * from sys.stats where name = N'PK_AspNetRoleClaims' and object_id = object_id(N'[dbo].[AspNetRoleClaims]'))
UPDATE STATISTICS [dbo].[AspNetRoleClaims]([PK_AspNetRoleClaims]) WITH STATS_STREAM = 0x01000000010000000000000000000000ED0358EE0000000040000000000000000000000000000000380300003800000004000A00000000000000000000000000, ROWCOUNT = 0, PAGECOUNT = 0
GO
