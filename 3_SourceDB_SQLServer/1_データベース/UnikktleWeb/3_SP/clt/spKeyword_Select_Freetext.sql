USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spKeyword_Select_Freetext]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spKeyword_Select_Freetext] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- フルテキスト検索
CREATE PROCEDURE [clt].[spKeyword_Select_Freetext]
	@SearchWord		[nvarchar](100),
	@AfterNum		bigint,
	@AllCnt			bigint OUTPUT
AS
BEGIN

	--declare @SearchWord1	[nvarchar](100) = '%oracle%'
	--declare @afterNum	bigint = 100;

	-- C#側のロジックで、@SearchWord1 と @SearchWord2 はnullになることは無い。


	SELECT Top 30
		--RowNumber,
		Id,
		Word
	FROM (
		SELECT
			ROW_NUMBER() OVER (ORDER BY [No]) AS RowNumber,
			[No] as Id,
			[Word]
		FROM
			[clt].[tKeyword]
		WHERE
			FREETEXT([Word], @SearchWord)
	) AS T
	WHERE
		RowNumber > @AfterNum	-- ページング
	;

	SELECT @AllCnt = COUNT_BIG(*) 
	FROM [clt].[tKeyword]
	WHERE 
		FREETEXT([Word], @SearchWord);


	--select @AllCnt as AllCnt

END
GO
/*
*/