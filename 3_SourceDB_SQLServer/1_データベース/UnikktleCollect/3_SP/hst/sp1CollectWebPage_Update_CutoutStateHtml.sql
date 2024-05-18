USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Update_State]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Update_State] ;
GO
IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Update_CutoutStateHtml]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Update_CutoutStateHtml] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectWebPage_Update_CutoutStateHtml]
	@DomainNo			bigint,
	@UrlNo				bigint,
	@CutoutStateHtml	tinyint
AS
BEGIN

	UPDATE [hst].[t1CollectWebPage]
	SET
		[CutoutStateHtml] = @CutoutStateHtml
	WHERE
		[DomainNo] = @DomainNo AND
		[UrlNo] = @UrlNo ;

END
GO

