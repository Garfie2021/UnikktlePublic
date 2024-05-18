USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_Update採用]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Update採用] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Update採用]
	@Word	nvarchar(100),
	@採用	tinyint
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET
		[採用] = @採用,		-- 0：不採用	1：採用
		[採用判定済み] = 1	-- 0：未判定	1：判定済み
	WHERE [Word] = @Word;

END
GO

