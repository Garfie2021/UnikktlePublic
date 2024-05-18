USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spKeyword_Insert_FromWltSearchWord]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Insert_FromWltSearchWord] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Insert_FromWltSearchWord]
AS
BEGIN

	INSERT INTO [mst].[tKeyword] (
        --[No],	-- localhostでテストする際は有効にする。
        [CollectTargetCategory],
		[名詞区分],
        [r_w],
        [Word],
        [解析元データ]
	)
	SELECT
		--100,	-- localhostでテストする際は有効にする。
		6,	-- [CollectTargetCategory]には「6：WebサーバのUIから検索されたキーワード」を設定。
		0,	-- 「0：人手で目検」の扱いにする。
		UnikktleCmn.calc.fTextWidth([Word]),
		[Word],
		''
	FROM
		[wlt].[tSearchWord]
	WHERE
		[Word] NOT IN (SELECT [Word] FROM [mst].[tKeyword])

END
GO
/*
*/

