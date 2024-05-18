USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp5CollaborateKeywordMail_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp5CollaborateKeywordMail_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 再計算ストアド
CREATE PROCEDURE [hst].[sp5CollaborateKeywordMail_Insert]
AS
BEGIN

	SET NOCOUNT ON;

	---- 計算対象のキーワードを抽出。
	---- ※テスト運用中なので1日/10キーワードづつ計算してる。
	--declare cursor_tKeyword cursor for
	--SELECT Top 10000
	--	[No]
	--FROM 
	--	[mst].[tKeyword]
	--WHERE 
	--	([採用判定済み] = 0 OR ([採用] = 1 AND [採用判定済み] = 1)) AND
	--	[Google検索日時] IS NOT NULL
	--ORDER BY
	--	[Collaborate更新日時],
	--	[No];

	--open cursor_tKeyword;

	--declare @No bigint;

	--FETCH NEXT FROM cursor_tKeyword INTO @No;

	---- 計算対象のキーワード分ループ
	--WHILE @@FETCH_STATUS = 0
	--BEGIN

		-- 古いデータは削除
		DELETE FROM [hst].[t5CollaborateKeyword_Mail]
		--WHERE [KeywordNo_元] = @No;


		-- キーワードの新しい関連をInsert
		INSERT INTO [hst].[t5CollaborateKeyword_Mail]
		(
			[KeywordNo_元],
			[KeywordNo_先],
			[同時出現ドキュメント数]
		)
		-- キーワード毎に最大100件の関連キーワードを抽出
		SELECT  --TOP(100)
			元No, 
			先No, 
			SUM(同時出現ドキュメント数) as 同時出現ドキュメント数
		FROM (
			SELECT
				元.[KeywordNo] AS 元No,
				先.[KeywordNo] AS 先No,
				--元.[CollectTargetMailNo] AS A1,
				--先.[CollectTargetMailNo] AS A2,
				--B.[No],
				--B.採用,
				--C.[No],
				--C.採用
				COUNT(*) AS 同時出現ドキュメント数
			FROM [hst].[t4CollectTargetKeyword_Mail] AS 元 INNER JOIN [hst].[t4CollectTargetKeyword_Mail] AS 先
				ON 元.[CollectTargetMailNo] = 先.[CollectTargetMailNo] AND 元.[SendDate] = 先.[SendDate]
				--INNER JOIN [mst].[tKeyword] AS B
				--ON 元.[KeywordNo] = B.[No]
				--INNER JOIN [mst].[tKeyword] AS C
				--ON 先.[KeywordNo] = C.[No]
			--WHERE
				--元.[KeywordNo] = @No AND
				--B.採用 = 1 --AND		-- ループ元で既に除外しているので不要
				--(C.[採用判定済み] = 0 or (C.[採用] = 1 AND C.[採用判定済み] = 1))
			GROUP BY
				元.[KeywordNo],
				先.[KeywordNo]

		) AS t
		WHERE
			[元No] <> [先No]
		GROUP BY
			元No,
			先No
		--ORDER BY
		--	同時出現ドキュメント数 DESC
		;


		--UPDATE [mst].[tKeyword]
		--SET	[Collaborate更新日時] = GETDATE()
		--WHERE [No] = @No;

	--	FETCH NEXT FROM cursor_tKeyword INTO @No;
	--END

	--CLOSE cursor_tKeyword;
	--DEALLOCATE cursor_tKeyword;

END
GO
/*
*/

