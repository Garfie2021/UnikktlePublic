USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp2HtmlParseWebPage_GetCount]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp2HtmlParseWebPage_GetCount] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp2HtmlParseWebPage_GetCount]
	@DomainNo	bigint,
	@UrlNo		bigint,
	@Cnt		bigint	OUTPUT
AS
BEGIN

	SELECT
		@Cnt = COUNT(*)
	FROM
		[hst].[t2HtmlParseWebPage]
	WHERE
		[DomainNo] = @DomainNo AND
		[UrlNo] = @UrlNo ;

END
GO

