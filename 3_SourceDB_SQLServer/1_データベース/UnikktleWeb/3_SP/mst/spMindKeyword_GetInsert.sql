USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMindKeyword_GetInsert]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMindKeyword_GetInsert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spMindKeyword_GetInsert]
	@Word	nvarchar(max),
	@No		bigint	output
AS
BEGIN

	declare @Cnt int = 0;
	SELECT @Cnt = COUNT(*)
	FROM
		[mst].[tMindKeyword]
	WHERE
		[Word] = @Word;

	if @Cnt < 1
	begin

		INSERT INTO [mst].[tMindKeyword] (
			[Word]
		) VALUES (
			@Word
		);

	end;

	SELECT
		@No = [No]
	FROM
		[mst].[tMindKeyword]
	WHERE
		[Word] = @Word
	;

END
GO
/*
*/

