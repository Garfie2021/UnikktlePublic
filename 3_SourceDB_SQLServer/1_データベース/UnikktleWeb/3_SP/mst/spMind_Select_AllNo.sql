USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_Select_AllNo]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_Select_AllNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spMind_Select_AllNo]
AS
BEGIN

	SELECT 
		[No]
	FROM
		[mst].[tMind]
	;

END
GO
/*
*/

