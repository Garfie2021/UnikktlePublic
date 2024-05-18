USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spKeyword_SelectNo_FullColumn]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spKeyword_SelectNo_FullColumn] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [clt].[spKeyword_SelectNo_FullColumn]
	@No		bigint
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[No] as Id,
		[r_w],
		[Word],
		[FullTextSupple]
	FROM
		[clt].[tKeyword]
	WHERE
		[No] = @No
	;

END
GO
/*
*/