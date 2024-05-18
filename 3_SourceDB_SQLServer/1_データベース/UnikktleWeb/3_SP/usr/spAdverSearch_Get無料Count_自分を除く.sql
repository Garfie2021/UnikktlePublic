USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearch_Get����Count_����������]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearch_Get����Count_����������] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [usr].[spAdverSearch_Get����Count_����������]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@AdverNo		int,
	@Count			int OUTPUT
AS
BEGIN
	--declare	@UserNo			bigint = 40;
	--declare	@BusinessNo		smallint = 2;
	--declare	@AdverNo		int = 2;
	--declare	@Count			int;

	SELECT
		@Count = COUNT(*)
	FROM
		[usr].[tAdverSearch]
	WHERE
		[UserNo] = @UserNo AND
		[Category] = 1 AND 	-- ����=1
		--[Valid] = 1	-- �L��=1
		([BusinessNo] <> @BusinessNo OR [No] <> @AdverNo)
	;

	--select @Count;

END
GO
/*
*/