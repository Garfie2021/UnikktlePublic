USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp2HtmlParseYahoo_Update_Html]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp2HtmlParseYahoo_Update_Html] ;
GO
IF OBJECT_ID(N'[hst].[sp2HtmlParseWebPage_Update_Html]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp2HtmlParseWebPage_Update_Html] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp2HtmlParseWebPage_Update_Html]
	@DomainNo				bigint,
	@UrlNo					bigint,
	@���ꔻ��				tinyint,
	@HtmlTag���O��1�i�K��	ntext,
	@HtmlTag���O��2�i�K��	ntext
AS
BEGIN

	UPDATE [hst].[t2HtmlParseWebPage]
	SET
		[3ExtractState] = null,
		[���ꔻ��] = @���ꔻ��,
		[HtmlTag���O��1�i�K��] = @HtmlTag���O��1�i�K��,
		[HtmlTag���O��2�i�K��] = @HtmlTag���O��2�i�K��
	WHERE
		[DomainNo] = @DomainNo AND
		[UrlNo] = @UrlNo
	;

END
GO

