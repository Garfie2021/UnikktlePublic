USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearchWord_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearchWord_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverSearchWord_Select]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@AdverNo		int
AS
BEGIN

	--declare	@No		bigint = 1;
	--declare	@UserNo			bigint = 0;
	--declare	@BusinessNo		smallint = 0;
	--declare	@AdverNo		int = 0;

	SELECT
		CAST(ROW_NUMBER() OVER(ORDER BY A.[Word]) as int) as Id,
		B.[SearchWordNo] as WordId,
		A.[Word],
		B.[ClickCost],
		convert(tinyint, 0) as DelFlg		-- 0:çÌèúÇµÇ»Ç¢Å@1:çÌèúÇ∑ÇÈ
	FROM
		mst.tSearchWord AS A
		INNER JOIN
		(
			SELECT
				[SearchWordNo],
				[ClickCost]
			FROM
				usr.tAdverSearchWord
			WHERE
				[UserNo] = @UserNo AND
				[BusinessNo] = @BusinessNo AND
				[AdverNo] = @AdverNo
		) AS B ON 
		A.No = B.SearchWordNo
	ORDER BY
		A.[Word] ;

END
GO
/*
*/