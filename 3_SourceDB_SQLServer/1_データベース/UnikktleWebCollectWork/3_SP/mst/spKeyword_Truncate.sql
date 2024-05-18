USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spKeyword_Truncate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spKeyword_Truncate] ;
GO
IF OBJECT_ID(N'[mst].[spKeyword_Truncate]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spKeyword_Truncate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spKeyword_Truncate]
AS
BEGIN

	SET NOCOUNT ON;

	TRUNCATE TABLE [mst].[tKeyword] ;

END
GO
/*
*/

