USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp2HtmlParseWebPage_Update_State]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp2HtmlParseWebPage_Update_State] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp2HtmlParseWebPage_Update_State]
	@DomainNo	bigint,
	@UrlNo		datetime,
	@State		tinyint
AS
BEGIN

	UPDATE [hst].[t2HtmlParseWebPage]
	SET
		[State] = @State
	WHERE
		[DomainNo] = @DomainNo AND
		[UrlNo] = @UrlNo

END
GO

