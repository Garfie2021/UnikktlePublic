USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearch_Get–³—¿Count]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearch_Get–³—¿Count] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spAdverSearch_Get–³—¿Count]
	@UserNo			bigint,
	@Count			int OUTPUT
AS
BEGIN
	--declare	@UserNo			bigint = 40;
	--declare	@BusinessNo		smallint = 1;

	SELECT
		@Count = COUNT(*)
	FROM 
		[usr].[tAdverSearch]
	WHERE
		[UserNo] = @UserNo AND
		[Category] = 1	-- –³—¿=1
		--[Valid] = 1	-- —LŒø=1
	;

END
GO
/*
*/