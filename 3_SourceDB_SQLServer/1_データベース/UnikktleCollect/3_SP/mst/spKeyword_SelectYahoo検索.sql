USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_SelectYahoo検索]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_SelectYahoo検索] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_SelectYahoo検索]
AS
BEGIN

	-- CollectYahooSearch2.exeが100件を処理するのに30分程度かかるので、2000件（10時間）にして夜間はずっと収集し続ける。
	SELECT  Top 1000
		[No],
		[Word]
	FROM 
		(
			SELECT Top 2000
				 [No]
				,[Word]
			FROM
				[mst].[tKeyword]
			WHERE 
				([採用判定済み] = 0 OR ([採用] = 1 AND [採用判定済み] = 1))
			ORDER BY
				--キーワードの検索が100年経っても終わらないので、Bing、Google、Yahooそれぞれで、まだ検索してないキーワードを検索する。
				 [Yahoo検索日時]
				,[Google検索日時]
				,[Bing検索日時]
				,[No]
		) as t
	ORDER BY
		[No] DESC
END
GO

