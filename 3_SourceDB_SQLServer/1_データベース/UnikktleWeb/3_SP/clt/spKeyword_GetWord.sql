USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spKeyword_GetWord]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spKeyword_GetWord] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [clt].[spKeyword_GetWord]
	@No		bigint,
	@Word	[nvarchar](100) OUTPUT
AS
BEGIN

	--declare	@No		bigint = 1;
	--declare	@Word	[nvarchar](100);

	SELECT
		@Word = [Word]
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