USE [UnikktleMentorWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spAttribute_SelectClass_EN]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spAttribute_SelectClass_EN] ;
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spAttribute_SelectClass_EN]
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