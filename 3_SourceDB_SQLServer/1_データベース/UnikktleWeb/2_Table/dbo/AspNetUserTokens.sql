USE [UnikktleWeb]
GO

/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 2019/04/28 19:25:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) COLLATE Japanese_CI_AS NOT NULL,
	[LoginProvider] [nvarchar](128) COLLATE Japanese_CI_AS NOT NULL,
	[Name] [nvarchar](128) COLLATE Japanese_CI_AS NOT NULL,
	[Value] [nvarchar](max) COLLATE Japanese_CI_AS NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC
	--,[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[AspNetUserTokens] TO  SCHEMA OWNER 
GO

