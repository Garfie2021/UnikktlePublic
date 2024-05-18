USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spJoinTeam_Insert_TeamID]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_Insert_TeamID] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spJoinTeam_Insert_TeamID]
	@UserNo		bigint,
	@TeamID		nvarchar(100)
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	INSERT INTO [usr].[tJoinTeam] (
        [UserNo],
        [TeamNo]
    ) VALUES (
        @UserNo,
        (SELECT TOP 1 [No] FROM mst.tTeam WHERE TeamID = @TeamID)
	);

END
GO
/*
*/