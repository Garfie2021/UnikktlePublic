USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_Update]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_Update] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_Update]
	@TeamNo					int,
	@TeamID					nvarchar(100),
	@TeamExplanation		nvarchar(200),
	@AllowPublic			bit,
	@AllowApplyToJoinTeam	bit,
	@UpdateUserNo			bigint
AS
BEGIN

	UPDATE [mst].[tTeam]
	SET [TeamID] = @TeamID,
		[TeamExplanation] = @TeamExplanation,
		[AllowPublic] = @AllowPublic,
		[AllowApplyToJoinTeam] = @AllowApplyToJoinTeam,
		[UpdateUserNo] = @UpdateUserNo
	WHERE
		[No] = @TeamNo
	;

END
GO
/*
*/