USE UnikktleCollect
GO

IF OBJECT_ID(N'[hst].[sp1CollectGoogle_Truncate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp1CollectGoogle_Truncate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[sp1CollectGoogle_Truncate]
AS
BEGIN

	TRUNCATE TABLE [hst].[t1CollectGoogle];

END
GO

