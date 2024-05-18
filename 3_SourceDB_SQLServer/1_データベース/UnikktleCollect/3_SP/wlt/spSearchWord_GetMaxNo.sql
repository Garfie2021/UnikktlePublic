USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[wlt].[spSearchWord_GetMaxNo]', N'P') IS NOT NULL
	DROP PROCEDURE [wlt].[spSearchWord_GetMaxNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wlt].[spSearchWord_GetMaxNo]
	@No	bigint	output
AS
BEGIN

	SELECT
		@No = MAX([No])
	FROM
		[wlt].[tSearchWord]
	;

END
GO
/*
*/

