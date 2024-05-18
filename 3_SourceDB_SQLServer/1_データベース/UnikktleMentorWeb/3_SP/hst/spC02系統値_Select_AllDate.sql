USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spC02系統値_Select_AllDate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spC02系統値_Select_AllDate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spC02系統値_Select_AllDate]
	@UserNo		bigint
AS
BEGIN

	SELECT
		hst.tAnswerHistory.[AnswerId] AS Id,
		hst.tAnswerHistory.AnswerDateStart,
		hst.tC02系統値.[D],
		hst.tC02系統値.[C],
		hst.tC02系統値.[I],
		hst.tC02系統値.[N],
		hst.tC02系統値.[O],
		hst.tC02系統値.[Co],
		hst.tC02系統値.[Ag],
		hst.tC02系統値.[G],
		hst.tC02系統値.[R],
		hst.tC02系統値.[T],
		hst.tC02系統値.[A],
		hst.tC02系統値.[S]
	FROM
		hst.tC02系統値 INNER JOIN hst.tAnswerHistory 
			ON	hst.tC02系統値.UserNo = hst.tAnswerHistory.UserNo AND 
				hst.tC02系統値.AnswerId = hst.tAnswerHistory.AnswerId
	WHERE
		hst.tAnswerHistory.[UserNo] = @UserNo

END
GO
/*
*/