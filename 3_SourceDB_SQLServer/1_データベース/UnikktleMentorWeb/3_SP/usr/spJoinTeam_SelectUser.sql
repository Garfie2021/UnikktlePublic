USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spJoinTeam_SelectUser]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spJoinTeam_SelectUser] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spJoinTeam_SelectUser]
	@TeamNo		int
AS
BEGIN

	--declare	@TeamNo		int = 8;

	SELECT
		usr.tUserSetting.[No] AS Id,
		usr.tUserSetting.Nickname,
		[dbo].[AspNetUsers].UserName AS EMail,
		usr.tJoinTeam.[Status],
		MAX(hst.tAnswerHistory.[AnswerId]) AS AnswerId,
		MAX(hst.tAnswerHistory.AnswerDateStart) AS AnswerDateStart
	FROM
		usr.tJoinTeam LEFT JOIN
		hst.tAnswerHistory ON usr.tJoinTeam.UserNo = hst.tAnswerHistory.UserNo INNER JOIN
		usr.tUserSetting ON usr.tJoinTeam.UserNo = usr.tUserSetting.[No] INNER JOIN
		[dbo].[AspNetUsers] ON usr.tJoinTeam.UserNo = [dbo].[AspNetUsers].[No]
	--FROM
	--	usr.tJoinTeam INNER JOIN
	--	usr.tUserSetting ON usr.tJoinTeam.UserNo = usr.tUserSetting.[No]
	WHERE
		usr.tJoinTeam.[TeamNo] = @TeamNo
	GROUP BY
		usr.tUserSetting.[No],
		usr.tUserSetting.Nickname,
		[dbo].[AspNetUsers].UserName,
		usr.tJoinTeam.[Status]
	;

END
GO
/*
*/