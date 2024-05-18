USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[pay].[spCreditHistory_SelectUserNo]', N'P') IS NOT NULL
	DROP PROCEDURE [pay].[spCreditHistory_SelectUserNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [pay].[spCreditHistory_SelectUserNo]
	@UserNo		bigint
AS
BEGIN

	--declare	@UserNo		bigint = 1;

	SELECT
		[No] AS Id,
		[RegisteredDate],
		[AddCredit]
	FROM
		[pay].[tCreditHistory]
  	WHERE
		[UserNo] = @UserNo
	ORDER BY
		[RegisteredDate] DESC
	;


END
GO
/*
*/