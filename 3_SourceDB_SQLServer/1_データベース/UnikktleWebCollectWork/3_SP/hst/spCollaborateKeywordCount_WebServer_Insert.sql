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
		[KeywordNo_��],
		[KeywordNo_��_Count])
	SELECT
		[KeywordNo_��],
		COUNT([KeywordNo_��]) AS Cnt
	FROM
		[UnikktleWeb].[clt].[tCollaborateKeyword]
	GROUP BY
		[KeywordNo_��]

END
GO
/*
*/

