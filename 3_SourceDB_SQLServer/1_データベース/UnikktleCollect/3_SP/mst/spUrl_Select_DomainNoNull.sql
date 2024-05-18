USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spUrl_Select_DomainNoNull]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spUrl_Select_DomainNoNull] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spUrl_Select_DomainNoNull]
AS
BEGIN

	SELECT
		[UrlNo],
		[Url]
	FROM
		[mst].[tUrl]
	WHERE
		[DomainNo] is null AND 
		[Url] like 'http%'

END
GO

