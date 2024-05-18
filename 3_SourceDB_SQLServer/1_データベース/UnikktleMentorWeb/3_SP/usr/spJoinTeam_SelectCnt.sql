USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spJoinTeam_SelectCnt]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_SelectCnt] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spJoinTeam_SelectCnt]
	@UserNo		bigint,
	@TeamNo		int,
	@Cnt		int OUTPUT
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		@Cnt = Count(*)
	FROM
		usr.tJoinTeam
	WHERE
		UserNo = @UserNo AND
		TeamNo = @TeamNo
	;

END
GO
/*
*/