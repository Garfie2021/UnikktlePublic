USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswerDetailSupplement_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswerDetailSupplement_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [hst].[spAnswerDetailSupplement_Select]
	@UserNo			bigint,
	@AnswerId		int
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[AnswerId] AS Id,
		[Gender],
		[Career],
		[RecentHappenings]
	FROM
		[hst].[tAnswerDetailSupplement]
	WHERE
		[UserNo] = @UserNo AND
		[AnswerId] = @AnswerId
	;

END
GO
/*
*/