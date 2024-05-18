USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractWebPage_GetCount]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractWebPage_GetCount] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractWebPage_GetCount]
	@DomainNo	bigint,
	@UrlNo		bigint,
	@Cnt		bigint	OUTPUT
AS
BEGIN

	SELECT
		@Cnt = COUNT_BIG(*)
	FROM
		[hst].[t3ExtractWebPage]
	WHERE
		[DomainNo] = @DomainNo AND
		[UrlNo] = @UrlNo ;

END
GO

