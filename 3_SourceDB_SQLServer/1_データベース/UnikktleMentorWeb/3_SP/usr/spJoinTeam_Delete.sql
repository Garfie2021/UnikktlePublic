USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spJoinTeam_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spJoinTeam_Delete]
	@TeamNo		int
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	DELETE
		usr.tJoinTeam
	WHERE
		TeamNo = @TeamNo
	;

END
GO
/*
*/