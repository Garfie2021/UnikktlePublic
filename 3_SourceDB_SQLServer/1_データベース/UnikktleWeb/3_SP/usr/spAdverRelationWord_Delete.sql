USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelationWord_Delete]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelationWord_Delete] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverRelationWord_Delete]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@AdverNo		int,
	@RelationWordNo	bigint
AS
BEGIN

	--declare	@UserNo			bigint =40;
	--declare	@BusinessNo		smallint =1;
	--declare	@AdverNo		int =3;
	--declare	@RelationWordNo	bigint =1;

	DELETE FROM [usr].[tAdverRelationWord]
    WHERE
		[UserNo] = @UserNo AND
        [BusinessNo] = @BusinessNo AND
        [AdverNo] = @AdverNo AND
        [RelationWordNo] = @RelationWordNo ;

END
GO
/*
*/