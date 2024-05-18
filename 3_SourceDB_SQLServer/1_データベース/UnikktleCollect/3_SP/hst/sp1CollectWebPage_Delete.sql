USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectWebPage_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectWebPage_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectWebPage_Delete]
	@DomainNo	bigint,
	@UrlNo		bigint
AS
BEGIN

	DELETE FROM [hst].[t1CollectWebPage]
	WHERE [DomainNo] = @DomainNo AND [UrlNo] = @UrlNo

END
GO

