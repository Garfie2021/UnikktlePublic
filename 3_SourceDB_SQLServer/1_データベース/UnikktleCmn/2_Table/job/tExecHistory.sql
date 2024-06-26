USE [UnikktleCmn]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [job].[tExecHistory](
	[Type]			[tinyint]		NOT NULL,	-- C#側の「enum ジョブType」と連動する。
	[StartDate]		[datetime]		NOT NULL,
	[EndDate]		[datetime]		NULL,
	[ExecMinute]	[int]			NULL,		-- 実行時間。([EndDate]-[StartDate])。
CONSTRAINT [PK_job_tExecHistory] PRIMARY KEY CLUSTERED
(
	[Type] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, DATA_COMPRESSION = PAGE) ON [PRIMARY]
) ON [PRIMARY]
GO

