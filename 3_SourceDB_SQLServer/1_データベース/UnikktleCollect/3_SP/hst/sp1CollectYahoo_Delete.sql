USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectYahoo_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectYahoo_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectYahoo_Delete]
	@day	int
AS
BEGIN

	DELETE FROM [hst].[t1CollectYahoo]
	WHERE [SearchDate] < DATEADD(DAY, @day, GETDATE());

END
GO

