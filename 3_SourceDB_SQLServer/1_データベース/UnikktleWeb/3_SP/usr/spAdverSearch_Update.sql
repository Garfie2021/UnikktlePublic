USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearch_Update]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearch_Update] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverSearch_Update]
	@UserNo				bigint,
	@BusinessNo			smallint,
	@No					int,
	@Valid				tinyint,
	@Category			tinyint,
	@AdverName			nvarchar(50),
	@AdverTitle1		nvarchar(100),
	@AdverTitle2		nvarchar(100),
	@AdverURL			varchar(200),
	@AdvertisingBudget	int
AS
BEGIN

	--declare	@No		bigint = 1;

	UPDATE
		[usr].[tAdverSearch]
	SET
		[Valid] = @Valid,
		[Category] = @Category,
		[AdverName] = @AdverName,
		[AdverTitle1] = @AdverTitle1,
		[AdverTitle2] = @AdverTitle2,
		[AdverURL] = @AdverURL,
		[AdvertisingBudget] = @AdvertisingBudget,
		[çXêVì˙éû] = GETDATE()
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[No] = @No
	;

END
GO
/*
*/