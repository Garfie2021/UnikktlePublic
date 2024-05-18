USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spCollaborateKeyword_SelectDistinct_KeywordNo��]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spCollaborateKeyword_SelectDistinct_KeywordNo��] ;
GO
IF OBJECT_ID(N'[clt].[spCollaborateKeyword_SelectDistinct_KeywordNo]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spCollaborateKeyword_SelectDistinct_KeywordNo] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [clt].[spCollaborateKeyword_SelectDistinct_KeywordNo]
AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		DISTINCT [KeywordNo_��]
	FROM
		[clt].[tCollaborateKeyword]
	UNION
	SELECT
		DISTINCT [KeywordNo_��]
	FROM
		[clt].[tCollaborateKeyword];


END
GO
/*
*/

