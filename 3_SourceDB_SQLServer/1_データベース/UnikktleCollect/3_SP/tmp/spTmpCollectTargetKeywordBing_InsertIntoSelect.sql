USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[tmp].[spTmpCollectTargetKeywordBing_InsertIntoSelect]', N'P') IS NOT NULL
	DROP PROCEDURE [tmp].[spTmpCollectTargetKeywordBing_InsertIntoSelect] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- MeCabの結果はtempテーブルを通して、Existsで本テーブルにInsertしないと行が重複するので、BulkCopy用に一時テーブルを作成する。
-- SELECT INTO を使うことで、テーブルの定義変更に動的に対応できる。
CREATE PROCEDURE [tmp].[spTmpCollectTargetKeywordBing_InsertIntoSelect]
AS
BEGIN

	INSERT INTO [hst].[t4CollectTargetKeyword_Bing]
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
	FROM [tmp].[tTmpCollectTargetKeywordBing] as a
	where not exists 
	(
		SELECT *
		FROM [hst].[t4CollectTargetKeyword_Bing] as b
		where 
			a.[SearchKeywordNo] = b.[SearchKeywordNo] AND
			a.[SearchDate] = b.[SearchDate] AND
			a.[SearchResultNo] = b.[SearchResultNo] AND
			a.[KeywordNo] = b.[KeywordNo]
	) ;

	/* 下記に変えてもコストは変わらなかった。
	INSERT INTO [hst].[t4CollectTargetKeyword_Bing]
	(
		[SearchKeywordNo],
		[SearchDate],
		[SearchResultNo],
		[KeywordNo],
		[登録日時],
		[更新日時]
	)
	SELECT DISTINCT
		A.[SearchKeywordNo],
		A.[SearchDate],
		A.[SearchResultNo],
		A.[KeywordNo],
		A.[登録日時],
		A.[更新日時]
	FROM
		[tmp].[tTmpCollectTargetKeywordBing] AS A LEFT JOIN [hst].[t4CollectTargetKeyword_Bing] AS B ON
			A.[SearchKeywordNo] = B.[SearchKeywordNo] AND
			A.[SearchDate] = B.[SearchDate] AND
			A.[SearchResultNo] = B.[SearchResultNo] AND
			A.[KeywordNo] = B.[KeywordNo]
	WHERE
		B.[SearchKeywordNo] IS NULL
	;
	*/

END
GO
/*
*/

