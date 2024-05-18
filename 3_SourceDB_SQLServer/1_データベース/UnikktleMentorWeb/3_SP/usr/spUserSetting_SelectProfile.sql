USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spUserSetting_SelectProfile]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spUserSetting_SelectProfile] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spUserSetting_SelectProfile]
	@No		bigint
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
		[No] AS Id,
		[Email],
		[Nickname],
		[Gender],
		[BirthDate],
		[Career],
		[MentorLiteracy]
	FROM
		[usr].[tUserSetting]
	WHERE
		[No] = @No
	;


END
GO
/*
*/