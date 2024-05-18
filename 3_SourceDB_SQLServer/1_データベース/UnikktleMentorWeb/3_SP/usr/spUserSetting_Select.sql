USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spUserSetting_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spUserSetting_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spUserSetting_Select]
	@No		bigint
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[No] AS Id,
		[BackgroundColor],
		[ExternalSearchEngine]
	FROM
		[usr].[tUserSetting]
	WHERE
		[No] = @No
	;


END
GO
/*
*/