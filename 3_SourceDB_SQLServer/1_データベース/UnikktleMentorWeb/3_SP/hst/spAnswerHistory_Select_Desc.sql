USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswerHistory_Select_Desc]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswerHistory_Select_Desc] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spAnswerHistory_Select_Desc]
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
		[AnswerDateStart] DESC
	;

END
GO
/*
*/