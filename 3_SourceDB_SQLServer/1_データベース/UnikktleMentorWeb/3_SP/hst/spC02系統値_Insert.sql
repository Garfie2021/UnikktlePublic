USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spC02�n���l_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spC02�n���l_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spC02�n���l_Insert]
	@UserNo		bigint,
	@AnswerId	int,
    @D			tinyint,
    @C			tinyint,
    @I			tinyint,
    @N			tinyint,
    @O			tinyint,
    @Co			tinyint,
    @Ag			tinyint,
    @G			tinyint,
    @R			tinyint,
    @T			tinyint,
    @A			tinyint,
    @S			tinyint
AS
BEGIN

	INSERT INTO [hst].[tC02�n���l] (
           [UserNo],
           [AnswerId],
           [D],
           [C],
           [I],
           [N],
           [O],
           [Co],
           [Ag],
           [G],
           [R],
           [T],
           [A],
           [S]
    ) VALUES (
           @UserNo,
           @AnswerId,
           @D,
           @C,
           @I,
           @N,
           @O,
           @Co,
           @Ag,
           @G,
           @R,
           @T,
           @A,
           @S
	);


END
GO
/*
*/