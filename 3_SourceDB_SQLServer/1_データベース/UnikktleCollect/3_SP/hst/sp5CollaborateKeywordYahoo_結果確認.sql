USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp5CollaborateKeywordYahoo_結果確認]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp5CollaborateKeywordYahoo_結果確認]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 再計算ストアド
CREATE PROCEDURE [hst].[sp5CollaborateKeywordYahoo_結果確認]
	@SearchKeywordNo	bigint,
	@KeywordNo			bigint
AS
BEGIN

	SET NOCOUNT ON;

	--declare	@SearchKeywordNo	bigint = 1996;
	--declare	@KeywordNo	bigint = 1562;

	--SELECT  *
	SELECT
		COUNT(*) AS t4CollectTargetKeyword_Yahoo_件数
	FROM
		[hst].[t4CollectTargetKeyword_Yahoo]
	WHERE
		[SearchKeywordNo] = @SearchKeywordNo AND [KeywordNo] = @KeywordNo
	;

	--SELECT  *
	SELECT
		同時出現ドキュメント数
	FROM
		[UnikktleCollect].[hst].[t5CollaborateKeyword_Yahoo]
	where
		[KeywordNo_元] = @SearchKeywordNo AND [KeywordNo_先] = @KeywordNo
	;

END
GO
/*
*/

