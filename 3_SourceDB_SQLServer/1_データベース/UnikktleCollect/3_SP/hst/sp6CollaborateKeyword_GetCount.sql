USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp6CollaborateKeyword_GetCount]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp6CollaborateKeyword_GetCount] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp6CollaborateKeyword_GetCount]
	@Cnt	bigint	OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		@Cnt = COUNT_BIG(*)
	FROM
		[hst].[t6CollaborateKeyword]
	;

END
GO
/*
*/

