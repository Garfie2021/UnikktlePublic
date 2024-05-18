USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelationWord_InsertUpdate]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelationWord_InsertUpdate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverRelationWord_InsertUpdate]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@AdverNo		int,
	@RelationWordNo	bigint,
	@ClickCost		smallint
AS
BEGIN

	--declare	@UserNo			bigint =40;
	--declare	@BusinessNo		smallint =1;
	--declare	@AdverNo		int =3;
	--declare	@RelationWordNo	bigint =1;
	--declare	@ClickCost		int =10;


	declare	@ClickCostTmp	int;
	SELECT @ClickCostTmp = [ClickCost]
	FROM [usr].[tAdverRelationWord]
    WHERE
		[UserNo] = @UserNo AND
        [BusinessNo] = @BusinessNo AND
        [AdverNo] = @AdverNo AND
        [RelationWordNo] = @RelationWordNo ;

	--select	@ClickCostTmp ;


	IF @ClickCostTmp IS NULL
	BEGIN

		INSERT INTO [usr].[tAdverRelationWord] (
			[UserNo],
			[BusinessNo],
			[AdverNo],
			[RelationWordNo],
			[ClickCost]
		) VALUES (
			@UserNo,
			@BusinessNo,
			@AdverNo,
			@RelationWordNo,
			@ClickCost
		);

	END
	ELSE
	BEGIN

		IF @ClickCostTmp <> @ClickCost
		BEGIN

			UPDATE [usr].[tAdverRelationWord]
			SET   [ClickCost] = @ClickCost
			WHERE [UserNo] = @UserNo AND
				  [BusinessNo] = @BusinessNo AND
				  [AdverNo] = @AdverNo AND
				  [RelationWordNo] = @RelationWordNo ;
				
		END;

	END;

END
GO
/*
*/