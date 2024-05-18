USE [UnikktleWebCollectWork]
GO
/*
*/
IF OBJECT_ID(N'[hst].[spCollaborateKeywordCount_WebServer_Truncate]', N'P') IS NOT NULL
	DROP PROCEDURE [hst].[spCollaborateKeywordCount_WebServer_Truncate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [hst].[spCollaborateKeywordCount_WebServer_Truncate]
AS
BEGIN

	SET NOCOUNT ON;

	Truncate TABLE [hst].[tCollaborateKeywordCount_WebServer] ;

END
GO
/*
*/

