USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver_Word]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver_Word] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver_Word]
AS
BEGIN

	SET NOCOUNT ON;


	-- [KeywordNo_元]列が30件以上重複している行を抽出。※[KeywordNo_元]に関連するキーワ―ド（[KeywordNo_先]）が30件以上の、[KeywordNo_元]を抽出しているのと同じ意味。

	SELECT 
		[Word]
	FROM
		[UnikktleCollect].[mst].[tKeyword]
	WHERE
		[No] IN (  
  			SELECT
				[KeywordNo_元] AS [No]
			FROM
				[UnikktleWebCollectWork].[hst].[tCollaborateKeywordCount_CollectServer]
			WHERE
				[KeywordNo_先_Count] > 30
		)
	ORDER BY
		[No]
	;

END
GO
/*
*/

