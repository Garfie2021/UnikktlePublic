USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectYahoo_Update_UrlState]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectYahoo_Update_UrlState] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectYahoo_Update_UrlState]
	@SearchKeywordNo	bigint,
	@SearchDate			datetime,
	@UrlState			tinyint
AS
BEGIN

	UPDATE [hst].[t1CollectYahoo]
	SET
		[UrlState] = @UrlState
	WHERE
		[SearchKeywordNo] = @SearchKeywordNo AND
		[SearchDate] = @SearchDate
	;

END
GO

