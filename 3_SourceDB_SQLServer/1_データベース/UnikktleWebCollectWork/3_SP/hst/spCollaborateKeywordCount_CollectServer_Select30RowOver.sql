USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spCollaborateKeywordCount_CollectServer_Select30RowOver]
AS
BEGIN

	SET NOCOUNT ON;


	-- [KeywordNo_元]列が30件以上重複している行を抽出。※[KeywordNo_元]に関連するキーワ―ド（[KeywordNo_先]）が30件以上の、[KeywordNo_元]を抽出しているのと同じ意味。

	SELECT
		[KeywordNo_元] AS [No]
	FROM
		[hst].[tCollaborateKeywordCount_CollectServer]
	WHERE
		[KeywordNo_先_Count] > 30
	ORDER BY
		[KeywordNo_元]
	;

END
GO
/*
*/

