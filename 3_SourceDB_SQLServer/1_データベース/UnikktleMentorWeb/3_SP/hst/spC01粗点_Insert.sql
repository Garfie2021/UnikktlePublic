/*
�g���ĂȂ��B

USE [UnikktleMentorWeb]
GO

IF OBJECT_ID(N'[hst].[spC01�e�__Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spC01�e�__Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spC01�e�__Insert]
	@UserNo		bigint,
	@AnswerId	int,
	@S			tinyint,
	@A			tinyint,
	@T			tinyint,
	@R			tinyint,
	@G			tinyint,
	@Ag			tinyint,
	@Co			tinyint,
	@O			tinyint,
	@N			tinyint,
	@I			tinyint,
	@C			tinyint,
	@D			tinyint
AS
BEGIN

	INSERT INTO [hst].[tC01�e�_] (
		[UserNo],
		[AnswerId],
		[S],
		[A],
		[T],
		[R],
		[G],
		[Ag],
		[Co],
		[O],
		[N],
		[I],
		[C],
		[D]
    ) VALUES (
        @UserNo,
        @AnswerId,
        @S,
        @A,
        @T,
        @R,
        @G,
        @Ag,
        @Co,
        @O,
        @N,
        @I,
        @C,
        @D
	);

END
GO
*/