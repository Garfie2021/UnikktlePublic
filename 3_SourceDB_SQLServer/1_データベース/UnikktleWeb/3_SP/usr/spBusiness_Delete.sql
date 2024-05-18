USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spBusiness_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spBusiness_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spBusiness_Delete]
	@UserNo		bigint,
	@No			smallint
AS
BEGIN

	--declare	@UserNo	bigint = 40;
	--declare	@No		smallint = 3;


	-- Business

	INSERT INTO [UnikktleWeb_Archive].[usr].[tBusiness] (
        [UserNo],
        [No],
        [Category],
        [OrganizationName],
        [OrganizationURL]
	)
	SELECT
        [UserNo],
        [No],
        [Category],
        [OrganizationName],
        [OrganizationURL]
	FROM
		[UnikktleWeb].[usr].[tBusiness]
	WHERE
		[UserNo] = @UserNo AND
		[No] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tBusiness]
	WHERE
		[UserNo] = @UserNo AND
		[No] = @No
	;


	-- AdverRelation

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
		[BusinessNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverRelation]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @No
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
		[BusinessNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverRelationWord]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @No
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
		[BusinessNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverRelationClickHistory]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @No
	;


	-- AdverSearch

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
		[BusinessNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverSearch]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @No
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
		[BusinessNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverSearchWord]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @No
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
		[BusinessNo] = @No
	;

	DELETE FROM [UnikktleWeb].[usr].[tAdverSearchClickHistory]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @No
	;

END
GO
/*
*/