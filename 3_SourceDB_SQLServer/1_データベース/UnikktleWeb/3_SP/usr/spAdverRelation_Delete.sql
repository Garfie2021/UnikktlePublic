USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelation_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelation_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverRelation_Delete]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@No				smallint
AS
BEGIN

	--DECLARE @UserNo			bigint;
	--DECLARE @BusinessNo		smallint;
	--DECLARE @No				smallint;

	INSERT INTO [UnikktleWeb_Archive].[usr].[tAdverRelation] (
		[UserNo],
        [BusinessNo],
        [No],
        [Valid],
        [Category],
        [AdverName],
        [AdverURL],
        [AdvertisingBudget],
        [更新日時],
        [ClickCnt],
        [TotalClickCost]
    )
	SELECT
		[UserNo],
        [BusinessNo],
        [No],
        [Valid],
        [Category],
        [AdverName],
        [AdverURL],
        [AdvertisingBudget],
        [更新日時],
        [ClickCnt],
        [TotalClickCost]
	FROM
		[UnikktleWeb].[usr].[tAdverRelation]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[No] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverRelation]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[No] = @No
	;


	INSERT INTO [UnikktleWeb_Archive].[usr].[tAdverRelationWord] (
        [UserNo],
        [BusinessNo],
        [AdverNo],
        [RelationWordNo],
        [ClickCost]
	)
	SELECT
        [UserNo],
        [BusinessNo],
        [AdverNo],
        [RelationWordNo],
        [ClickCost]
	FROM
		[UnikktleWeb].[usr].[tAdverRelationWord]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverRelationWord]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @No
	;


	INSERT INTO [UnikktleWeb_Archive].[usr].[tAdverRelationClickHistory] (
		[UserNo],
        [BusinessNo],
        [AdverNo],
        [ClickUserNo],
        [WordNo],
        [ClickDate],
        [ClickCost]
	)
	SELECT
		[UserNo],
        [BusinessNo],
        [AdverNo],
        [ClickUserNo],
        [WordNo],
        [ClickDate],
        [ClickCost]
	FROM
		[UnikktleWeb].[usr].[tAdverRelationClickHistory]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverRelationClickHistory]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @No
	;


END
GO
/*
*/