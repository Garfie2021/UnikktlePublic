USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectYahoo_Select_State0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectYahoo_Select_State0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectYahoo_Select_State0]
AS
BEGIN

	-- Html解析が終わっていない収集Webを取得。
	SELECT
		[SearchKeywordNo],
		[SearchDate],
		[検索結果Html]
	FROM [hst].[t1CollectYahoo]
	WHERE [State] = 0; 

END
GO

