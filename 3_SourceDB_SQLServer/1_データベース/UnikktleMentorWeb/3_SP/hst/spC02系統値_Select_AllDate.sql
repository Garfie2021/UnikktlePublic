USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spC02�n���l_Select_AllDate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spC02�n���l_Select_AllDate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spC02�n���l_Select_AllDate]
	@UserNo		bigint
AS
BEGIN

	SELECT
		hst.tAnswerHistory.[AnswerId] AS Id,
		hst.tAnswerHistory.AnswerDateStart,
		hst.tC02�n���l.[D],
		hst.tC02�n���l.[C],
		hst.tC02�n���l.[I],
		hst.tC02�n���l.[N],
		hst.tC02�n���l.[O],
		hst.tC02�n���l.[Co],
		hst.tC02�n���l.[Ag],
		hst.tC02�n���l.[G],
		hst.tC02�n���l.[R],
		hst.tC02�n���l.[T],
		hst.tC02�n���l.[A],
		hst.tC02�n���l.[S]
	FROM
		hst.tC02�n���l INNER JOIN hst.tAnswerHistory 
			ON	hst.tC02�n���l.UserNo = hst.tAnswerHistory.UserNo AND 
				hst.tC02�n���l.AnswerId = hst.tAnswerHistory.AnswerId
	WHERE
		hst.tAnswerHistory.[UserNo] = @UserNo

END
GO
/*
*/