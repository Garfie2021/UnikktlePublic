USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractMail_Select_MeCabState0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractMail_Select_MeCabState0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractMail_Select_MeCabState0]
AS
BEGIN

	-- 未解析で日本語のレコードを取得
	SELECT
		 [CollectTargetNo]
		,[SendDate]
		,[登録日時]
		,[英語連結名詞除外後]
	FROM [hst].[t3ExtractMail]
	WHERE [MeCabState] = 0 AND [言語判定] = 1; 

END
GO

