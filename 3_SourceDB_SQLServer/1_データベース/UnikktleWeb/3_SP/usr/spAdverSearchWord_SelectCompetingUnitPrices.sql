USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearchWord_SelectCompetingUnitPrices]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearchWord_SelectCompetingUnitPrices] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ã£çáÇ∑ÇÈíPâø(CompetingUnitPrices)
CREATE PROCEDURE [usr].[spAdverSearchWord_SelectCompetingUnitPrices]
	@Word	nvarchar(100)
AS
BEGIN

	--declare	@Word	nvarchar(100) = 'aa'

	SELECT TOP 10
		CAST(ROW_NUMBER() OVER(ORDER BY t.[ClickCost] DESC) as tinyint) as Id,
		t.[ClickCost] as Price
	FROM (
		SELECT
			DISTINCT B.[ClickCost]
		FROM
			[mst].[tSearchWord] as A INNER JOIN usr.tAdverSearchWord as B 
				ON A.No = B.SearchWordNo
		WHERE
			A.Word = @Word 
		) AS t
	ORDER BY
		t.[ClickCost] DESC ;


END
GO
/*
*/