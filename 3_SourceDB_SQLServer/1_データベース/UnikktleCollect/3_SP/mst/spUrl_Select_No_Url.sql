USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spUrl_Select_FirstCollect]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spUrl_Select_FirstCollect] ;
GO
IF OBJECT_ID(N'[mst].[spUrl_Select_No_Url]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spUrl_Select_No_Url] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spUrl_Select_No_Url]
	@DomainNo		bigint
AS
BEGIN

	DECLARE @now DATETIME = GETDATE();

	SELECT
	--SELECT TOP 2 -- デバッグ用
		[UrlNo],
		[CollectDate],
		[Url]
	FROM
		[mst].[tUrl]
	WHERE
		[DomainNo] = @DomainNo AND
		([CollectDate] is null OR DATEDIFF(month, [CollectDate], @now) > 1)
	ORDER BY
		[CollectDate],
		[DomainNo],
		[UrlNo]

END
GO

