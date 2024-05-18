USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[mst].[spMind_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [mst].[spMind_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [mst].[spMind_Delete]
	@No		bigint
AS
BEGIN

	--declare	@No		bigint = 1;

	DELETE FROM
		[mst].[tMind]
	WHERE
		[No] = @No
	;

END
GO
/*
*/