USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spKeyword_Select_Contains]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spKeyword_Select_Contains] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- フルテキスト検索
CREATE PROCEDURE [clt].[spKeyword_Select_Contains]
	@SearchWord1	[nvarchar](100),
	@SearchWord2	[nvarchar](100),
	@SearchWord3	[nvarchar](100),
	@SearchWord4	[nvarchar](100),
	@SearchWord5	[nvarchar](100),
	@AfterNum		bigint,
	@AllCnt			bigint OUTPUT
AS
BEGIN

	--declare @SearchWord1	[nvarchar](100) = '%oracle%'
	--declare @SearchWord2	[nvarchar](100) = '%xen%'
	--declare @SearchWord3	[nvarchar](100) = '%vm%'
	--declare @SearchWord4	[nvarchar](100) = ''
	--declare @SearchWord5	[nvarchar](100) = ''
	--declare @afterNum	bigint = 100;

	-- C#側のロジックで、@SearchWord1 と @SearchWord2 はnullになることは無い。

	if @SearchWord2 = ''
	begin

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
				CONTAINS([Word], @SearchWord1)
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
		;

		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [clt].[tKeyword]
		WHERE 
			CONTAINS([Word], @SearchWord1);

	end
	else if @SearchWord3 = ''
	begin
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
				CONTAINS([Word], @SearchWord1) AND 
				CONTAINS([Word], @SearchWord2)
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
		;

		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [clt].[tKeyword]
		WHERE 
			CONTAINS([Word], @SearchWord1) AND 
			CONTAINS([Word], @SearchWord2)
	end
	else if @SearchWord4 = ''
	begin
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
				CONTAINS([Word], @SearchWord1) AND 
				CONTAINS([Word], @SearchWord2) AND 
				CONTAINS([Word], @SearchWord3)
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
		;

		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [clt].[tKeyword]
		WHERE 
			CONTAINS([Word], @SearchWord1) AND 
			CONTAINS([Word], @SearchWord2) AND 
			CONTAINS([Word], @SearchWord3)
	end
	else if @SearchWord5 = ''
	begin
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
				CONTAINS([Word], @SearchWord1) AND 
				CONTAINS([Word], @SearchWord2) AND 
				CONTAINS([Word], @SearchWord3) AND 
				CONTAINS([Word], @SearchWord4)
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
		;

		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [clt].[tKeyword]
		WHERE 
			CONTAINS([Word], @SearchWord1) AND 
			CONTAINS([Word], @SearchWord2) AND 
			CONTAINS([Word], @SearchWord3) AND 
			CONTAINS([Word], @SearchWord4)
	end
	else
	begin
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
				CONTAINS([Word], @SearchWord1) AND 
				CONTAINS([Word], @SearchWord2) AND 
				CONTAINS([Word], @SearchWord3) AND 
				CONTAINS([Word], @SearchWord4) AND 
				CONTAINS([Word], @SearchWord5)
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
		;

		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [clt].[tKeyword]
		WHERE 
			CONTAINS([Word], @SearchWord1) AND 
			CONTAINS([Word], @SearchWord2) AND 
			CONTAINS([Word], @SearchWord3) AND 
			CONTAINS([Word], @SearchWord4) AND 
			CONTAINS([Word], @SearchWord5)
	end;

	--select @AllCnt as AllCnt

END
GO
/*
*/