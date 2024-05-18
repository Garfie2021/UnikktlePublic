USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverRelationClickHistory_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverRelationClickHistory_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 不正クリック対策はWEB側で処理。1セッション/1クリックまで記録して課金。
-- 課金はセッション単位。
-- SelectPRでは、1日以内にクリックされたPRは表示しないことで、同じ広告が常に表示される問題に対処。
CREATE PROCEDURE [usr].[spAdverRelationClickHistory_Insert]
	@UserNo				bigint,
	@BusinessNo			smallint,
	@AdverNo			int,
	@WordNo				bigint,
	@ClickUserNo		bigint,
	@ClickUserIP		varchar(20),
	@Email				nvarchar(256) OUTPUT
AS
BEGIN

	--declare	@No		bigint = 1;

	DECLARE @ClickCost int;
	SELECT
		@ClickCost = [ClickCost]
	FROM
		[usr].[tAdverRelationWord]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @AdverNo AND
		[RelationWordNo] = @WordNo ;

	-- クリック履歴
	INSERT INTO [usr].[tAdverRelationClickHistory] (
		[UserNo],
		[BusinessNo],
		[AdverNo],
		[WordNo],
		[ClickCost],
		[ClickUserNo],
		[ClickUserIP]
	) VALUES (
		@UserNo,
		@BusinessNo,
		@AdverNo,
		@WordNo,
		@ClickCost,
		@ClickUserNo,
		@ClickUserIP
	);

	-- 保有クレジットを減算
	UPDATE
		[usr].[tUserSetting]
	SET
		[OwnedCredit] = [OwnedCredit] - @ClickCost
	WHERE
		[No] = @UserNo ;

	DECLARE @OwnedCredit int;
	SELECT
		@OwnedCredit = [OwnedCredit]
	FROM
		[usr].[tUserSetting]
	WHERE
		[No] = @UserNo ;
	
	-- クレジットが0以下になったユーザの広告は無効にする。
	IF @OwnedCredit < 1
	BEGIN
		UPDATE
			[usr].[tAdverRelation]
		SET
			[Valid] = 0	-- 無効 = 0
		WHERE
			[Category] = 2 AND	-- 有料=2
			[UserNo] = @UserNo ;
		
		UPDATE
			[usr].[tAdverSearch]
		SET
			[Valid] = 0	-- 無効 = 0
		WHERE
			[Category] = 2 AND	-- 有料=2
			[UserNo] = @UserNo ;

		-- 広告主のメールアドレス
		SELECT
			@Email = [Email]
		FROM
			[dbo].[AspNetUsers]
		WHERE
			[No] = @UserNo ;
	END;

END
GO
/*
*/