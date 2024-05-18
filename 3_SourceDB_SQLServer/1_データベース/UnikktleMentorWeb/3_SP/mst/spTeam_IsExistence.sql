USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_IsExistence]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_IsExistence] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_IsExistence]
	@TeamID		nvarchar(100),
	@Cnt		int OUTPUT
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		@Cnt = COUNT(*)
	FROM
		mst.tTeam
	WHERE
		mst.tTeam.[TeamID] = @TeamID
	;

END
GO
/*
*/