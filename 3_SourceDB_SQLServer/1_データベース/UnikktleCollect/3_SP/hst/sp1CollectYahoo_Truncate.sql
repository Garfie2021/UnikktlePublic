USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectYahoo_Truncate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectYahoo_Truncate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectYahoo_Truncate]
AS
BEGIN

	TRUNCATE TABLE [hst].[t1CollectYahoo];

END
GO

