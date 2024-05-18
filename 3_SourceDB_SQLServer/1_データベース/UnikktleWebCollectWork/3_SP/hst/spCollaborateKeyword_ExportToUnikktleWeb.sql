USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp7CollaborateKeyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp7CollaborateKeyword_ExportToUnikktleWeb] ;
GO
IF OBJECT_ID(N'[hst].[sp6CollaborateKeyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp6CollaborateKeyword_ExportToUnikktleWeb] ;
GO
IF OBJECT_ID(N'[hst].[spCollaborateKeyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeyword_ExportToUnikktleWeb] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 計算結果をWEBデータベースへ出力
-- UnikktleCollectデータベースとUnikktleWebデータベースが、別サーバ上で稼働していて、
-- UnikktleWebCollectWorkデータベースを中間データベースとして使用する本番環境で使う。
CREATE PROCEDURE [hst].[spCollaborateKeyword_ExportToUnikktleWeb]
AS
BEGIN

	SET NOCOUNT ON;

	declare cursor_tCollaborateKeyword cursor for
	SELECT DISTINCT [KeywordNo_元]
	FROM [UnikktleWebCollectWork].[hst].[tCollaborateKeyword];

	open cursor_tCollaborateKeyword;

	declare @KeywordNo_元 bigint;
	FETCH NEXT FROM cursor_tCollaborateKeyword INTO @KeywordNo_元;

	declare @Cnt int;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		SELECT @Cnt = Count(*)
		FROM [UnikktleWeb].[clt].[tCollaborateKeyword]
		WHERE [KeywordNo_元] = @KeywordNo_元 ;

		BEGIN TRANSACTION;

		IF @Cnt > 0
		BEGIN

			-- 古いデータは削除
			-- ※レコード件数が不定なのでUpdateではなく、DELETEをしないといけない。
			DELETE FROM [UnikktleWeb].[clt].[tCollaborateKeyword]
			WHERE [KeywordNo_元] = @KeywordNo_元;

		END;

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
			[UnikktleWebCollectWork].[hst].[tCollaborateKeyword]
		WHERE
			[KeywordNo_元] = @KeywordNo_元 ;

		COMMIT; 


		FETCH NEXT FROM cursor_tCollaborateKeyword INTO @KeywordNo_元;
	END;


	CLOSE cursor_tCollaborateKeyword;
	DEALLOCATE cursor_tCollaborateKeyword;

	-- デバッグ用にデータは残しておく
	--TRUNCATE TABLE [UnikktleWebCollectWork].[hst].[tCollaborateKeyword] ;

END
GO
/*
*/

