USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp4CollectTargetKeywordBing_結果確認]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp4CollectTargetKeywordBing_結果確認] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 再計算ストアド
CREATE PROCEDURE [hst].[sp4CollectTargetKeywordBing_結果確認]
	@SearchKeywordNo	bigint
AS
BEGIN

	SET NOCOUNT ON;

	--declare	@SearchKeywordNo	bigint = 1;

	SELECT
		A.[SearchKeywordNo],
		B.Word AS SearchWord,
		A.[SearchDate],
		A.[SearchResultNo],
		A.[KeywordNo],
		C.Word AS Keyword
	FROM [hst].[t4CollectTargetKeyword_Bing] AS A 
		INNER JOIN [mst].[tKeyword] AS B ON A.[SearchKeywordNo] = B.[No]
		INNER JOIN [mst].[tKeyword] AS C ON A.[KeywordNo] = C.[No]
	WHERE
		A.[SearchKeywordNo] = @SearchKeywordNo
	ORDER BY
		A.[SearchKeywordNo],
		A.[SearchDate],
		A.[SearchResultNo]
	;

END
GO
/*
*/

