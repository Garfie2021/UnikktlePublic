USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswerHistory_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswerHistory_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spAnswerHistory_Insert]
	@UserNo				bigint,
	@AnswerDateStart	datetime,
	@AnswerDateEnd		datetime,
	@AnswerId			int	OUTPUT
AS
BEGIN

	--declare	@No		bigint = 1;

	DECLARE @AnswerId_MAX int = (SELECT MAX([AnswerId]) FROM [hst].[tAnswerHistory] WHERE [UserNo] = @UserNo) ;

	IF @AnswerId_MAX is null
	BEGIN

		SET @AnswerId = 1;

	END
	ELSE
	BEGIN

		SET @AnswerId = @AnswerId_MAX + 1;

	END;

		INSERT INTO [hst].[tAnswerHistory] (
			[UserNo],
			[AnswerId],
			[AnswerDateStart],
			[AnswerDateEnd]
		) VALUES (
			@UserNo,
			@AnswerId,
			@AnswerDateStart,
			@AnswerDateEnd
		);

END
GO
/*
*/