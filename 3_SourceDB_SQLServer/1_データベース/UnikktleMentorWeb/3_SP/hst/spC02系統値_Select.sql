USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spC02ånìùíl_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spC02ånìùíl_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spC02ånìùíl_Select]
	@UserNo		bigint,
	@AnswerId	int
AS
BEGIN

	SELECT
		[AnswerId] AS Id,
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
	FROM
		[hst].[tC02ånìùíl]
	WHERE
		[UserNo] = @UserNo AND
		[AnswerId] = @AnswerId

END
GO
/*
*/