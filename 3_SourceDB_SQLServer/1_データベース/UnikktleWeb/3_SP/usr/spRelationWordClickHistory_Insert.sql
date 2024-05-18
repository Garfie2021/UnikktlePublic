USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spRelationWordClickHistory_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spRelationWordClickHistory_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spRelationWordClickHistory_Insert]
	@WordNo			bigint,
	@ClickUserNo	bigint,
	@ClickUserIP	varchar(20)
AS
BEGIN

	--declare	@No		bigint = 1;

	declare	@cnt tinyint;
	SELECT @cnt = COUNT(*)
	FROM [usr].[tRelationWordClickHistory]
    WHERE
		[WordNo] = @WordNo AND
        [ClickUserNo] = @ClickUserNo AND
        [ClickUserIP] = @ClickUserIP ;

	IF @cnt < 1
	BEGIN
		INSERT INTO [usr].[tRelationWordClickHistory] (
			[WordNo],
			[ClickUserNo],
			[ClickUserIP]
		) VALUES (
			@WordNo,
			@ClickUserNo,
			@ClickUserIP
		);
	END ;

END
GO
/*
*/