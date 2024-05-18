USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_SelectNo_JsonViewModel]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_SelectNo_JsonViewModel] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spMind_SelectNo_JsonViewModel]
	@No		bigint
AS
BEGIN

	SELECT
		[No] as Id,
		[JsonViewModel]
	FROM
		[mst].[tMind]
	WHERE
		[No] = @No
	;

END
GO
/*
*/

