USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_Select_TeamID]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_Select_TeamID] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_Select_TeamID]
	@TeamID		nvarchar(100)
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
		mst.tTeam.[TeamID] = @TeamID
	;

END
GO
/*
*/