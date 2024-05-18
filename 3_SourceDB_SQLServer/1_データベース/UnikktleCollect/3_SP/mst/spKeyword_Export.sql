USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_Export]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Export] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Export]
AS
BEGIN

	INSERT INTO [UnikktleWeb].[clt].[tKeyword] (
		[No],
		[r_w],
		[Word],
		[FullTextSupple])
	SELECT
		[No],
		[r_w],
		[Word],
		''
	FROM [UnikktleCollect].[mst].[tKeyword] as a
	WHERE not exists 
	(
		SELECT *
		FROM [UnikktleWeb].[clt].[tKeyword] as b
		where (a.[No] = b.[No])
	);

	UPDATE [UnikktleWeb].[clt].[tKeyword]
	SET
		[r_w] = a.[r_w],
		[Word] = a.[Word],
		[FullTextSupple] = ''
	FROM
		[UnikktleCollect].[mst].[tKeyword] as a
	WHERE
		[UnikktleWeb].[clt].[tKeyword].[No] = a.[No]  ;

	-- Collect���ō폜���ꂽ���[�h��Web���ɗL��΍폜�B
	DELETE FROM [UnikktleWeb].[clt].[tKeyword]
	WHERE not exists 
	(
		SELECT *
		FROM [UnikktleCollect].[mst].[tKeyword] as b
		WHERE ([UnikktleWeb].[clt].[tKeyword].[No] = b.[No])
	);

	-- Collect���ŕs�̗p���肳�ꂽ���[�h��Web���ɗL��΍폜
	DELETE 
	--select *
	FROM [UnikktleWeb].[clt].[tKeyword]
	WHERE exists 
	(
		SELECT *
		FROM
			[UnikktleCollect].[mst].[tKeyword] as b
		WHERE
			([UnikktleWeb].[clt].[tKeyword].[No] = b.[No]) AND
			b.[�̗p] = 0 AND		-- 0�F�s�̗p
			b.[�̗p����ς�] = 1	-- 1�F����ς�
	);

END
GO

