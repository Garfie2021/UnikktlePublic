USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[pay].[spCreditHistory_GetCnt]', N'P') IS NOT NULL
	DROP PROCEDURE [pay].[spCreditHistory_GetCnt] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [pay].[spCreditHistory_GetCnt]
	@UserNo		bigint,
	@txn_id		varchar(50),
	@Cnt		int OUTPUT
AS
BEGIN
	
	SELECT
		@Cnt = COUNT(*)
	FROM
		[pay].[tCreditHistory]
	WHERE
		[UserNo] = @UserNo AND
		[txn_id] = @txn_id AND
		DATEDIFF(hour, [RegisteredDate], GETDATE()) < 24;	-- 24ŽžŠÔˆÈã‘O‚Í‚ ‚è‚¦‚È‚¢BˆÙí‚ÆŒ©‚È‚·B
	;
	
END
GO
/*
*/