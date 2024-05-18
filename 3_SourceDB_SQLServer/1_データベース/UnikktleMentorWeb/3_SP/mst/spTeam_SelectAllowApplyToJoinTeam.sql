USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_SelectAllowApplyToJoinTeam]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_SelectAllowApplyToJoinTeam] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_SelectAllowApplyToJoinTeam]
	@TeamID					nvarchar(100),
	@AllowApplyToJoinTeam	bit OUTPUT
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		@AllowApplyToJoinTeam = mst.tTeam.AllowApplyToJoinTeam
	FROM
		mst.tTeam
	WHERE
		mst.tTeam.[TeamID] = @TeamID
	;

END
GO
/*
*/

