USE [UnikktleCollect]
GO
/*
*/
IF OBJECT_ID(N'[tmp].[spTmpCollectTargetKeyword_ReCreateTemptable]', N'P') IS NOT NULL
	DROP PROCEDURE [tmp].[spTmpCollectTargetKeyword_ReCreateTemptable] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- MeCabの結果はtempテーブルを通して、Existsで本テーブルにInsertしないと行が重複するので、BulkCopy用に一時テーブルを作成する。
-- SELECT INTO を使うことで、テーブルの定義変更に動的に対応できる。
CREATE PROCEDURE [tmp].[spTmpCollectTargetKeyword_ReCreateTemptable]
AS
BEGIN

	DROP TABLE IF EXISTS [tmp].[tTmpCollectTargetKeywordBing];
	DROP TABLE IF EXISTS [tmp].[tTmpCollectTargetKeywordYahoo];
	DROP TABLE IF EXISTS [tmp].[tTmpCollectTargetKeywordGoogle];
	DROP TABLE IF EXISTS [tmp].[tTmpCollectTargetKeywordMail];
	DROP TABLE IF EXISTS [tmp].[tTmpCollectTargetKeywordWebPage];

	SELECT * INTO [tmp].[tTmpCollectTargetKeywordBing] FROM [hst].[t4CollectTargetKeyword_Bing] WHERE 1<>1
	SELECT * INTO [tmp].[tTmpCollectTargetKeywordYahoo] FROM [hst].[t4CollectTargetKeyword_Yahoo] WHERE 1<>1
	SELECT * INTO [tmp].[tTmpCollectTargetKeywordGoogle] FROM [hst].[t4CollectTargetKeyword_Google] WHERE 1<>1
	SELECT * INTO [tmp].[tTmpCollectTargetKeywordMail] FROM [hst].[t4CollectTargetKeyword_Mail] WHERE 1<>1
	SELECT * INTO [tmp].[tTmpCollectTargetKeywordWebPage] FROM [hst].[t4CollectTargetKeyword_WebPage] WHERE 1<>1

END
GO
/*
*/

