USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_Select_Contains]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_Select_Contains] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- フルテキスト検索
CREATE PROCEDURE [mst].[spMind_Select_Contains]
	@SearchWord1	[nvarchar](100),
	@SearchWord2	[nvarchar](100),
	@SearchWord3	[nvarchar](100),
	@SearchWord4	[nvarchar](100),
	@SearchWord5	[nvarchar](100),
	@AfterNum		bigint,
	@CreateUserNo	bigint,
	@AllCnt			bigint OUTPUT
AS
BEGIN



	-- 下記は全文検索対応

	/*
	--declare @SearchWord1	[nvarchar](100) = '%oracle%'
	--declare @SearchWord2	[nvarchar](100) = '%xen%'
	--declare @SearchWord3	[nvarchar](100) = '%vm%'
	--declare @SearchWord4	[nvarchar](100) = ''
	--declare @SearchWord5	[nvarchar](100) = ''
	--declare @afterNum	bigint = 100;
	--declare @CreateUserNo	bigint = 100;

	-- C#側のロジックで、@SearchWord1 と @SearchWord2 はnullになることは無い。
	*/

	if @SearchWord2 = ''
	begin

		SELECT Top 30
			RowNumber,
			Id,
			PublishOnlyToMe,
			AllowOtherEdit,
			[Title],
			[Explanation],
			[LastUpdate]
		FROM (
			SELECT
				ROW_NUMBER() OVER (ORDER BY [No]) AS RowNumber,
				[No] as Id,
				PublishOnlyToMe,
				AllowOtherEdit,
				[Title],
				[Explanation],
				[LastUpdate]
			FROM
				[mst].[tMind]
			WHERE
				(CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND
				(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo))
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
			
		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [mst].[tMind]
		WHERE 
			(CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND
			(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo)) ;

	end
	else if @SearchWord3 = ''
	begin

		SELECT Top 30
			RowNumber,
			Id,
			PublishOnlyToMe,
			AllowOtherEdit,
			[Title],
			[Explanation],
			[LastUpdate]
		FROM (
			SELECT
				ROW_NUMBER() OVER (ORDER BY [No]) AS RowNumber,
				[No] as Id,
				PublishOnlyToMe,
				AllowOtherEdit,
				[Title],
				[Explanation],
				[LastUpdate]
			FROM
				[mst].[tMind]
			WHERE
				((CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND 
				 (CONTAINS([Title], @SearchWord2) or CONTAINS([Item_SpaceSeparator], @SearchWord2) or CONTAINS([Explanation], @SearchWord2))) AND 
				(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo))
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
		;

		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [mst].[tMind]
		WHERE 
			((CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND 
			 (CONTAINS([Title], @SearchWord2) or CONTAINS([Item_SpaceSeparator], @SearchWord2) or CONTAINS([Explanation], @SearchWord2))) AND 
			(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo)) ;
	end
	else if @SearchWord4 = ''
	begin

		SELECT Top 30
			RowNumber,
			Id,
			PublishOnlyToMe,
			AllowOtherEdit,
			[Title],
			[Explanation],
			[LastUpdate]
		FROM (
			SELECT
				ROW_NUMBER() OVER (ORDER BY [No]) AS RowNumber,
				[No] as Id,
				PublishOnlyToMe,
				AllowOtherEdit,
				[Title],
				[Explanation],
				[LastUpdate]
			FROM
				[mst].[tMind]
			WHERE
				((CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND 
				 (CONTAINS([Title], @SearchWord2) or CONTAINS([Item_SpaceSeparator], @SearchWord2) or CONTAINS([Explanation], @SearchWord2)) AND 
				 (CONTAINS([Title], @SearchWord3) or CONTAINS([Item_SpaceSeparator], @SearchWord3) or CONTAINS([Explanation], @SearchWord3))) AND
				(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo))
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
		;

		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [mst].[tMind]
		WHERE 
			((CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND 
			 (CONTAINS([Title], @SearchWord2) or CONTAINS([Item_SpaceSeparator], @SearchWord2) or CONTAINS([Explanation], @SearchWord2)) AND 
			 (CONTAINS([Title], @SearchWord3) or CONTAINS([Item_SpaceSeparator], @SearchWord3) or CONTAINS([Explanation], @SearchWord3))) AND
			(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo)) ;
	end
	else if @SearchWord5 = ''
	begin
		SELECT Top 30
			RowNumber,
			Id,
			PublishOnlyToMe,
			AllowOtherEdit,
			[Title],
			[Explanation],
			[LastUpdate]
		FROM (
			SELECT
				ROW_NUMBER() OVER (ORDER BY [No]) AS RowNumber,
				[No] as Id,
				PublishOnlyToMe,
				AllowOtherEdit,
				[Title],
				[Explanation],
				[LastUpdate]
			FROM
				[mst].[tMind]
			WHERE
				((CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND 
				 (CONTAINS([Title], @SearchWord2) or CONTAINS([Item_SpaceSeparator], @SearchWord2) or CONTAINS([Explanation], @SearchWord2)) AND 
				 (CONTAINS([Title], @SearchWord3) or CONTAINS([Item_SpaceSeparator], @SearchWord3) or CONTAINS([Explanation], @SearchWord3)) AND 
				 (CONTAINS([Title], @SearchWord4) or CONTAINS([Item_SpaceSeparator], @SearchWord4) or CONTAINS([Explanation], @SearchWord4))) AND 
				(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo))
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
		;

		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [mst].[tMind]
		WHERE 
			((CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND 
			 (CONTAINS([Title], @SearchWord2) or CONTAINS([Item_SpaceSeparator], @SearchWord2) or CONTAINS([Explanation], @SearchWord2)) AND 
			 (CONTAINS([Title], @SearchWord3) or CONTAINS([Item_SpaceSeparator], @SearchWord3) or CONTAINS([Explanation], @SearchWord3)) AND 
			 (CONTAINS([Title], @SearchWord4) or CONTAINS([Item_SpaceSeparator], @SearchWord4) or CONTAINS([Explanation], @SearchWord4))) AND 
			(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo)) ;
	end
	else
	begin
		SELECT Top 30
			RowNumber,
			Id,
			PublishOnlyToMe,
			AllowOtherEdit,
			[Title],
			[Explanation],
			[LastUpdate]
		FROM (
			SELECT
				ROW_NUMBER() OVER (ORDER BY [No]) AS RowNumber,
				[No] as Id,
				PublishOnlyToMe,
				AllowOtherEdit,
				[Title],
				[Explanation],
				[LastUpdate]
			FROM
				[mst].[tMind]
			WHERE
				((CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND 
				 (CONTAINS([Title], @SearchWord2) or CONTAINS([Item_SpaceSeparator], @SearchWord2) or CONTAINS([Explanation], @SearchWord2)) AND 
				 (CONTAINS([Title], @SearchWord3) or CONTAINS([Item_SpaceSeparator], @SearchWord3) or CONTAINS([Explanation], @SearchWord3)) AND 
				 (CONTAINS([Title], @SearchWord4) or CONTAINS([Item_SpaceSeparator], @SearchWord4) or CONTAINS([Explanation], @SearchWord4)) AND 
				 (CONTAINS([Title], @SearchWord5) or CONTAINS([Item_SpaceSeparator], @SearchWord5) or CONTAINS([Explanation], @SearchWord5))) AND 
				(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo))
		) AS T
		WHERE
			RowNumber > @AfterNum	-- ページング
		;

		SELECT @AllCnt = COUNT_BIG(*) 
		FROM [mst].[tMind]
		WHERE 
			((CONTAINS([Title], @SearchWord1) or CONTAINS([Item_SpaceSeparator], @SearchWord1) or CONTAINS([Explanation], @SearchWord1)) AND 
			 (CONTAINS([Title], @SearchWord2) or CONTAINS([Item_SpaceSeparator], @SearchWord2) or CONTAINS([Explanation], @SearchWord2)) AND 
			 (CONTAINS([Title], @SearchWord3) or CONTAINS([Item_SpaceSeparator], @SearchWord3) or CONTAINS([Explanation], @SearchWord3)) AND 
			 (CONTAINS([Title], @SearchWord4) or CONTAINS([Item_SpaceSeparator], @SearchWord4) or CONTAINS([Explanation], @SearchWord4)) AND 
			 (CONTAINS([Title], @SearchWord5) or CONTAINS([Item_SpaceSeparator], @SearchWord5) or CONTAINS([Explanation], @SearchWord5))) AND 
			(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo)) ;
	end;

	--select @AllCnt as AllCnt

END
GO
/*
*/