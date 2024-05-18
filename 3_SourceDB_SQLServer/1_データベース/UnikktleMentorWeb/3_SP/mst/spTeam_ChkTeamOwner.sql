USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_ChkTeamOwner]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_ChkTeamOwner] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_ChkTeamOwner]
	@UserNo		bigint,
	@TeamNo		int,
	@Cnt		int OUTPUT
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		@Cnt = COUNT(*)
	FROM
		mst.tTeam
	WHERE
		mst.tTeam.[No] = @TeamNo AND
		mst.tTeam.CreateUserNo = @UserNo
	;

END
GO
/*
*/