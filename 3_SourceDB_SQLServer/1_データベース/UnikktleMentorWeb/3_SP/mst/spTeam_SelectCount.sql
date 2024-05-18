USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spTeam_SelectCount]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spTeam_SelectCount] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spTeam_SelectCount]
	@TeamID		nvarchar(100),
	@Cnt		int OUTPUT,
	@TeamNo		int OUTPUT
AS
BEGIN

	--declare	@CreateUserNo		bigint = 1;

	SELECT
		@Cnt = COUNT(*),
		@TeamNo = [No]
	FROM
		mst.tTeam
	WHERE
		mst.tTeam.[TeamID] = @TeamID
	GROUP BY
		[No]
	;

END
GO
/*
*/