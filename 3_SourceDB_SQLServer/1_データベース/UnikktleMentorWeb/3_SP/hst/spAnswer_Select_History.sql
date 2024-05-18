USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswer_Select_History]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswer_Select_History] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spAnswer_Select_History]
	@UserNo		bigint
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		DISTINCT [AnswerDate] AS Id
	FROM
		[hst].[tAnswer]
	WHERE
		[UserNo] = @UserNo
	;

END
GO
/*
*/