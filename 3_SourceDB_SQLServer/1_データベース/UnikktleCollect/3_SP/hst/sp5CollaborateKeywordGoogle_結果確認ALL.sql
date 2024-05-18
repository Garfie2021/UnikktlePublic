USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp5CollaborateKeywordGoogle_Select_結果確認]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp5CollaborateKeywordGoogle_Select_結果確認] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 再計算ストアド
CREATE PROCEDURE [hst].[sp5CollaborateKeywordGoogle_Select_結果確認]
	@同時出現ドキュメント数	bigint
AS
BEGIN

	SET NOCOUNT ON;

  	SELECT
		A.[KeywordNo_元] AS 元No,
		B.Word AS 元Word,
		A.[KeywordNo_先] AS 先No,
		C.Word AS 先Word,
		A.同時出現ドキュメント数 AS 同時出現ドキュメント数
	FROM [hst].[t5CollaborateKeyword_Google] AS A 
		INNER JOIN [mst].[tKeyword] AS B ON A.[KeywordNo_元] = B.[No]
		INNER JOIN [mst].[tKeyword] AS C ON A.[KeywordNo_先] = C.[No]
	WHERE
		同時出現ドキュメント数 > @同時出現ドキュメント数
	ORDER BY
		元No,
		同時出現ドキュメント数 DESC
	;


END
GO
/*
*/

