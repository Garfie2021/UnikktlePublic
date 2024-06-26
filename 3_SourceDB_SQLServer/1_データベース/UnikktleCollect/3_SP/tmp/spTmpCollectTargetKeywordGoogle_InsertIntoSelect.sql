USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[tmp].[spTmpCollectTargetKeywordGoogle_InsertIntoSelect]', N'P') IS NOT NULL
	DROP PROCEDURE [tmp].[spTmpCollectTargetKeywordGoogle_InsertIntoSelect] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- MeCabの結果はtempテーブルを通して、Existsで本テーブルにInsertしないと行が重複するので、BulkCopy用に一時テーブルを作成する。
-- SELECT INTO を使うことで、テーブルの定義変更に動的に対応できる。
CREATE PROCEDURE [tmp].[spTmpCollectTargetKeywordGoogle_InsertIntoSelect]
AS
BEGIN

	INSERT INTO [hst].[t4CollectTargetKeyword_Google]
	(
		[SearchKeywordNo],
		[SearchDate],
		[SearchResultNo],
		[KeywordNo],
		[登録日時],
		[更新日時]
	)
	SELECT DISTINCT
		[SearchKeywordNo],
		[SearchDate],
		[SearchResultNo],
		[KeywordNo],
		[登録日時],
		[更新日時]
	FROM [tmp].[tTmpCollectTargetKeywordGoogle] as a
	where not exists 
	(
		SELECT *
		FROM [hst].[t4CollectTargetKeyword_Google] as b
		where 
			a.[SearchKeywordNo] = b.[SearchKeywordNo] AND
			a.[SearchDate] = b.[SearchDate] AND
			a.[SearchResultNo] = b.[SearchResultNo] AND
			a.[KeywordNo] = b.[KeywordNo]
	) ;

END
GO
/*
*/

