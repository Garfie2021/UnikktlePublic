USE UnikktleCmn
GO

DROP PROCEDURE [job].[spExecHistory_Insert]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [job].[spExecHistory_Insert]
	@Type			[tinyint],
	@StartDate		[datetime],
	@EndDate		[datetime]
AS
BEGIN

	INSERT INTO [job].[tExecHistory](
		[Type],
		[StartDate],
		[EndDate],
		[ExecMinute]
    ) VALUES (
		@Type,
		@StartDate,
		@EndDate,
		DATEDIFF(minute, @StartDate, @EndDate)
	);

END
GO

