USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[tmp].[spTmpCollectTargetKeywordWebPage_InsertIntoSelect]', N'P') IS NOT NULL
	DROP PROCEDURE [tmp].[spTmpCollectTargetKeywordWebPage_InsertIntoSelect] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- MeCabの結果はtempテーブルを通して、Existsで本テーブルにInsertしないと行が重複するので、BulkCopy用に一時テーブルを作成する。
-- SELECT INTO を使うことで、テーブルの定義変更に動的に対応できる。
CREATE PROCEDURE [tmp].[spTmpCollectTargetKeywordWebPage_InsertIntoSelect]
AS
BEGIN

	INSERT INTO [hst].[t4CollectTargetKeyword_WebPage]
	(
		[DomainNo],
		[UrlNo],
		[KeywordNo],
		[登録日時],
		[更新日時]
	)
	SELECT DISTINCT
		[DomainNo],
		[UrlNo],
		[KeywordNo],
		[登録日時],
		[更新日時]
	FROM [tmp].[tTmpCollectTargetKeywordWebPage] as a
	where not exists 
	(
		SELECT *
		FROM [hst].[t4CollectTargetKeyword_WebPage] as b
		where 
			a.[DomainNo] = b.[DomainNo] AND
			a.[UrlNo] = b.[UrlNo] AND
			a.[KeywordNo] = b.[KeywordNo]
	) ;

END
GO
/*
*/

