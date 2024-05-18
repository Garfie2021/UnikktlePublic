USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectGoogle_Update_State]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectGoogle_Update_State] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectGoogle_Update_State]
	@SearchKeywordNo	bigint,
	@SearchDate			datetime,
	@State				tinyint,
	@HtmlParseResult	tinyint
AS
BEGIN

	UPDATE [hst].[t1CollectGoogle]
	SET
		[State] = @State,
		[HtmlParseResult] = @HtmlParseResult
	WHERE
		[SearchKeywordNo] = @SearchKeywordNo AND
		[SearchDate] = @SearchDate

END
GO

