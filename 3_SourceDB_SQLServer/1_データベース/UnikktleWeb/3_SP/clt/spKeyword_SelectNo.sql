USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spKeyword_SelectNo]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spKeyword_SelectNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [clt].[spKeyword_SelectNo]
	@No		bigint
AS
BEGIN

	--declare	@No		bigint = 1;
	--declare	@Word	[nvarchar](100);

	SELECT
		[No] as Id,
		[r_w],
		[Word]
	FROM
		[clt].[tKeyword]
	WHERE
		[No] = @No
	;

	--SELECT @Word;

END
GO
/*
*/