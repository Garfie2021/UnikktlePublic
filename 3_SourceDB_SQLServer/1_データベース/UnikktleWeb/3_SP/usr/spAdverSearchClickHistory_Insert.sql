USE [UnikktleWeb]
GO
/*
*/
IF OBJECT_ID(N'[usr].[spAdverSearchClickHistory_Insert]', N'P') IS NOT NULL
	DROP PROCEDURE [usr].[spAdverSearchClickHistory_Insert] ;
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	
-- �s���N���b�N�΍��WEB���ŏ����B1�Z�b�V����/1�N���b�N�܂ŋL�^���ĉۋ��B
-- �ۋ��̓Z�b�V�����P�ʁB
-- SelectPR�ł́A1���ȓ��ɃN���b�N���ꂽPR�͕\�����Ȃ����ƂŁA�����L������ɕ\���������ɑΏ��B
CREATE PROCEDURE [usr].[spAdverSearchClickHistory_Insert]
	@UserNo				bigint,
	@BusinessNo			smallint,
	@AdverNo			int,
	@WordNo				bigint,
	@ClickUserNo		bigint,
	@ClickUserIP		varchar(20),
	@Email				nvarchar(256) OUTPUT
AS
BEGIN

	--declare @UserNo		bigint = 40;
	--declare @BusinessNo	smallint = 2;
	--declare @AdverNo		int = 1;
	--declare @ClickUserNo	bigint = 1;
	--declare @WordNo		bigint = 2;
	--declare @Email		nvarchar(256);

	
	DECLARE @ClickCost int;
	SELECT
		@ClickCost = [ClickCost]
	FROM
		[usr].[tAdverSearchWord]
	WHERE
		[UserNo] = @UserNo AND
		[BusinessNo] = @BusinessNo AND
		[AdverNo] = @AdverNo AND
		[SearchWordNo] = @WordNo ;

	-- �N���b�N����
	INSERT INTO [usr].[tAdverSearchClickHistory] (
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

	-- �ۗL�N���W�b�g�����Z
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

	-- �N���W�b�g��0�ȉ��ɂȂ������[�U�̍L���͖����ɂ���B
	IF @OwnedCredit < 1
	BEGIN
		UPDATE
			[usr].[tAdverRelation]
		SET
			[Valid] = 0	-- ���� = 0
		WHERE
			[Category] = 2 AND	-- �L��=2
			[UserNo] = @UserNo ;
		
		UPDATE
			[usr].[tAdverSearch]
		SET
			[Valid] = 0	-- ���� = 0
		WHERE
			[Category] = 2 AND	-- �L��=2
			[UserNo] = @UserNo ;

		-- �L����̃��[���A�h���X
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