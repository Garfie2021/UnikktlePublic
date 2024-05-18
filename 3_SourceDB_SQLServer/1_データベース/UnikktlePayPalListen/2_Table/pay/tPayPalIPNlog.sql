USE [UnikktlePayPalListen]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [pay].[tPayPalIPNlog](
	[No]				[bigint] 		NOT NULL IDENTITY(1,1),
	[RegisteredDate]	[datetime] 		NOT NULL,
	[RequestBody]		[varchar](max)	NOT NULL,
CONSTRAINT [PK_pay_tPayPalIPNlog] PRIMARY KEY CLUSTERED
(
	[No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF	, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

