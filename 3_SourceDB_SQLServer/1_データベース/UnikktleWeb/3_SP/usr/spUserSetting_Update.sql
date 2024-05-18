USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spUserSetting_Update]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spUserSetting_Update] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spUserSetting_Update]
	@No						bigint,
	@BackgroundColor		tinyint,
	@ExternalSearchEngine	tinyint
AS
BEGIN

	--declare	@No		bigint = 1;

	UPDATE
		[usr].[tUserSetting]
	SET
		[BackgroundColor] = @BackgroundColor,
		[ExternalSearchEngine] = @ExternalSearchEngine
	WHERE
		[No] = @No 
	;



END
GO
/*
*/