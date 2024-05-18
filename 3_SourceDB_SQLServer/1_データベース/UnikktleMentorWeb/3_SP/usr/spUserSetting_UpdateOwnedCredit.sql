USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spUserSetting_UpdateOwnedCredit]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spUserSetting_UpdateOwnedCredit] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spUserSetting_UpdateOwnedCredit]
	@No			bigint,
	@Credit		int
AS
BEGIN

	--declare	@No		bigint = 1;

	UPDATE
		[usr].[tUserSetting]
	SET
		[OwnedCredit] = [OwnedCredit] + @Credit
	WHERE
		[No] = @No 
	;

END
GO
/*
*/