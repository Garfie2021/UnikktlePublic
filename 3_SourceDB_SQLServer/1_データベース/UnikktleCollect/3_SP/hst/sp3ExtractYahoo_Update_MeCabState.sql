USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractYahoo_Update_MeCabState]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractYahoo_Update_MeCabState]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractYahoo_Update_MeCabState]
	@SearchKeywordNo	bigint,
	@SearchDate			datetime,
	@SearchResultNo		tinyint,
	@MeCabState			tinyint,
	@MeCab–¼ŽŒ			nvarchar(max)
AS
BEGIN

	UPDATE [hst].[t3ExtractYahoo]
	SET 
		[MeCabState] = @MeCabState,
		[MeCab–¼ŽŒ] = @MeCab–¼ŽŒ
	WHERE
		[SearchKeywordNo] = @SearchKeywordNo AND
		[SearchDate] = @SearchDate AND
		[SearchResultNo] = @SearchResultNo;


END
GO

