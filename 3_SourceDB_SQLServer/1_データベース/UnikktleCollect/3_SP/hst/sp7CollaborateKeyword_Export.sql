USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp7CollaborateKeyword_Export]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp7CollaborateKeyword_Export] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 計算結果をWEBデータベースへ出力
-- UnikktleCollectデータベースとUnikktleWebデータベースが、同一サーバ上で稼働している環境で使う。
-- ※[mst].[tKeyword]のエクスポートしてないので、本番環境では使えない。開発環境用。
CREATE PROCEDURE [hst].[sp7CollaborateKeyword_Export]
AS
BEGIN

	SET NOCOUNT ON;

	declare cursor_tKeyword cursor for
	SELECT [No]
	FROM [mst].[tKeyword];

	open cursor_tKeyword;

	declare @No bigint;
	FETCH NEXT FROM cursor_tKeyword INTO @No;

	declare @CntCollect bigint;
	declare @CntWeb bigint;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		SELECT @CntCollect = Count(*)
		FROM [hst].[t6CollaborateKeyword]
		WHERE [KeywordNo_元] = @No ;

		SELECT @CntWeb = Count(*)
		FROM [UnikktleWeb].[clt].[tCollaborateKeyword]
		WHERE [KeywordNo_元] = @No ;

		IF @CntCollect <> @CntWeb
		BEGIN
			BEGIN TRANSACTION;  

			-- 古いデータは削除
			DELETE FROM [UnikktleWeb].[clt].[tCollaborateKeyword]
			WHERE [KeywordNo_元] = @No;

			-- キーワードの新しい関連をInsert
			INSERT INTO [UnikktleWeb].[clt].[tCollaborateKeyword]
			(
				[KeywordNo_元],
				[KeywordNo_先],
				[同時出現ドキュメント数]
			)
			SELECT
				[KeywordNo_元],
				[KeywordNo_先],
				[同時出現ドキュメント数]
			FROM
				[hst].[t6CollaborateKeyword]
			WHERE
				[KeywordNo_元] = @No ;

			COMMIT; 
		END;

		FETCH NEXT FROM cursor_tKeyword INTO @No;
	END;

	CLOSE cursor_tKeyword;
	DEALLOCATE cursor_tKeyword;

END
GO
/*
*/

