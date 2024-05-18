USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spCollaborateKeywordCount_WebServer_GetCount]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeywordCount_WebServer_GetCount] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spCollaborateKeywordCount_WebServer_GetCount]
	@Cnt	bigint	OUTPUT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		@Cnt = COUNT_BIG(*)
	FROM
		[hst].[tCollaborateKeywordCount_WebServer]
	;

END
GO
/*
*/

