USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spCareer_Select_JA]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spCareer_Select_JA] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spCareer_Select_JA]
	@CareerCategoryNo		int
AS
BEGIN

	--declare	@CareerCategoryNo		int = 1;

	SELECT
		[No] AS Id,
		[CareerName_JA] AS Name
	FROM
		[mst].[tCareer]
	WHERE
		[CareerCategoryNo] = @CareerCategoryNo
	;

END
GO
/*
*/