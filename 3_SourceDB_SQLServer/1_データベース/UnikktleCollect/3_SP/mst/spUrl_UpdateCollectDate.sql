USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spUrl_UpdateCollectDate]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spUrl_UpdateCollectDate] ;
GO
IF OBJECT_ID(N'[mst].[spUrl_CollectDate]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spUrl_CollectDate] ;
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spUrl_UpdateCollectDate]
	@DomainNo		bigint,
	@UrlNo			bigint,
	@CollectDate	datetime
AS
BEGIN

	UPDATE [mst].[tUrl]
	SET
		[CollectDate] = @CollectDate
	WHERE
		[DomainNo] = @DomainNo AND
		[UrlNo] = @UrlNo;

END
GO

