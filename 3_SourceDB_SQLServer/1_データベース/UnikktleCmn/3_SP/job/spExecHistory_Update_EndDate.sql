USE UnikktleCmn
GO

DROP PROCEDURE [job].[spExecHistory_Update_EndDate]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [job].[spExecHistory_Update_EndDate]
	@Type			[tinyint],
	@EndDate		[datetime]
AS
BEGIN


	--DECLARE @EndDate datetime = GETDATE()
	DECLARE @StartDate datetime;

	SELECT TOP 1
		@StartDate = [StartDate]
	FROM
		[job].[tExecHistory]
	WHERE
		[Type] = @Type AND [EndDate] is null
	;

	UPDATE
		[job].[tExecHistory]
	SET
		[EndDate] = @EndDate,
		[ExecMinute] = DATEDIFF(minute, @StartDate, @EndDate)
	WHERE
		[Type] = @Type AND [EndDate] is null
	;

	--DECLARE @Type tinyint = 130

	--SELECT * FROM [job].[tExecHistory]
	--	WHERE
	--	[Type] = @Type AND @EndDate is null

END
GO

