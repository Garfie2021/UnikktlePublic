USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spCareer_SelectAll]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spCareer_SelectAll] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spCareer_SelectAll]
AS
BEGIN

	--declare	@CareerCategoryNo		int = 1;

	SELECT
		[No] AS Id,
		[CareerName] AS Name
	FROM
		[mst].[tCareer]
	;

END
GO
/*
*/