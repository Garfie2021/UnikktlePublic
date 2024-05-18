USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spC02系統値_SelectTeamUserAll]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spC02系統値_SelectTeamUserAll] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spC02系統値_SelectTeamUserAll]
	@TeamNo	int
AS
BEGIN

	--declare	@TeamNo		int = 8;

	SELECT 
		[UserNo] AS Id,
		[Nickname],
		[D] AS 抑うつ性,
		[C] AS 気分の変化,
		[I] AS 劣等感,
		[N] AS 神経質,
		[O] AS 主観性,
		[Co] AS 協調性,
		[Ag] AS 攻撃性,
		[G] AS 活動性,
		[R] AS のん気,
		[T] AS 思考性,
		[A] AS 支配性,
		[S] AS 社会性
	FROM [hst].[tC02系統値] INNER JOIN
	(
		SELECT
			usr.tUserSetting.[No] AS Id,
			usr.tUserSetting.Nickname AS Nickname,
			MAX(hst.tAnswerHistory.AnswerId) AS AnswerId
		FROM
			usr.tJoinTeam INNER JOIN
			hst.tAnswerHistory ON usr.tJoinTeam.UserNo = hst.tAnswerHistory.UserNo INNER JOIN
			usr.tUserSetting ON usr.tJoinTeam.UserNo = usr.tUserSetting.No
		WHERE
			usr.tJoinTeam.TeamNo = @TeamNo AND
			usr.tJoinTeam.[Status] in (2,3) -- 2：参加済み　3：チームオーナー
		GROUP BY
			usr.tUserSetting.[No],
			usr.tUserSetting.Nickname
		) AS teamUser
		ON teamUser.Id = [hst].[tC02系統値].[UserNo] AND teamUser.[AnswerId] = [hst].[tC02系統値].[AnswerId]

END
GO
/*
*/