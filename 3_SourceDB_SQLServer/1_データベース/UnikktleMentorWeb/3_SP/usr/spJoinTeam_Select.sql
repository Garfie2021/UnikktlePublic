USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spJoinTeam_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spJoinTeam_Select]
	@UserNo		bigint
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		mst.tTeam.[No] AS Id,
		mst.tTeam.TeamId,
		usr.tJoinTeam.[Status]
	FROM
		mst.tTeam INNER JOIN usr.tJoinTeam
			ON mst.tTeam.[No] = usr.tJoinTeam.TeamNo
	WHERE
		usr.tJoinTeam.UserNo = @UserNo
	;

END
GO
/*
*/