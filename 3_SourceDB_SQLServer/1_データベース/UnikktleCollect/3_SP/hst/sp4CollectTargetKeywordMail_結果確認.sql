USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp4CollectTargetKeywordMail_結果確認]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp4CollectTargetKeywordMail_結果確認] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 再計算ストアド
CREATE PROCEDURE [hst].[sp4CollectTargetKeywordMail_結果確認]
	@CollectTargetMailNo	bigint
AS
BEGIN

	SET NOCOUNT ON;

	--declare	@CollectTargetMailNo	bigint = 1;

	SELECT
		A.[CollectTargetMailNo],
		A.[SendDate],
		A.[KeywordNo],
		B.Word AS Keyword
	FROM [hst].[t4CollectTargetKeyword_Mail] AS A 
		INNER JOIN [mst].[tKeyword] AS B ON A.[KeywordNo] = B.[No]
	WHERE
		A.[CollectTargetMailNo] = @CollectTargetMailNo
	ORDER BY
		A.[CollectTargetMailNo],
		A.[SendDate]
	;

END
GO
/*
*/

