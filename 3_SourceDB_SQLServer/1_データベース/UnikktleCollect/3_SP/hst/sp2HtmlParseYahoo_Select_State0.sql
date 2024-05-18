USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp2HtmlParseYahoo_Select_State0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp2HtmlParseYahoo_Select_State0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp2HtmlParseYahoo_Select_State0]
AS
BEGIN

	-- 未解析のメールを取得
	SELECT
		[SearchKeywordNo],
		[SearchDate],
		[HtmlTag除外後2段階目]
	FROM [hst].[t2HtmlParseYahoo]
	WHERE [State] = 0; 

END
GO

