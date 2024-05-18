USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Update_Html]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Update_Html] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectWebPage_Update_Html]
	@DomainNo		bigint,
	@UrlNo			bigint,
	@CollectState	tinyint,
	@Html			ntext
AS
BEGIN

	UPDATE [hst].[t1CollectWebPage]
	SET
		[CollectState] = @CollectState,
		[CutoutStateUrl] = null,	-- 「null：未解析」に初期化。
		[CutoutStateHtml] = null,	-- 「null：未解析」に初期化。
		[Html] = @Html
	WHERE
		[DomainNo] = @DomainNo AND
		[UrlNo] = @UrlNo ;

END
GO

