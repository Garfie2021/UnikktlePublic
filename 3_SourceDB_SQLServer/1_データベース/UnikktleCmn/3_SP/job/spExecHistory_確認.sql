USE UnikktleCmn
GO

DROP PROCEDURE [job].[spExecHistory_確認]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [job].[spExecHistory_確認]
AS
BEGIN

	SELECT TOP (1000)
		[Type],
		CASE
			WHEN [Type] = 1		THEN '1 CollectEmailMEagazine'
			WHEN [Type] = 2		THEN '1 CollectGoogleSearch'
			WHEN [Type] = 3		THEN '1 CollectBingSearch'
			WHEN [Type] = 4		THEN '1 CollectYahooSearch'
			WHEN [Type] = 5		THEN '1_CollectWebPage'
			WHEN [Type] = 100	THEN '2 HtmlParse'
			WHEN [Type] = 101	THEN '2 HtmlParseWebPage'
			WHEN [Type] = 110	THEN '3 ExtractEnglishConcatNoun'
			WHEN [Type] = 120	THEN '4 MeCabExec'
			WHEN [Type] = 130	THEN '5 UnikktleCollect_協調フィルタリング'
			WHEN [Type] = 140	THEN 'FullText'
			ELSE ''
		END as TypeName,
		[StartDate],
		[EndDate],
		DATEDIFF(minute, [StartDate], [EndDate]) as '実行時間(分)'
	FROM
		[job].[tExecHistory]
	--WHERE
	--	[Type] in (1)
	ORDER BY
		[StartDate] DESC

END
GO

