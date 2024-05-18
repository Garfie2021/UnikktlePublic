USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearch_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearch_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverSearch_Delete]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@No				smallint
AS
BEGIN

	--DECLARE @UserNo			bigint;
	--DECLARE @BusinessNo		smallint;
	--DECLARE @No				smallint;

	INSERT INTO [UnikktleWeb_Archive].[usr].[tAdverSearch] (
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
		[UnikktleWeb].[usr].[tAdverSearch]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[No] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverSearch]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[No] = @No
	;


	INSERT INTO [UnikktleWeb_Archive].[usr].[tAdverSearchWord] (
        [UserNo],
        [BusinessNo],
        [AdverNo],
        [SearchWordNo],
        [ClickCost]
	)
	SELECT
        [UserNo],
        [BusinessNo],
        [AdverNo],
        [SearchWordNo],
        [ClickCost]
	FROM
		[UnikktleWeb].[usr].[tAdverSearchWord]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverSearchWord]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @No
	;


	INSERT INTO [UnikktleWeb_Archive].[usr].[tAdverSearchClickHistory] (
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
		[UnikktleWeb].[usr].[tAdverSearchClickHistory]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverSearchClickHistory]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @No
	;

END
GO
/*
*/