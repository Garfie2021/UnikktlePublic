USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spUserSetting_GetOwnedCredit]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spUserSetting_GetOwnedCredit] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spUserSetting_GetOwnedCredit]
	@No				bigint,
	@OwnedCredit	int OUTPUT
AS
BEGIN

	--declare	@No		bigint = 1;


	SELECT
		@OwnedCredit = [OwnedCredit]
	FROM
		[usr].[tUserSetting]
	WHERE
		[No] = @No
	;

END
GO
/*
*/