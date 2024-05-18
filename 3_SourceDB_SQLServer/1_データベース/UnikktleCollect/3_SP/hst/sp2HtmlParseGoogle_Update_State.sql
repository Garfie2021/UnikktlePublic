USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp2HtmlParseGoogle_Update_State]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp2HtmlParseGoogle_Update_State] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp2HtmlParseGoogle_Update_State]
	@SearchKeywordNo	bigint,
	@SearchDate			datetime,
	@State				tinyint
AS
BEGIN

	UPDATE [hst].[t2HtmlParseGoogle]
	SET
		[State] = @State
	WHERE
		[SearchKeywordNo] = @SearchKeywordNo AND
		[SearchDate] = @SearchDate

END
GO

