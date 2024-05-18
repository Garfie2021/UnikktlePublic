USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswerHistory_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswerHistory_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spAnswerHistory_Select]
	@UserNo		bigint
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[AnswerId] AS Id,
		[AnswerDateStart] AS AnswerDate
	FROM
		[hst].[tAnswerHistory]
	WHERE
		[UserNo] = @UserNo
	ORDER BY 
		[AnswerDateStart]
	;

END
GO
/*
*/