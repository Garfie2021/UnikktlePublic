USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spCollaborateKeyword_GetCount]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spCollaborateKeyword_GetCount] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [clt].[spCollaborateKeyword_GetCount]
	@Cnt		bigint OUTPUT
AS
BEGIN

	SELECT
		@Cnt = COUNT_BIG(*)
	FROM
		[clt].[tCollaborateKeyword]
	;

END
GO
/*
*/