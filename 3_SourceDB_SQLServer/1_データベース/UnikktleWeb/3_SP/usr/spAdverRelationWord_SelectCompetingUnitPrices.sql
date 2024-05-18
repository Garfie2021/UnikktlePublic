USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelationWord_SelectCompetingUnitPrices]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelationWord_SelectCompetingUnitPrices] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ã£çáÇ∑ÇÈíPâø(CompetingUnitPrices)
CREATE PROCEDURE [usr].[spAdverRelationWord_SelectCompetingUnitPrices]
	@WordNo		bigint
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
			usr.tAdverRelationWord as B 
		WHERE
			B.RelationWordNo = @WordNo
		) AS t
	ORDER BY
		t.[ClickCost] DESC ;

END
GO
/*
*/