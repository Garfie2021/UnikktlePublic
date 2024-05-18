USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelationWord_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelationWord_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverRelationWord_Select]
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
		B.[RelationWordNo] as WordId,
		A.[Word],
		B.[ClickCost],
		convert(tinyint, 0) as DelFlg		-- 0:çÌèúÇµÇ»Ç¢Å@1:çÌèúÇ∑ÇÈ
	FROM
		clt.tKeyword AS A
		INNER JOIN
		(
			SELECT
				[RelationWordNo],
				[ClickCost]
			FROM
				usr.tAdverRelationWord
			WHERE
				[UserNo] = @UserNo AND
				[BusinessNo] = @BusinessNo AND
				[AdverNo] = @AdverNo
		) AS B ON 
		A.No = B.RelationWordNo
	ORDER BY
		A.[Word] ;

END
GO
/*
*/