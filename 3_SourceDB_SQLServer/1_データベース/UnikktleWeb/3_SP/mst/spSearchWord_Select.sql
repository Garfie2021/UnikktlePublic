USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spSearchWord_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spSearchWord_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spSearchWord_Select]
	@No		bigint
AS
BEGIN

	SELECT 
		[No],
		[Word]
	FROM
		[mst].[tSearchWord]
	WHERE
		[No] > @No
	;

END
GO
/*
*/

