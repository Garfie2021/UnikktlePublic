USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spSearchWord_GetNoWithInsert]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spSearchWord_GetNoWithInsert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spSearchWord_GetNoWithInsert]
	@Word	nvarchar(100),
	@No		bigint			output
AS
BEGIN

	declare @Cnt int = 0;
	SELECT @Cnt = count(*)
	FROM [mst].[tSearchWord]
	where [Word] = @Word;
	
	if @Cnt < 1
	begin

		INSERT INTO [mst].[tSearchWord] (
            [Word]
	    ) VALUES (
            @Word
		);

	end;

	SELECT @No = [No]
	FROM [mst].[tSearchWord]
	where [Word] = @Word;

END
GO
/*
*/

