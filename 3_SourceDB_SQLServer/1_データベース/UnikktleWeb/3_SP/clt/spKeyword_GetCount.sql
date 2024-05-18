USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[clt].[spKeyword_GetCount]', N'P') IS NOT NULL
	DROP PROCEDURE [clt].[spKeyword_GetCount] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [clt].[spKeyword_GetCount]
	@Cnt		bigint OUTPUT
AS
BEGIN

	SELECT
		@Cnt = COUNT_BIG(*)
	FROM
		[clt].[tKeyword]
	;

END
GO
/*
*/