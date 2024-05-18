USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearchWord_InsertUpdate]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearchWord_InsertUpdate] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
CREATE PROCEDURE [usr].[spAdverSearchWord_InsertUpdate]
	@UserNo			bigint,
	@BusinessNo		smallint,
	@AdverNo		int,
	@SearchWordNo	bigint,
	@ClickCost		smallint
AS
BEGIN

	--declare	@No		bigint = 1;
	--declare	@UserNo			bigint =0;
	--declare	@BusinessNo		smallint =0;
	--declare	@AdverNo		int =0;
	--declare	@SearchWordNo	bigint =0;


	declare	@ClickCostTmp	int;
	SELECT @ClickCostTmp = [ClickCost]
	FROM [usr].[tAdverSearchWord]
    WHERE
		[UserNo] = @UserNo AND
        [BusinessNo] = @BusinessNo AND
        [AdverNo] = @AdverNo AND
        [SearchWordNo] = @SearchWordNo ;

	--select	@ClickCost ;


	IF @ClickCostTmp IS NULL
	BEGIN

		INSERT INTO [usr].[tAdverSearchWord] (
			[UserNo],
			[BusinessNo],
			[AdverNo],
			[SearchWordNo],
			[ClickCost]
		) VALUES (
			@UserNo,
			@BusinessNo,
			@AdverNo,
			@SearchWordNo,
			@ClickCost
		);

	END
	ELSE
	BEGIN

		IF @ClickCostTmp <> @ClickCost
		BEGIN

			UPDATE [usr].[tAdverSearchWord]
			SET   [ClickCost] = @ClickCost
			WHERE [UserNo] = @UserNo AND
				  [BusinessNo] = @BusinessNo AND
				  [AdverNo] = @AdverNo AND
				  [SearchWordNo] = @SearchWordNo ;
				
		END;

	END;


END
GO
/*
*/