USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_Select]
	@TeamNo		bigint
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		mst.tTeam.[No] AS Id,
		mst.tTeam.TeamID,
		mst.tTeam.TeamExplanation,
		mst.tTeam.AllowPublic,
		mst.tTeam.AllowApplyToJoinTeam,
		mst.tTeam.CreateUserNo
	FROM
		mst.tTeam
	WHERE
		mst.tTeam.[No] = @TeamNo
	;

END
GO
/*
*/