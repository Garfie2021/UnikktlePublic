USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelation_Get無料Count_自分を除く]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelation_Get無料Count_自分を除く] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spAdverRelation_Get無料Count_自分を除く]
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
		[Category] = 1 AND 	-- 無料=1
		--[Valid] = 1	-- 有効=1
		([BusinessNo] <> @BusinessNo OR [No] <> @AdverNo)
	;

END
GO
/*
*/