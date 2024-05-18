USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spJoinTeam_SelectCount_Joined]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_SelectCount_Joined] ;
GO
IF OBJECT_ID(N'[usr].[spJoinTeam_SelectStatus]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_SelectStatus] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spJoinTeam_SelectStatus]
	@UserNo		bigint,
	@TeamNo		int,
	@Status		tinyint OUTPUT
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		@Status = usr.tJoinTeam.[Status]
	FROM
		usr.tJoinTeam
	WHERE
		usr.tJoinTeam.[UserNo] = @UserNo AND
		usr.tJoinTeam.[TeamNo] = @TeamNo
	;

END
GO
/*
*/