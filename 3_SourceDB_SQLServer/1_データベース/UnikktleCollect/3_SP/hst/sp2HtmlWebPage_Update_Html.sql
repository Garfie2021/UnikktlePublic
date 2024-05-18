USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp2HtmlParseYahoo_Update_Html]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp2HtmlParseYahoo_Update_Html] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp2HtmlParseYahoo_Update_Html]
	@DomainNo				bigint,
	@UrlNo					bigint,
	@3ExtractState			tinyint,
	@言語判定				tinyint,
	@HtmlTag除外後1段階目	ntext,
	@HtmlTag除外後2段階目	ntext
AS
BEGIN

	UPDATE [hst].[t2HtmlParseWebPage]
	SET
		[3ExtractState] = @3ExtractState,
		[言語判定] = @言語判定,
		[HtmlTag除外後1段階目] = @HtmlTag除外後1段階目,
		[HtmlTag除外後2段階目] = @HtmlTag除外後2段階目
	WHERE
		[DomainNo] = @DomainNo AND
		[UrlNo] = @UrlNo
	;

END
GO

