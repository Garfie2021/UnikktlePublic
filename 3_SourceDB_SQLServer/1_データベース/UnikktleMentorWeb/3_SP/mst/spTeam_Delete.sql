USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_Delete]
	@TeamNo		int
AS
BEGIN

	DELETE
		[mst].[tTeam]
	WHERE
		[No] = @TeamNo;

END
GO
/*
*/