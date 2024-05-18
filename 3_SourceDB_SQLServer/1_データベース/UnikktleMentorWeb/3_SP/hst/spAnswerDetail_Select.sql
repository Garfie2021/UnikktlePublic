USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswerDetail_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswerDetail_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spAnswerDetail_Select]
	@UserNo			bigint,
	@AnswerId		int
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[AnswerNo] AS Id,
		[Answer] AS ‰ñ“š
	FROM
		[hst].[tAnswerDetail]
	WHERE
		[UserNo] = @UserNo AND
		[AnswerId] = @AnswerId
	ORDER BY
		[AnswerNo]
	;

END
GO
/*
*/