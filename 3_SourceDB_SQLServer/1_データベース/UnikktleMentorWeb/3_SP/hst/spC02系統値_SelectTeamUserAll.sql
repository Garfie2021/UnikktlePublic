USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spC02�n���l_SelectTeamUserAll]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spC02�n���l_SelectTeamUserAll] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spC02�n���l_SelectTeamUserAll]
	@TeamNo	int
AS
BEGIN

	--declare	@TeamNo		int = 8;

	SELECT 
		[UserNo] AS Id,
		[Nickname],
		[D] AS �}����,
		[C] AS �C���̕ω�,
		[I] AS �򓙊�,
		[N] AS �_�o��,
		[O] AS ��ϐ�,
		[Co] AS ������,
		[Ag] AS �U����,
		[G] AS ������,
		[R] AS �̂�C,
		[T] AS �v�l��,
		[A] AS �x�z��,
		[S] AS �Љ
	FROM [hst].[tC02�n���l] INNER JOIN
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
			usr.tJoinTeam.[Status] in (2,3) -- 2�F�Q���ς݁@3�F�`�[���I�[�i�[
		GROUP BY
			usr.tUserSetting.[No],
			usr.tUserSetting.Nickname
		) AS teamUser
		ON teamUser.Id = [hst].[tC02�n���l].[UserNo] AND teamUser.[AnswerId] = [hst].[tC02�n���l].[AnswerId]

END
GO
/*
*/