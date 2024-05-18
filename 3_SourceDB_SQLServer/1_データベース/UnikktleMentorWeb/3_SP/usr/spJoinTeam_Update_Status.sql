USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spJoinTeam_Update_Status]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_Update_Status] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spJoinTeam_Update_Status]
	@UserNo		bigint,
	@TeamNo		int,
	@Status		tinyint
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	UPDATE
		[usr].[tJoinTeam]
	SET
		[Status] = @Status
	WHERE
		[UserNo] = @UserNo AND
		[TeamNo] = @TeamNo
	;

END
GO
/*
*/