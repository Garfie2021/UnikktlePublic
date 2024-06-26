USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[tmp].[spTmpCollectTargetKeywordMail_InsertIntoSelect]', N'P') IS NOT NULL
	DROP PROCEDURE [tmp].[spTmpCollectTargetKeywordMail_InsertIntoSelect] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- MeCabの結果はtempテーブルを通して、Existsで本テーブルにInsertしないと行が重複するので、BulkCopy用に一時テーブルを作成する。
-- SELECT INTO を使うことで、テーブルの定義変更に動的に対応できる。
CREATE PROCEDURE [tmp].[spTmpCollectTargetKeywordMail_InsertIntoSelect]
AS
BEGIN

	INSERT INTO [hst].[t4CollectTargetKeyword_Mail]
	(
		[CollectTargetMailNo],
		[SendDate],
		[KeywordNo],
		[登録日時],
		[更新日時]
	)
	SELECT DISTINCT
		[CollectTargetMailNo],
		[SendDate],
		[KeywordNo],
		[登録日時],
		[更新日時]
	FROM [tmp].[tTmpCollectTargetKeywordMail] as a
	where not exists 
	(
		SELECT *
		FROM [hst].[t4CollectTargetKeyword_Mail] as b
		where 
			a.[CollectTargetMailNo] = b.[CollectTargetMailNo] AND
			a.[SendDate] = b.[SendDate] AND
			a.[KeywordNo] = b.[KeywordNo]
	) ;

END
GO
/*
*/

