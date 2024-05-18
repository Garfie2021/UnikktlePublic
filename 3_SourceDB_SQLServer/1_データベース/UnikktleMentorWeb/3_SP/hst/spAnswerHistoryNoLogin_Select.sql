USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswerHistoryNoLogin_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswerHistoryNoLogin_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spAnswerHistoryNoLogin_Select]
	@SessionId	varchar(100)
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		1 AS Id,
		[AnswerDateStart] AS AnswerDate
	FROM
		[hst].[tAnswerHistoryNoLogin]
	WHERE
		[SessionId] = @SessionId
	ORDER BY 
		[AnswerDateStart]
	;

END
GO
/*
*/