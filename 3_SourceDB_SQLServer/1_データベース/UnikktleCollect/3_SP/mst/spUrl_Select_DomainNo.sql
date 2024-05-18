USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spUrl_Select_FirstCollect_DomainNo]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spUrl_Select_FirstCollect_DomainNo] ;
GO
IF OBJECT_ID(N'[mst].[spUrl_Select_DomainNo]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spUrl_Select_DomainNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spUrl_Select_DomainNo]
AS
BEGIN

	DECLARE @now DATETIME = GETDATE();

	SELECT
	--SELECT TOP 2 -- デバッグ用
		T.[DomainNo]
	FROM
	(
		SELECT
			[DomainNo],
			MIN([CollectDate]) AS CollectDate
		FROM
			[mst].[tUrl]
		WHERE
			[CollectDate] is null OR
			DATEDIFF(month, [CollectDate], @now) > 1
		GROUP BY
			[DomainNo],
			[CollectDate]
	) AS T
	ORDER BY
		[CollectDate],
		[DomainNo]
	;

END
GO

