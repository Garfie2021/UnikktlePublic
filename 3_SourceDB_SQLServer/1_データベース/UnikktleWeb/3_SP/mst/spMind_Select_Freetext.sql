USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_Select_Freetext]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_Select_Freetext] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- �t���e�L�X�g����
CREATE PROCEDURE [mst].[spMind_Select_Freetext]
	@SearchWord		[nvarchar](100),
	@AfterNum		bigint,
	@CreateUserNo	bigint,
	@AllCnt			bigint OUTPUT
AS
BEGIN

	/*
	--declare @SearchWord1	[nvarchar](100) = '%oracle%'

	-- C#���̃��W�b�N�ŁA@SearchWord1 �� @SearchWord2 ��null�ɂȂ邱�Ƃ͖����B
	*/

	SELECT Top 30
		RowNumber,
		Id,
		[Title],
		[Explanation],
		[LastUpdate]
	FROM (
		SELECT
			ROW_NUMBER() OVER (ORDER BY [No]) AS RowNumber,
			[No] as Id,
			[Title],
			[Explanation],
			[LastUpdate]
		FROM
			[mst].[tMind]
		WHERE
			(FREETEXT([Title], @SearchWord) or FREETEXT([Item_SpaceSeparator], @SearchWord) or FREETEXT([Explanation], @SearchWord)) AND
			(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo))
	) AS T
	WHERE
		RowNumber > @AfterNum	-- �y�[�W���O

	SELECT
		@AllCnt = COUNT_BIG(*)
	FROM
		[mst].[tMind]
	WHERE
		(FREETEXT([Title], @SearchWord) or FREETEXT([Item_SpaceSeparator], @SearchWord) or FREETEXT([Explanation], @SearchWord)) AND
		(PublishOnlyToMe = 0 OR (PublishOnlyToMe = 1 AND UserNo = @CreateUserNo)) ;

	--SELECT @AllCnt AS AllCnt

END
GO
/*
*/