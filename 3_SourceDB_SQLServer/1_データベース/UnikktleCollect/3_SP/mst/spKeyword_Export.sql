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

	-- Collect側で削除されたワードがWeb側に有れば削除。
	DELETE FROM [UnikktleWeb].[clt].[tKeyword]
	WHERE not exists 
	(
		SELECT *
		FROM [UnikktleCollect].[mst].[tKeyword] as b
		WHERE ([UnikktleWeb].[clt].[tKeyword].[No] = b.[No])
	);

	-- Collect側で不採用判定されたワードがWeb側に有れば削除
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
			b.[採用] = 0 AND		-- 0：不採用
			b.[採用判定済み] = 1	-- 1：判定済み
	);

END
GO

