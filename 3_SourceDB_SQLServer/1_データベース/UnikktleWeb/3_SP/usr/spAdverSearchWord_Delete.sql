USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearchWord_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearchWord_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverSearchWord_Delete]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@AdverNo		int,
	@SearchWordNo	bigint
AS
BEGIN

	--declare	@No		bigint = 1;
	--declare	@UserNo			bigint =0;
	--declare	@BusinessNo		smallint =0;
	--declare	@AdverNo		int =0;
	--declare	@SearchWordNo	bigint =0;

	DELETE FROM [usr].[tAdverSearchWord]
    WHERE
		[UserNo] = @UserNo AND
        [BusinessNo] = @BusinessNo AND
        [AdverNo] = @AdverNo AND
        [SearchWordNo] = @SearchWordNo ;

END
GO
/*
*/