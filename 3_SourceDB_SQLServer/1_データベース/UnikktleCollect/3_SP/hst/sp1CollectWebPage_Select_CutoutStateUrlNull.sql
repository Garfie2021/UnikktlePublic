USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Select_UrlState0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Select_UrlState0] ;
GO
IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Select_CutoutStateUrl0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Select_CutoutStateUrl0] ;
GO
IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Select_CutoutStateUrlNull]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Select_CutoutStateUrlNull] ;
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectWebPage_Select_CutoutStateUrlNull]
AS
BEGIN

	-- 収集したHtmlからURL切出しが終わっていないHtml。
	SELECT TOP 100	-- SqlDataReaderで1件づつ処理する作りだと、SQLServerから応答が返って来ない現象に遭遇したので、100件づつDataTableに入れて、0件になるまで処理を続行する。
		[DomainNo],
		[UrlNo],
		[Html]
	FROM
		[hst].[t1CollectWebPage]
	WHERE
		[CollectState] = 1 AND
		[CutoutStateUrl] is null
	;

END
GO
