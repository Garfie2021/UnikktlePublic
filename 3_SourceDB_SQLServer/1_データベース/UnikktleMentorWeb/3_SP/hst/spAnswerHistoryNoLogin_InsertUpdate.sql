USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spAnswerHistoryNoLogin_InsertUpdate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spAnswerHistoryNoLogin_InsertUpdate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spAnswerHistoryNoLogin_InsertUpdate]
	@SessionId			varchar(100),
	@AnswerDateStart	datetime,
	@AnswerDateEnd		datetime
AS
BEGIN

	DECLARE @cnt tinyint = (SELECT COUNT(*) FROM [hst].[tAnswerHistoryNoLogin]  WHERE [SessionId] = @SessionId) ;

	IF @cnt > 0
	BEGIN

		UPDATE
			[hst].[tAnswerHistoryNoLogin]
		SET
			[AnswerDateStart] = @AnswerDateStart,
			[AnswerDateEnd] = @AnswerDateEnd
		WHERE
			[SessionId] = @SessionId
		;

	END
	ELSE
	BEGIN

		INSERT INTO [hst].[tAnswerHistoryNoLogin] (
			[SessionId],
			[AnswerDateStart],
			[AnswerDateEnd]
		) VALUES (
			@SessionId,
			@AnswerDateStart,
			@AnswerDateEnd
		);

	END;

END
GO
/*
*/