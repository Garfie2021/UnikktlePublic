USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectGoogle_Select_UrlState0]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectGoogle_Select_UrlState0] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectGoogle_Select_UrlState0]
AS
BEGIN

	-- Html‰ğÍ‚ªI‚í‚Á‚Ä‚¢‚È‚¢ûWWeb‚ğæ“¾B
	SELECT
		[SearchKeywordNo],
		[SearchDate],
		[ŒŸõŒ‹‰ÊHtml]
	FROM [hst].[t1CollectGoogle]
	WHERE [UrlState] = 0;

END
GO

