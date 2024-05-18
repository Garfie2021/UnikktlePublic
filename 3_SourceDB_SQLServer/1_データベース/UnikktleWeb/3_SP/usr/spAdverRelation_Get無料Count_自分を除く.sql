USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelation_Get–³—¿Count_©•ª‚ğœ‚­]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelation_Get–³—¿Count_©•ª‚ğœ‚­] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spAdverRelation_Get–³—¿Count_©•ª‚ğœ‚­]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@AdverNo		int,
	@Count			int OUTPUT
AS
BEGIN
	--declare	@UserNo			bigint = 40;
	--declare	@BusinessNo		smallint = 1;
	--declare	@AdverNo		int = 2;
	--declare	@Count			int;

	SELECT
		@Count = COUNT(*)
	FROM 
		[usr].[tAdverRelation]
	WHERE
		[UserNo] = @UserNo AND
		[Category] = 1 AND 	-- –³—¿=1
		--[Valid] = 1	-- —LŒø=1
		([BusinessNo] <> @BusinessNo OR [No] <> @AdverNo)
	;

END
GO
/*
*/