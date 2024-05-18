USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_SelectCount_CreateUserNo]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_SelectCount_CreateUserNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_SelectCount_CreateUserNo]
	@CreateUserNo	bigint,
	@Cnt			int OUTPUT
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		@Cnt = COUNT(*)
	FROM
		mst.tTeam
	WHERE
		mst.tTeam.[CreateUserNo] = @CreateUserNo
	;

END
GO
/*
*/