USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp2HtmlParseWebPage_Select_State0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp2HtmlParseWebPage_Select_State0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp2HtmlParseWebPage_Select_State0]
AS
BEGIN

	SELECT
		[DomainNo],
		[UrlNo],
		[���ꔻ��],
		[HtmlTag���O��2�i�K��]
	FROM
		[hst].[t2HtmlParseWebPage]
	WHERE
		[State] IS NULL AND
		[���ꔻ��] = 1
	;

END
GO

