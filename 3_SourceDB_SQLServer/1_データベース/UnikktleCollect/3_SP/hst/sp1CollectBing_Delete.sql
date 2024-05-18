USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectBing_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectBing_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectBing_Delete]
	@day	int
AS
BEGIN

	DELETE FROM [hst].[t1CollectBing]
	WHERE [SearchDate] < DATEADD(DAY, @day, GETDATE());

END
GO

