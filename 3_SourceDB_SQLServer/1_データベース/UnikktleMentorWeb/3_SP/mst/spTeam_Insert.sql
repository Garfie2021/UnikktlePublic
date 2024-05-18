USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_Insert]
	@TeamID					nvarchar(100),
	@TeamExplanation		nvarchar(200),
	@AllowPublic			bit,
	@AllowApplyToJoinTeam	bit,
	@CreateUserNo			bigint,
	@TeamNo					int OUTPUT
AS
BEGIN

	INSERT INTO [mst].[tTeam] (
		[TeamID],
		[TeamExplanation],
		[AllowPublic],
		[AllowApplyToJoinTeam],
		[CreateUserNo],
		[UpdateUserNo]
    ) VALUES (
        @TeamID,
        @TeamExplanation,
		@AllowPublic,
		@AllowApplyToJoinTeam,
        @CreateUserNo,
        @CreateUserNo
	);

	Set @TeamNo = SCOPE_IDENTITY();

END
GO
/*
*/