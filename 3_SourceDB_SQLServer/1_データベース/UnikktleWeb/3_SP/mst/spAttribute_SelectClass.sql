USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spAttribute_SelectClass]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spAttribute_SelectClass] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spAttribute_SelectClass]
	@Class		int
AS
BEGIN

	--declare	@No		bigint = 1;

	SELECT
      [AttributeNo] AS Id,
      [AttributeName] AS [Name]
	FROM
		[mst].[tAttribute]
	WHERE
		[Class] = @Class
	ORDER BY
		[OrderNo]
	;

END
GO
/*
*/