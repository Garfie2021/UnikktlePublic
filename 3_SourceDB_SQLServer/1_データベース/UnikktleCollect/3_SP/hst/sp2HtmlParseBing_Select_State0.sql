USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp2HtmlParseBing_Select_State0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp2HtmlParseBing_Select_State0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp2HtmlParseBing_Select_State0]
AS
BEGIN

	SELECT
		[SearchKeywordNo],
		[SearchDate],
		[HtmlTagèúäOå„2íiäKñ⁄]
	FROM [hst].[t2HtmlParseBing]
	WHERE [State] = 0; 

END
GO

