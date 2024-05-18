USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_Update�̗p]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Update�̗p] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Update�̗p]
	@Word	nvarchar(100),
	@�̗p	tinyint
AS
BEGIN

	UPDATE [mst].[tKeyword]
	SET
		[�̗p] = @�̗p,		-- 0�F�s�̗p	1�F�̗p
		[�̗p����ς�] = 1	-- 0�F������	1�F����ς�
	WHERE [Word] = @Word;

END
GO

