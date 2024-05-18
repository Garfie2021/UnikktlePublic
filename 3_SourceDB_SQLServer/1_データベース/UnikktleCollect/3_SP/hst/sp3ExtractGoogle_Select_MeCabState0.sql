USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractGoogle_Select_MeCabState0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractGoogle_Select_MeCabState0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractGoogle_Select_MeCabState0]
	@CntMeCabState0			bigint output,
	@Cnt関連キーワード以外	bigint output,
	@Cnt日本語				bigint output,
	@Cnt英語				bigint output
AS
BEGIN

	-- 未解析の日本語レコードを取得
	SELECT
		[SearchKeywordNo],
		[SearchDate],
		[SearchResultNo],
		[英語連結名詞除外後]
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0 AND [言語判定] = 1 AND [関連キーワード] = 0; 

	-- MeCabState0 の件数。
	SELECT
		@CntMeCabState0 = COUNT(*)
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0; 

	-- 関連キーワード以外。
	SELECT
		@Cnt関連キーワード以外 = COUNT(*)
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0 AND [関連キーワード] = 0 ; 

	-- 日本語データの件数。
	SELECT
		@Cnt日本語 = COUNT(*)
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0 AND [言語判定] = 1 AND [関連キーワード] = 0; 

	-- 英語データの件数。
	SELECT
		@Cnt英語 = COUNT(*)
	FROM [hst].[t3ExtractGoogle]
	WHERE [MeCabState] = 0 AND [言語判定] = 2 AND [関連キーワード] = 0; 

END
GO

