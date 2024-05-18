USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp3ExtractBing_Update_MeCabState]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp3ExtractBing_Update_MeCabState] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp3ExtractBing_Update_MeCabState]
	@SearchKeywordNo	bigint,
	@SearchDate			datetime,
	@SearchResultNo		tinyint,
	@MeCabState			tinyint,
	@MeCab����			nvarchar(max)
AS
BEGIN

	UPDATE [hst].[t3ExtractBing]
	SET 
		[MeCabState] = @MeCabState,
		[MeCab����] = @MeCab����
	WHERE
		[SearchKeywordNo] = @SearchKeywordNo AND
		[SearchDate] = @SearchDate AND
		[SearchResultNo] = @SearchResultNo;


END
GO

