USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spJoinTeam_DeleteMember]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_DeleteMember] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spJoinTeam_DeleteMember]
	@TeamNo		int,
	@UserNo		bigint
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	DELETE
		usr.tJoinTeam
	WHERE
		TeamNo = @TeamNo AND
		UserNo = @UserNo
	;

END
GO
/*
*/