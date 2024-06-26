USE [Profile]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- クレジット購入履歴
CREATE TABLE [test].[tDebugLog](
	[RegisteredDate]	[datetime] 		NOT NULL default getdate(),
	[Log]				[varchar](max)	NOT NULL,
CONSTRAINT [PK_pay_tCreditHistory] PRIMARY KEY CLUSTERED
(
	[RegisteredDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

