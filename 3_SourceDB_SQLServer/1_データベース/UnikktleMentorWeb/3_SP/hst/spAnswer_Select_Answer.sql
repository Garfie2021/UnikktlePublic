USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswer_Select_Answer]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswer_Select_Answer] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spAnswer_Select_Answer]
	@UserNo			bigint,
	@AnswerDate		datetime
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[AnswerNo] AS Id,
		[Answer]
	FROM
		[hst].[tAnswer]
	WHERE
		[UserNo] = @UserNo AND
		[AnswerDate] = @AnswerDate
	ORDER BY
		[AnswerNo]
	;

END
GO
/*
*/