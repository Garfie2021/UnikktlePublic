USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spCollaborateKeywordCount_WebServer_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeywordCount_WebServer_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spCollaborateKeywordCount_WebServer_Insert]
AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_WebServer] (
		[KeywordNo_元],
		[KeywordNo_先_Count])
	SELECT
		[KeywordNo_元],
		COUNT([KeywordNo_元]) AS Cnt
	FROM
		[UnikktleWeb].[clt].[tCollaborateKeyword]
	GROUP BY
		[KeywordNo_元]

END
GO
/*
*/

