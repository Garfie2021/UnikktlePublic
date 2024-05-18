USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[sp6CollaborateKeyword_Truncate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[sp6CollaborateKeyword_Truncate] ;
GO
IF OBJECT_ID(N'[hst].[spCollaborateKeyword_Truncate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeyword_Truncate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spCollaborateKeyword_Truncate]
AS
BEGIN

	SET NOCOUNT ON;

	Truncate TABLE [hst].[tCollaborateKeyword] ;

END
GO
/*
*/

