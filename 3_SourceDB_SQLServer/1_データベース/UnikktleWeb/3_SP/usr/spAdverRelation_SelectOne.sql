USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelation_SelectOne]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelation_SelectOne] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spAdverRelation_SelectOne]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@No				int
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[No] AS Id,
		[Valid],
		[Category],
		[AdverName],
		[AdverTitle1],
		[AdverTitle2],
		[AdverURL],
		[AdvertisingBudget]
	FROM 
		[usr].[tAdverRelation]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[No] = @No
	;

END
GO
/*
*/