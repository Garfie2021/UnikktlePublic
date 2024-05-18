USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spUserSetting_UpdateProfile]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spUserSetting_UpdateProfile] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spUserSetting_UpdateProfile]
	@No			bigint,
	@Email		varchar(450),
	@Nickname	nvarchar(100),
	@Gender		tinyint,
	@BirthDate	Date,
	@Career		int
AS	
BEGIN

	--declare	@No		bigint = 1;

	UPDATE
		[usr].[tUserSetting]
	SET
		[Email] = @Email,
		[Nickname] = @Nickname,
		[Gender] = @Gender,
		[BirthDate] = @BirthDate,
		[Career] = @Career
	WHERE
		[No] = @No 
	;

END
GO
/*
*/