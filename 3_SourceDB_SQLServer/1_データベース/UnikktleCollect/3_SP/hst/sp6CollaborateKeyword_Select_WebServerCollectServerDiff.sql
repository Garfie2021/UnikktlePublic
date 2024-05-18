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

-- 再計算ストアド
CREATE PROCEDURE [hst].[sp6CollaborateKeyword_Select_WebServerCollectServerDiff]
AS
BEGIN

	SET NOCOUNT ON;


	SELECT
		[KeywordNo_元],[KeywordNo_先],[同時出現ドキュメント数] 
	FROM
		UnikktleCollect.[hst].[t6CollaborateKeyword] 
	WHERE
		[KeywordNo_元] in (
			SELECT
				A.[KeywordNo_元]
			FROM
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_CollectServer] AS A INNER JOIN 
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_WebServer] AS B
				ON A.[KeywordNo_元] = B.[KeywordNo_元]
			WHERE
				A.[KeywordNo_先_Count] <> B.[KeywordNo_先_Count]
			UNION ALL
			SELECT
				A.[KeywordNo_元]
			FROM
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_CollectServer] AS A LEFT JOIN 
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_WebServer] AS B
				ON A.[KeywordNo_元] = B.[KeywordNo_元]
			WHERE
				B.[KeywordNo_元] is null
		);


END
GO
/*
*/

