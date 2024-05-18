USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spCollaborateKeywordCount_CollectServer_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Insert]
AS
BEGIN

	SET NOCOUNT ON;


	INSERT INTO [UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_CollectServer] (
		[KeywordNo_��],
		[KeywordNo_��_Count])
	SELECT
		[KeywordNo_��],
		COUNT([KeywordNo_��]) AS Cnt
	FROM
		[UnikktleCollect].[hst].[t6CollaborateKeyword]
	GROUP BY
		[KeywordNo_��]

END
GO
/*
*/

