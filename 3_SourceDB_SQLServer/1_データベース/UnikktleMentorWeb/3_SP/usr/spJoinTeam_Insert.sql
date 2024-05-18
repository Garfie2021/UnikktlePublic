USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spJoinTeam_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spJoinTeam_Insert]
	@UserNo		bigint,
	@TeamNo		int,
	@Status		tinyint
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	INSERT INTO [usr].[tJoinTeam] (
        [UserNo],
        [TeamNo],
		[Status]
    ) VALUES (
        @UserNo,
        @TeamNo,
		@Status
	);

END
GO
/*
*/