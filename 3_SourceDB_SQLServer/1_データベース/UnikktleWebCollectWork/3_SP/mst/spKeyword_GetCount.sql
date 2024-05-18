USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spKeyword_GetCount]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_GetCount] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_GetCount]
	@Cnt	bigint	OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		@Cnt = COUNT_BIG(*)
	FROM
		[mst].[tKeyword]
	;

END
GO
/*
*/

