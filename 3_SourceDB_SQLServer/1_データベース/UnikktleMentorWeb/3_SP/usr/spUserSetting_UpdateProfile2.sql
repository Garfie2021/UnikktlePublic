USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spUserSetting_UpdateProfile2]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spUserSetting_UpdateProfile2] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spUserSetting_UpdateProfile2]
	@No			bigint,
	@Gender		tinyint,
	@Career		int
AS	
BEGIN

	--declare	@No		bigint = 1;

	UPDATE
		[usr].[tUserSetting]
	SET
		[Gender] = @Gender,
		[Career] = @Career
	WHERE
		[No] = @No 
	;

END
GO
/*
*/