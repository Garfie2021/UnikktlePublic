USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelation_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelation_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spAdverRelation_Select]
	@UserNo			bigint,
	@BusinessNo		smallint
AS
BEGIN
	--declare	@UserNo			bigint = 40;
	--declare	@BusinessNo		smallint = 1;

	SELECT
		[No] AS Id,
		[Valid],
		[Category],
		[AdverName],
		[AdvertisingBudget],
		DATEADD(DAY, 30, [更新日時]) AS ExpirationDate -- 最後の更新から30日後が表示期限
	FROM 
		[usr].[tAdverRelation]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo
	;

END
GO
/*
*/