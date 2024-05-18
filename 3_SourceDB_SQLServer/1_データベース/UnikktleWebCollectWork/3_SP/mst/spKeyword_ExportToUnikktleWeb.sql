USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[mst].[sp7Keyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[sp7Keyword_ExportToUnikktleWeb] ;
GO
IF OBJECT_ID(N'[mst].[sp6Keyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[sp6Keyword_ExportToUnikktleWeb] ;
GO
IF OBJECT_ID(N'[mst].[spKeyword_ExportToUnikktleWeb]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_ExportToUnikktleWeb] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 計算結果をWEBデータベースへ出力
-- UnikktleCollectデータベースとUnikktleWebデータベースが、別サーバ上で稼働していて、
-- UnikktleWebCollectWorkデータベースを中間データベースとして使用する本番環境で使う。
CREATE PROCEDURE [mst].[spKeyword_ExportToUnikktleWeb]
AS
BEGIN

	SET NOCOUNT ON;


	declare cursor_tKeyword cursor for
	SELECT [No]
	FROM [UnikktleWebCollectWork].[mst].[tKeyword];

	open cursor_tKeyword;

	declare @No bigint;
	FETCH NEXT FROM cursor_tKeyword INTO @No;

	declare @Cnt int;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		SELECT @Cnt = COUNT(*)
		FROM [UnikktleWeb].[clt].[tKeyword]
		WHERE [No] = @No ;

		IF @Cnt < 1
		BEGIN
			-- 未登録のキーワード

			--print 'insert';
			--print @No;

			INSERT INTO [UnikktleWeb].[clt].[tKeyword]
			(
				[No],
				[r_w],
				[Word],
				[FullTextSupple]
			)
			SELECT
				[No],
				[r_w],
				[Word],
				[FullTextSupple]
			FROM
				[UnikktleWebCollectWork].[mst].[tKeyword]
			WHERE
				[No] = @No ;

		END
		ELSE
		BEGIN
			-- 登録済みのキーワード

			--print 'update';
			--print @No;

			UPDATE
				[UnikktleWeb].[clt].[tKeyword]
			SET
				[r_w] = B.[r_w],
				[Word] = B.[Word],
				[FullTextSupple] = B.[FullTextSupple]
			FROM
				[UnikktleWeb].[clt].[tKeyword] AS A INNER JOIN [UnikktleWebCollectWork].[mst].[tKeyword] AS B
				ON A.[No] = B.[No]
			WHERE
				A.[No] = @No ;
			;

		END;


		FETCH NEXT FROM cursor_tKeyword INTO @No;
	END;

	CLOSE cursor_tKeyword;
	DEALLOCATE cursor_tKeyword;


	-- デバッグ用にデータは残しておく
	--TRUNCATE TABLE [UnikktleWebCollectWork].[mst].[tKeyword] ;

/*
*/
END
GO

GO