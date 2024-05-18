USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp5CollaborateKeyword_結果確認]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp5CollaborateKeyword_結果確認] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 再計算ストアド
CREATE PROCEDURE [hst].[sp5CollaborateKeyword_結果確認]
	@KeywordNo	bigint
AS
BEGIN

	SET NOCOUNT ON;

	--declare	@KeywordNo	bigint = 1;

	SELECT
		't4CollectTargetKeyword_Bing' as TableName,
		[SearchKeywordNo],
		[SearchDate],
		[SearchResultNo],
		[KeywordNo]
	FROM
		[UnikktleCollect].[hst].[t4CollectTargetKeyword_Bing]
	WHERE
		[KeywordNo] = @KeywordNo
	;


END
GO
/*
*/

