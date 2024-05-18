USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectBing_Truncate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectBing_Truncate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectBing_Truncate]
AS
BEGIN

	TRUNCATE TABLE [hst].[t1CollectBing];

END
GO

