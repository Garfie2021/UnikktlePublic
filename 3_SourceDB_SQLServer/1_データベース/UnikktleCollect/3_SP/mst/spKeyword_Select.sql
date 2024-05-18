USE UnikktleCollect
GO

IF OBJECT_ID(N'[mst].[spKeyword_Select]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Select] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Select]
AS
BEGIN

	SELECT
		 [No]
		,[Word]
	FROM [mst].[tKeyword]

END
GO

