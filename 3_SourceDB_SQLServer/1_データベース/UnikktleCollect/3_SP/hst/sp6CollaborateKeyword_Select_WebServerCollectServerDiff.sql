USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp6CollaborateKeyword_Select_WebServerCollectServerDiff]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp6CollaborateKeyword_Select_WebServerCollectServerDiff] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �Čv�Z�X�g�A�h
CREATE PROCEDURE [hst].[sp6CollaborateKeyword_Select_WebServerCollectServerDiff]
AS
BEGIN

	SET NOCOUNT ON;


	SELECT
		[KeywordNo_��],[KeywordNo_��],[�����o���h�L�������g��] 
	FROM
		UnikktleCollect.[hst].[t6CollaborateKeyword] 
	WHERE
		[KeywordNo_��] in (
			SELECT
				A.[KeywordNo_��]
			FROM
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_CollectServer] AS A INNER JOIN 
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_WebServer] AS B
				ON A.[KeywordNo_��] = B.[KeywordNo_��]
			WHERE
				A.[KeywordNo_��_Count] <> B.[KeywordNo_��_Count]
			UNION ALL
			SELECT
				A.[KeywordNo_��]
			FROM
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_CollectServer] AS A LEFT JOIN 
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_WebServer] AS B
				ON A.[KeywordNo_��] = B.[KeywordNo_��]
			WHERE
				B.[KeywordNo_��] is null
		);


END
GO
/*
*/

