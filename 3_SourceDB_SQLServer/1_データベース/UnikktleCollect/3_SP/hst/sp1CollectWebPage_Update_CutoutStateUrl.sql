USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Update_UrlState]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Update_UrlState] ;
GO
IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Update_CutoutStateUrl]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Update_CutoutStateUrl] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectWebPage_Update_CutoutStateUrl]
	@DomainNo		bigint,
	@UrlNo			bigint,
	@CutoutStateUrl	tinyint
AS
BEGIN

	UPDATE [hst].[t1CollectWebPage]
	SET
		[CutoutStateUrl] = @CutoutStateUrl
	WHERE
		[DomainNo] = @DomainNo AND
		[UrlNo] = @UrlNo ;

END
GO

